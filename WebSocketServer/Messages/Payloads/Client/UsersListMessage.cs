// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UsersListMessage.cs" company="mpgp">
//   Multiplayer Game Platform
// </copyright>
// <summary>
//   Список пользователей онлайн.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WebSocketServer.Messages.Payloads.Client
{
    /// <inheritdoc />
    public class UsersListMessage : BaseMessage
    {
        /// <summary>
        /// Тип сообщения.
        /// </summary>
        public const string Type = "USERS_LIST_MESSAGE";

        /// <inheritdoc />
        protected override string MessageType => Type;
    }
}
