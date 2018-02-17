// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DialogMessage.cs" company="mpgp">
//   Multiplayer Game Platform
// </copyright>
// <summary>
//   Defines the DialogMessage type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WebSocketServer.Messages.Payloads.Client
{
    /// <inheritdoc />
    public class DialogMessage : BaseMessage
    {
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string Message { get; set; }
        
        /// <summary>
        /// Gets or sets the receiver.
        /// </summary>
        public string Receiver { get; set; }

        /// <inheritdoc />
        protected override string MessageType => Types.DialogMessage;
    }
}
