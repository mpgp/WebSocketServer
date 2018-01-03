namespace WebSocketServer
{
    public class WebSocketMessage<T> where T : BaseMessage
    {
        public string Type { get; set; }
        public T Payload { get; set; }
    }

    public class WebSocketMessage
    {
        public static WebSocketMessage<T> BuildMessage<T>(T message) where T : BaseMessage
        {
            return new WebSocketMessage<T>()
            {
                Type = message.ToString(),
                Payload = message
            };
        }

    }
}
