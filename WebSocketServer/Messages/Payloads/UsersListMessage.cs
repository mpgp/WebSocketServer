// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UsersListMessage.cs" company="mpgp">
//   Multiplayer Game Platform
// </copyright>
// <summary>
//   Список пользователей онлайн.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WebSocketServer.Messages.Payloads
{
    /// <inheritdoc />
    public class UsersListMessage : BaseMessage
    {
        /// <summary>
        /// Тип сообщения.
        /// </summary>
        public const string Type = "USERS_LIST_MESSAGE";

        /// <summary>
        /// Список пользователей онлайн.
        /// </summary>
        public string[] UsersList { get; set; }

        /// <inheritdoc />
        protected override string MessageType => Type;
    }
}
