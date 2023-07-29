using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;
using WebApi.Auth;
using WebApi.Caching;
using WebApi.Interfaces;
using WebApi.Repository;

namespace WebApi.SignalR
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MessageHub : Hub
    {
        private readonly IUserInfoInMemory _userInfoInMemory;
        private readonly IUnitOfWork _unitOfWork;

        public MessageHub(IUserInfoInMemory userInfoInMemory, IUnitOfWork unitOfWork)
        {
            _userInfoInMemory = userInfoInMemory;
            _unitOfWork = unitOfWork;
        }

        public override async Task OnConnectedAsync()
        {
            var currentUser = new CurrentUser();
            currentUser.SetCurrentUser(Context.User!);

            var connectionID = this.GetConnectionId();
            Trace.TraceInformation("MapHub started. ID: {0}", connectionID);

            _userInfoInMemory.AddUpdate(currentUser.GetUserId()!, currentUser.GetUserEmail()!, currentUser.GetRoleType()!, connectionID);

            var message = new MessageModel()
            {
                Sender = currentUser.GetUserEmail()!,
                Receiver = currentUser.GetUserEmail()!,
                Message = "Connected"
            };

            await Clients.Caller.SendAsync("Send", message);

            await Clients.Client(connectionID).SendAsync("Send", message);

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? ex)
        {
            var connectionID = this.GetConnectionId();

            var currentUser = new CurrentUser();
            currentUser.SetCurrentUser(Context.User!);

            _userInfoInMemory.Remove(currentUser.GetNameKey());

            await Clients.Caller.SendAsync("Send", currentUser.GetUserEmail()!, "Disconnected");

            var message = new MessageModel()
            {
                Sender = currentUser.GetUserEmail()!,
                Receiver = currentUser.GetUserEmail()!,
                Message = "Disconnected"
            };

            await Clients.Client(connectionID).SendAsync("Send", message);

            await base.OnDisconnectedAsync(ex);
        }


        public Task Send(string roletype, string targetUserid, string targetUserName, string message)
        {
            var currentUser = new CurrentUser();
            currentUser.SetCurrentUser(Context.User!);

            var receiverKey = _userInfoInMemory.GetNameKey(targetUserid, targetUserName, roletype);

            var userInfoReciever = _userInfoInMemory.GetUserInfo(receiverKey);

            if (currentUser.IsAuthenticated() && userInfoReciever != null)
            {

                var objMessage = new MessageModel()
                {
                    Sender = currentUser.GetUserEmail()!,
                    Receiver = userInfoReciever.UserName!,
                    Message = message
                };

                return Task.FromResult(async () => await Clients.Client(userInfoReciever!.ConnectionId).SendAsync("Send", objMessage));
            }
            return Task.FromResult(() => { return; });

        }

        public async Task Leave()
        {
            var currentUser = new CurrentUser();
            currentUser.SetCurrentUser(Context.User!);

            _userInfoInMemory.Remove(currentUser.GetNameKey());
            var objMessage = new MessageModel()
            {
                Sender = currentUser.GetUserEmail()!,
                Receiver = currentUser.GetUserEmail()!,
                Message = "User left"
            };
            await Clients.AllExcept(new List<string> { this.GetConnectionId() }).SendAsync("Leave", objMessage);
        }

        public async Task Join()
        {
            var currentUser = _unitOfWork.CurrentUser;

            if (!_userInfoInMemory.AddUpdate(currentUser.GetUserId(), currentUser.GetUserEmail()!, currentUser.GetRoleType()!, this.GetConnectionId()))
            {
                var objMessage1 = new MessageModel()
                {
                    Sender = currentUser.GetUserEmail()!,
                    Message = $"New Online User {_userInfoInMemory.GetUserInfo(currentUser.GetNameKey())!.UserName}"
                };
                await Clients.AllExcept(new List<string> { this.GetConnectionId() }).SendAsync("Join", objMessage1);

            }
            else
            {
                // existing user joined again

            }

            var objMessage = new MessageModel()
            {
                Sender = currentUser.GetUserEmail()!,
                Message = $"Joined : {_userInfoInMemory.GetUserInfo(currentUser.GetNameKey())!.UserName}"
            };

            await Clients.Client(this.GetConnectionId()).SendAsync("Join", objMessage);

            await Clients.Client(this.GetConnectionId()).SendAsync("Join", objMessage);
        }

        public string GetConnectionId() => Context.ConnectionId;
    }
}
