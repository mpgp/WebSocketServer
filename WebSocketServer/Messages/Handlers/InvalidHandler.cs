// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InvalidHandler.cs" company="mpgp">
//   Multiplayer Game Platform
// </copyright>
// <summary>
//   Defines the InvalidHandler type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WebSocketServer.Messages.Handlers
{
    /// <inheritdoc />
    public class InvalidHandler : BaseHandler
    {
        /// <inheritdoc />
        protected override string Target => string.Empty;

        /// <inheritdoc />
        public override void Handle(Fleck2.Interfaces.IWebSocketConnection socket, string webSocketMessage, IServer server)
        {
            var invalidMessage = new Payloads.InvalidMessage()
            {
                Message = $"Обнаружена ошибка. Предоставьте, пожалуйста, эту информацию разработчикам: '{webSocketMessage}'"
            };
            socket.Send(Helper.BuildMessage(invalidMessage));
        }
    }
}
