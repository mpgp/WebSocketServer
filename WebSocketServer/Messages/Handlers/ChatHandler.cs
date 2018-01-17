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
                    UserName = server.ConnectedSockets[socket].Login,
                    Message = data.Payload.Message
                };
                SendToAll(chatMessage, server);
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine(e.ToString());
            }
        }

        /// <summary>
        /// The send to all.
        /// </summary>
        /// <param name="chatMessage">
        /// The chat message.
        /// </param>
        /// <param name="server">
        /// The server.
        /// </param>
        public void SendToAll(Payloads.Server.ChatMessage chatMessage, IServer server)
        {
            var message = Helper.BuildMessage(chatMessage);
            foreach (var client in server.ConnectedSockets)
            {
                client.Key.Send(message);
            }
        }
    }
}
