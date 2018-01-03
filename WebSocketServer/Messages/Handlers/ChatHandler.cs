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

                var message = Helper.BuildMessage(chatMessage);
                foreach (var client in server.ConnectedSockets)
                {
                    client.Key.Send(message);
                }
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine(e.ToString());
            }
        }
    }
}
