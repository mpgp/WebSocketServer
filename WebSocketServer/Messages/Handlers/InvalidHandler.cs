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
    using IWebSocketConnection = Fleck2.Interfaces.IWebSocketConnection;
    
    /// <inheritdoc />
    public class InvalidHandler : BaseHandler
    {
        /// <inheritdoc />
        protected override string Target => string.Empty;

        /// <inheritdoc />
        public override void Handle(IWebSocketConnection socket, string webSocketMessage, IServer server)
        {
            var invalidMessage = new Payloads.Server.InvalidMessage()
            {
                Message = "Обнаружена ошибка. Предоставьте, пожалуйста, эту информацию разработчикам: "
                    + webSocketMessage
            };
            socket.Send(invalidMessage);
        }
    }
}
