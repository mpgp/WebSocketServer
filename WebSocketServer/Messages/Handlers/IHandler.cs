// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHandler.cs" company="mpgp">
//   Multiplayer Game Platform
// </copyright>
// <summary>
//   Defines the IHandler type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WebSocketServer.Messages.Handlers
{
    /// <summary>
    /// The auth handler.
    /// </summary>
    public interface IHandler
    {
        /// <summary>
        /// Gets the target.
        /// </summary>
        string Target { get; }

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
        void Handle(Fleck2.Interfaces.IWebSocketConnection socket, string webSocketMessage, IServer server);
    }
}
