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
    using IWebSocketConnection = Fleck2.Interfaces.IWebSocketConnection;

    /// <summary>
    /// The Server interface.
    /// </summary>
    public interface IServer
    {
        /// <summary>
        /// Gets or sets the connected sockets.
        /// </summary>
        System.Collections.Generic.Dictionary<IWebSocketConnection, string> ConnectedSockets { get; set; }

        /// <summary>
        /// The listen messages.
        /// </summary>
        void ListenMessages();

        /// <summary>
        /// The start.
        /// </summary>
        /// <returns>
        /// The <see cref="IServer"/>.
        /// </returns>
        IServer Start();
    }
}