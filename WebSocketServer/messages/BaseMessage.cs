// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseMessage.cs" company="mpgp">
//   Multiplayer Game Platform
// </copyright>
// <summary>
//   Defines the BaseMessage type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WebSocketServer.Messages
{
    /// <summary>
    /// The base message.
    /// </summary>
    public abstract class BaseMessage
    {
        /// <summary>
        /// Gets the message type.
        /// </summary>
        protected abstract string MessageType { get; }

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public override string ToString() => this.MessageType;
    }
}
