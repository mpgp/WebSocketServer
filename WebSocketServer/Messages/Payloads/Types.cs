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
        public static readonly string AuthMessage = "AUTH_MESSAGE";

        /// <summary>
        /// The chat message.
        /// </summary>
        public static readonly string ChatMessage = "CHAT_MESSAGE";

        /// <summary>
        /// The users list message.
        /// </summary>
        public static readonly string UsersListMessage = "USERS_LIST_MESSAGE";

        /// <summary>
        /// The invalid message.
        /// </summary>
        public static readonly string InvalidMessage = "INVALID_MESSAGE";
    }
}
