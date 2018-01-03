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
    /// <summary>
    /// Список пользователей онлайн.
    /// </summary>
    public class UsersListMessage : BaseMessage
    {
        /// <summary>
        /// Тип сообщения.
        /// </summary>
        public const string Type = "USERS_LIST_MESSAGE";

        /// <summary>
        /// Список пользователей онлайн.
        /// </summary>
        public string UsersList { get; set; } = "ivan, andrew";

        /// <inheritdoc />
        protected override string MessageType => Type;
    }
}
