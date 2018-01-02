namespace WebSocketServer
{
    public class ChatMessage : IMessage
    {
        public const string Type = "CHAT_MESSAGE";
        public string Message { get; set; }
        public string Time { get; set; } = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        public string UserName { get; set; }
        protected override string MessageType { get { return Type; } }
    }
}
