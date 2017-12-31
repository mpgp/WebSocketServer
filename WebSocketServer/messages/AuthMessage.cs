namespace WebSocketServer
{
    public class AuthMessage
    {
        public enum StatusCode { ERROR, SUCCESS }

        public static readonly string Type = "AUTH_MESSAGE";
        public string Message { get; set; }
        public string UserName { get; set; }
        public StatusCode Status { get; set; }
    }
}
