// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InvalidMessage.cs" company="mpgp">
//   Multiplayer Game Platform
// </copyright>
// <summary>
//   Сообщение об ошибке.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WebSocketServer.Messages.Payloads.Server
{
    /// <inheritdoc />
    public class InvalidMessage : BaseMessage
    {
        /// <summary>
        /// Сообщение об ошибке.
        /// </summary>
        public string Message { get; set; }

        /// <inheritdoc />
        protected override string MessageType => Types.InvalidMessage;
    }
}
