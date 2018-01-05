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
        /// <inheritdoc />
        protected override string MessageType => Types.UsersListMessage;
    }
}
