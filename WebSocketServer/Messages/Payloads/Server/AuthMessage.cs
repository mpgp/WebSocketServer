// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthMessage.cs" company="mpgp">
//   Multiplayer Game Platform
// </copyright>
// <summary>
//   Данные для авторизации.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WebSocketServer.Messages.Payloads.Server
{
    /// <inheritdoc />
    public class AuthMessage : BaseMessage
    {        
        /// <summary>
        /// Текст сообщения об успешной или неудачной авторизации.
        /// </summary>
        public string Message { get; set; }
        
        /// <summary>
        /// Список пользователей онлайн
        /// </summary>
        public string[] UsersList { get; set; }

        /// <inheritdoc />
        protected override string MessageType => Types.AuthMessage;
    }
}
