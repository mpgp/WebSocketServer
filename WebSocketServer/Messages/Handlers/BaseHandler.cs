// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseHandler.cs" company="mpgp">
//   Multiplayer Game Platform
// </copyright>
// <summary>
//   Defines the BaseHandler type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WebSocketServer.Messages.Handlers
{
    using IWebSocketConnection = Fleck2.Interfaces.IWebSocketConnection;
    
    /// <summary>
    /// The auth handler.
    /// </summary>
    public abstract class BaseHandler
    {
        /// <summary>
        /// Gets the target.
        /// </summary>
        protected abstract string Target { get; }

        /// <summary>
        /// The handle.
        /// </summary>
        /// <param name="socket">
        /// The socket.
        /// </param>
        /// <param name="webSocketMessage">
        /// The message type.
        /// </param>
        /// <param name="server">
        /// The server.
        /// </param>
        public abstract void Handle(IWebSocketConnection socket, string webSocketMessage, IServer server);

        /// <summary>
        /// The can handle.
        /// </summary>
        /// <param name="socket">
        /// The socket.
        /// </param>
        /// <param name="messageType">
        /// The message type.
        /// </param>
        /// <param name="server">
        /// The server.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public virtual bool CanHandle(IWebSocketConnection socket, string messageType, IServer server)
        {
            return messageType == Target;
        }
    }
}
