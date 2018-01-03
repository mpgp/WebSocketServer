using Fleck2.Interfaces;

namespace WebSocketServer
{
    public interface IServer
    {
        void Authorize(IWebSocketConnection socket, WebSocketMessage<AuthMessage> webSocketMessage);
        void ListenMessages();
        void SendMessage(IWebSocketConnection socket, WebSocketMessage<ChatMessage> webSocketMessage);
        IServer Start();
    }
}