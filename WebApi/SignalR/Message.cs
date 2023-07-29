namespace WebApi.SignalR
{
    public class MessageModel
    {
        public string Sender { get; set; } = default!;
        public string Receiver { get; set; } = default!;
        public string Message { get; set; } = default!;
    }
}
