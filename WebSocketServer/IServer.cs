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
        System.Collections.Generic.Dictionary<IWebSocketConnection, Models.UserModel> ConnectedSockets { get; set; }

        /// <summary>
        /// The listen messages.
        /// </summary>
        void ListenMessages();

        /// <summary>
        /// The send to all.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        void SendToAll(Messages.Payloads.BaseMessage message);

        /// <summary>
        /// The send to all.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="exclude">
        /// The exclude.
        /// </param>
        void SendToAllExcludeOne(Messages.Payloads.BaseMessage message, IWebSocketConnection exclude);

        /// <summary>
        /// The send to user.
        /// </summary>
        /// <param name="receiver">
        /// The receiver.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        void SendToUser(string receiver, Messages.Payloads.BaseMessage message);

        /// <summary>
        /// The start.
        /// </summary>
        /// <returns>
        /// The <see cref="IServer"/>.
        /// </returns>
        IServer Start();
    }
}