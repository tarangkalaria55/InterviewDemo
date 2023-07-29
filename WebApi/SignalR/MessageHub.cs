using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;
using WebApi.Caching;
using WebApi.Interfaces;
using WebApi.Repository;

namespace WebApi.SignalR
{
    [Authorize]
    public class MessageHub : Hub
    {
        private readonly IUserInfoInMemory _userInfoInMemory;
        private readonly IUnitOfWork _unitOfWork;

        public MessageHub(IUserInfoInMemory userInfoInMemory, IUnitOfWork unitOfWork)
        {
            _userInfoInMemory = userInfoInMemory;
            _unitOfWork = unitOfWork;
        }

        public override Task OnConnectedAsync()
        {
            Trace.TraceInformation("MapHub started. ID: {0}", this.GetConnectionId());

            var currentUser = _unitOfWork.CurrentUser;

            _userInfoInMemory.AddUpdate(currentUser.GetUserId()!, currentUser.GetUserEmail()!, currentUser.GetRoleType()!, this.GetConnectionId());

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? ex)
        {
            var currentUser = _unitOfWork.CurrentUser;
            _userInfoInMemory.Remove(currentUser.GetNameKey());
            return base.OnDisconnectedAsync(ex);
        }

        public Task SendDirectMessage(string roletype, string targetUserid, string targetUserName, string message)
        {
            var currentUserKey = _unitOfWork.CurrentUser.GetNameKey();

            var userInfoSender = _userInfoInMemory.GetUserInfo(currentUserKey);

            var receiverKey = _userInfoInMemory.GetNameKey(targetUserid, targetUserName, roletype);

            var userInfoReciever = _userInfoInMemory.GetUserInfo(receiverKey);

            if (userInfoSender != null)
            {
                return Clients.Client(userInfoReciever!.ConnectionId).SendAsync("SendDM", message, userInfoSender);
            }
            return Task.FromResult(() => { return; });

        }

        public async Task Leave()
        {
            var currentUserID = _unitOfWork.CurrentUser.GetUserId();
            _userInfoInMemory.Remove(Context.User!.Identity!.Name);
            await Clients.AllExcept(new List<string> { this.GetConnectionId() }).SendAsync(
                   "UserLeft",
                   currentUserID
                   );
        }

        public async Task Join()
        {
            var currentUser = _unitOfWork.CurrentUser;

            if (!_userInfoInMemory.AddUpdate(currentUser.GetUserId(), currentUser.GetUserEmail()!, currentUser.GetRoleType()!, this.GetConnectionId()))
            {
                // new user
                // var list = _userInfoInMemory.GetAllUsersExceptThis(Context.User.Identity.Name).ToList();
                await Clients.AllExcept(new List<string> { this.GetConnectionId() }).SendAsync(
                    "NewOnlineUser",
                    _userInfoInMemory.GetUserInfo(currentUser.GetNameKey())
                    );
            }
            else
            {
                // existing user joined again

            }

            await Clients.Client(this.GetConnectionId()).SendAsync(
                "Joined",
                _userInfoInMemory.GetUserInfo(currentUser.GetNameKey())
                );

            await Clients.Client(this.GetConnectionId()).SendAsync(
                "OnlineUsers",
                _userInfoInMemory.GetAllUsersExceptThis(currentUser.GetNameKey())
            );
        }

        public string GetConnectionId() => Context.ConnectionId;
    }
}
