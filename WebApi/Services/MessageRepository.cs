using Microsoft.AspNetCore.SignalR;
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
            var currentUserKey = _unitOfWork.CurrentUser.GetNameKey();

            var userInfoSender = _userInfoInMemory.GetUserInfo(currentUserKey);

            var receiverKey = _userInfoInMemory.GetNameKey(targetUserid, targetUserName, roletype);

            var userInfoReciever = _userInfoInMemory.GetUserInfo(receiverKey);

            if (userInfoSender != null)
            {
                return _hubContext.Clients.Client(userInfoReciever!.ConnectionId).SendAsync("SendDM", message, userInfoSender);
            }
            return Task.FromResult(() => { return; });

        }
    }
}
