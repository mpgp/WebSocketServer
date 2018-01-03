// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InvalidMessage.cs" company="mpgp">
//   Multiplayer Game Platform
// </copyright>
// <summary>
//   Сообщение об ошибке.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WebSocketServer.Messages.Payloads
{
    /// <inheritdoc />
    public class InvalidMessage : BaseMessage
    {
        /// <summary>
        /// Тип сообщения.
        /// </summary>
        public const string Type = "INVALID_MESSAGE";

        /// <summary>
        /// Сообщение об ошибке.
        /// </summary>
        public string Message { get; set; }

        /// <inheritdoc />
        protected override string MessageType => Type;
    }
}
