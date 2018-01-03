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
    using Payloads;

    /// <inheritdoc />
    public class ChatHandler : IHandler
    {
        /// <inheritdoc />
        public string Target => ChatMessage.Type;

        /// <inheritdoc />
        public void Handle(Fleck2.Interfaces.IWebSocketConnection socket, string webSocketMessage, IServer server)
        {
            try
            {
                var data = Newtonsoft.Json.JsonConvert.DeserializeObject<WebSocketMessage<ChatMessage>>(webSocketMessage);
                var chatMessage = new ChatMessage()
                                      {
                                          UserName = server.ConnectedSockets[socket],
                                          Message = data.Payload.Message
                                      };
                this.SendToAll(chatMessage, server);

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
        public void SendToAll(ChatMessage chatMessage, IServer server)
        {
            var message = Helper.BuildMessage(chatMessage);
            foreach (var client in server.ConnectedSockets)
            {
                client.Key.Send(message);
            }
        }
    }
}
