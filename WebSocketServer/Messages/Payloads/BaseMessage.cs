// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseMessage.cs" company="mpgp">
//   Multiplayer Game Platform
// </copyright>
// <summary>
//   Defines the BaseMessage type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WebSocketServer.Messages.Payloads
{
    /// <summary>
    /// The base message.
    /// </summary>
    public class BaseMessage
    {
        /// <summary>
        /// Gets the message type.
        /// </summary>
        protected virtual string MessageType { get; } = string.Empty;

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public override string ToString() => MessageType;
    }
}
