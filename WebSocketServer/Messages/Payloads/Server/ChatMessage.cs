// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChatMessage.cs" company="mpgp">
//   Multiplayer Game Platform
// </copyright>
// <summary>
//   Defines the ChatMessage type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WebSocketServer.Messages.Payloads.Server
{
    /// <inheritdoc />
    public class ChatMessage : BaseMessage
    {
        /// <summary>
        /// The type.
        /// </summary>
        public const string Type = "CHAT_MESSAGE";

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        public long Time { get; set; } = (long)(System.DateTime.UtcNow - new System.DateTime(1970, 1, 1)).TotalSeconds;

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string UserName { get; set; }

        /// <inheritdoc />
        protected override string MessageType => Type;
    }
}
