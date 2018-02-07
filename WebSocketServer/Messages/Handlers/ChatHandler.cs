// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChatHandler.cs" company="mpgp">
//   Multiplayer Game Platform
// </copyright>
// <summary>
//   Defines the AuthHandler type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WebSocketServer.Messages.Handlers
{
    using IWebSocketConnection = Fleck2.Interfaces.IWebSocketConnection;

    /// <inheritdoc />
    public class ChatHandler : BaseHandler
    {
        /// <inheritdoc />
        protected override string Target => Payloads.Types.ChatMessage;

        /// <inheritdoc />
        public override void Handle(IWebSocketConnection socket, string webSocketMessage, IServer server)
        {
            try
            {
                var data =
                    Newtonsoft.Json.JsonConvert.DeserializeObject<WebSocketMessage<Payloads.Client.ChatMessage>>(webSocketMessage);
                var chatMessage = new Payloads.Server.ChatMessage()
                {
                    Login = server.ConnectedSockets[socket].Login,
                    Message = data.Payload.Message
                };
                server.SendToAll(chatMessage);
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine(e.ToString());
            }
        }
    }
}
