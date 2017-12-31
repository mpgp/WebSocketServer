namespace WebSocketServer
{
    public class AuthMessage
    {
        public static readonly string Type = "AUTH_MESSAGE";
        public string UserName { get; set; }
    }
}
