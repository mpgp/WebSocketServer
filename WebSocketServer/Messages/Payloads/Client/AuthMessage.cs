// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthMessage.cs" company="mpgp">
//   Multiplayer Game Platform
// </copyright>
// <summary>
//   Данные для авторизации.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WebSocketServer.Messages.Payloads.Client
{
    /// <inheritdoc />
    public partial class AuthMessage : BaseMessage
    {
        /// <summary>
        /// Тип сообщения.
        /// </summary>
        public const string Type = "AUTH_MESSAGE";

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string UserName { get; set; }

        /// <inheritdoc />
        protected override string MessageType => Type;
    }
}
