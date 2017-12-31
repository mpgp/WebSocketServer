namespace WebSocketServer
{
    public class WebSocketMessage<T>
    {
        public string Type { get; set; }
        public T Payload { get; set; }
    }
}
