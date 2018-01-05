// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UsersListMessage.cs" company="mpgp">
//   Multiplayer Game Platform
// </copyright>
// <summary>
//   Список пользователей онлайн.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WebSocketServer.Messages.Payloads.Server
{
    /// <inheritdoc />
    public class UsersListMessage : BaseMessage
    {
        /// <summary>
        /// Список пользователей онлайн.
        /// </summary>
        public string[] UsersList { get; set; }

        /// <inheritdoc />
        protected override string MessageType => Types.UsersListMessage;
    }
}
