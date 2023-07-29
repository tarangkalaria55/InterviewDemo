using Microsoft.AspNetCore.SignalR;
using WebApi.Auth;
using WebApi.Caching;
using WebApi.Interfaces;
using WebApi.Repository;
using WebApi.SignalR;

namespace WebApi.Services
{
    public class MessageRepository : IMessageRepository
    {

        private readonly IHubContext<MessageHub> _hubContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserInfoInMemory _userInfoInMemory;
        public MessageRepository(IHubContext<MessageHub> hubContext, IUnitOfWork unitOfWork, IUserInfoInMemory userInfoInMemory)
        {
            _hubContext = hubContext;
            _unitOfWork = unitOfWork;
            _userInfoInMemory = userInfoInMemory;
        }


        public Task SendDirectMessage(string roletype, string targetUserid, string targetUserName, string message)
        {

            var currentUser = _unitOfWork.CurrentUser;

            var receiverKey = _userInfoInMemory.GetNameKey(targetUserid, targetUserName, roletype);

            var userInfoReciever = _userInfoInMemory.GetUserInfo(receiverKey);

            if (currentUser.GetUserEmail() != null && userInfoReciever != null)
            {
                var objMessage = new MessageModel()
                {
                    Sender = currentUser.GetUserEmail()!,
                    Receiver = userInfoReciever.UserName!,
                    Message = "Connected"
                };
                return Task.FromResult(async () => await _hubContext.Clients.Client(userInfoReciever.ConnectionId).SendAsync("Send", objMessage));
            }
            return Task.FromResult(() => { return; });

        }
    }
}
