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
    public partial class AuthMessage : BaseMessage
    {
        /// <summary>
        /// Текст сообщения об успешной или неудачной авторизации.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        public StatusCode Status { get; set; }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string UserName { get; set; }

        /// <inheritdoc />
        protected override string MessageType => Types.AuthMessage;
    }
}
