namespace WebSocketServer
{
    public static class Helper
    {
        public static string Serialize<T>(T message) => Newtonsoft.Json.JsonConvert.SerializeObject(message);

        public static void Send<T>(this Fleck2.Interfaces.IWebSocketConnection socket, WebSocketMessage<T> message) where T : BaseMessage
        {
            socket.Send(Serialize(message));
        }
    }
}
