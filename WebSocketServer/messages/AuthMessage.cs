namespace WebSocketServer
{
    public class AuthMessage : BaseMessage
    {
        public const string Type = "AUTH_MESSAGE";
        public string Message { get; set; }
        public StatusCode Status { get; set; }
        public string UserName { get; set; }
        public enum StatusCode { ERROR, SUCCESS }
        protected override string MessageType { get { return Type; } }
    }
}
