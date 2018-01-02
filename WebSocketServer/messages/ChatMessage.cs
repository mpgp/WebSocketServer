namespace WebSocketServer
{
    public class ChatMessage : IMessage
    {
        public const string MessageType = "CHAT_MESSAGE";
        public string Message { get; set; }
        public string Time { get; set; } = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        public string Type { get { return MessageType; } }
        public string UserName { get; set; }
    }
}
