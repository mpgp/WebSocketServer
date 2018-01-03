// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChatMessage.cs" company="mpgp">
//   Multiplayer Game Platform
// </copyright>
// <summary>
//   Defines the ChatMessage type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WebSocketServer.Messages
{
    /// <inheritdoc />
    /// <summary>
    /// The chat message.
    /// </summary>
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
        public string Time { get; set; } = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string UserName { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets the message type.
        /// </summary>
        protected override string MessageType => Type;
    }
}
