using WebApi.Common.Interfaces;

namespace WebApi.Interfaces
{
    public interface IMessageRepository : ITransientService
    {
        Task SendDirectMessage(string roletype, string targetUserid, string targetUserName, string message);
    }
}
