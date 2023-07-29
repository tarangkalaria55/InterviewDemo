namespace WebApi.SignalR
{
    public interface IMessageHubClient
    {
        Task Send(MessageModel message);

        Task Join(MessageModel message);

        Task Leave(MessageModel message);
    }
}
