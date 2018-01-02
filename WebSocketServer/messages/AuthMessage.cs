namespace WebSocketServer
{
    public class AuthMessage : IMessage
    {
        public const string MessageType = "AUTH_MESSAGE";
        public string Message { get; set; }
        public StatusCode Status { get; set; }
        public string Type { get { return MessageType; } }
        public string UserName { get; set; }
        public enum StatusCode { ERROR, SUCCESS }
    }
}
