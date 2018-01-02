using Fleck2.Interfaces;

namespace WebSocketServer
{
    public interface IServer
    {
        void Authorize(IWebSocketConnection socket, string webSocketMessage);
        void ListenMessages();
        void SendMessage(IWebSocketConnection socket, string webSocketMessage);
        IServer Start();
    }
}