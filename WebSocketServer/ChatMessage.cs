namespace WebSocketServer
{
    public class ChatMessage
    {
        public string Message { get; set; }
        public string Time { get; set; } = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        public string UserName { get; set; }
    }
}
