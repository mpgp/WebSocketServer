// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Types.cs" company="mpgp">
//   Multiplayer Game Platform
// </copyright>
// <summary>
//   Defines the Types of Messages.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WebSocketServer.Messages.Payloads
{
    /// <summary>
    /// The types of messages.
    /// </summary>
    public static class Types
    {
        /// <summary>
        /// The auth message.
        /// </summary>
        public const string AuthMessage = "AUTH_MESSAGE";

        /// <summary>
        /// The chat message.
        /// </summary>
        public const string ChatMessage = "CHAT_MESSAGE";

        /// <summary>
        /// The dialog message.
        /// </summary>
        public const string DialogMessage = "DIALOG_MESSAGE";

        /// <summary>
        /// The invalid message.
        /// </summary>
        public const string InvalidMessage = "INVALID_MESSAGE";
        
        /// <summary>
        /// The user connection message.
        /// </summary>
        public const string UserConnectionMessage = "USER_CONNECTION_MESSAGE";
    }
}
