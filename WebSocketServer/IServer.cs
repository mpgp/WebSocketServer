// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IServer.cs" company="mpgp">
//   Multiplayer Game Platform
// </copyright>
// <summary>
//   Defines the IServer type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WebSocketServer
{
    using Fleck2.Interfaces;
    using WebSocketServer.Messages;
    using WebSocketServer.Messages.Payloads;

    /// <summary>
    /// The Server interface.
    /// </summary>
    public interface IServer
    {
        /// <summary>
        /// The authorize.
        /// </summary>
        /// <param name="socket">
        /// The socket.
        /// </param>
        /// <param name="webSocketMessage">
        /// The web socket message.
        /// </param>
        void Authorize(IWebSocketConnection socket, WebSocketMessage<AuthMessage> webSocketMessage);

        /// <summary>
        /// The listen messages.
        /// </summary>
        void ListenMessages();

        /// <summary>
        /// The send message.
        /// </summary>
        /// <param name="socket">
        /// The socket.
        /// </param>
        /// <param name="webSocketMessage">
        /// The web socket message.
        /// </param>
        void SendMessage(IWebSocketConnection socket, WebSocketMessage<ChatMessage> webSocketMessage);

        /// <summary>
        /// The start.
        /// </summary>
        /// <returns>
        /// The <see cref="IServer"/>.
        /// </returns>
        IServer Start();
    }
}