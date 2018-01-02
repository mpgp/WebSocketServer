namespace WebSocketServer
{
    public class WebSocketMessage<T> where T : BaseMessage
    {
        public string Type { get; set; }
        public T Payload { get; set; }

        public static string BuildMessage(T message)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject
            (
                new WebSocketMessage<T>()
                {
                    Type = message.ToString(),
                    Payload = message
                }
            );
        }

    }
}
