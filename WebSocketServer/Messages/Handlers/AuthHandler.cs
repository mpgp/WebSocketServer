// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthHandler.cs" company="mpgp">
//   Multiplayer Game Platform
// </copyright>
// <summary>
//   Defines the AuthHandler type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WebSocketServer.Messages.Handlers
{
    using Payloads;
    using IWebSocketConnection = Fleck2.Interfaces.IWebSocketConnection;

    /// <inheritdoc />
    public class AuthHandler : BaseHandler
    {
        /// <inheritdoc />
        protected override string Target => AuthMessage.Type;

        /// <inheritdoc />
        public override bool CanHandle(IWebSocketConnection socket, string messageType, IServer server)
        {
            return base.CanHandle(socket, messageType, server) && !server.ConnectedSockets.ContainsKey(socket);
        }

        /// <inheritdoc />
        public override void Handle(IWebSocketConnection socket, string webSocketMessage, IServer server)
        {
            this.Authorize(socket, webSocketMessage, server);
        }

        /// <summary>
        /// The authorize.
        /// </summary>
        /// <param name="socket">
        /// The socket.
        /// </param>
        /// <param name="webSocketMessage">
        /// The web socket message.
        /// </param>
        /// <param name="server">
        /// The server.
        /// </param>
        private void Authorize(IWebSocketConnection socket, string webSocketMessage, IServer server)
        {
            try
            {
                var data = Newtonsoft.Json.JsonConvert.DeserializeObject<WebSocketMessage<AuthMessage>>(webSocketMessage);
                if (server.ConnectedSockets.ContainsValue(data.Payload.UserName))
                {
                    this.DisconnectSocket(socket, data, server);
                }
                else
                {
                    this.ConnectSocket(socket, data, server);
                }
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine(e.ToString());
            }
        }

        /// <summary>
        /// The connect socket.
        /// </summary>
        /// <param name="socket">
        /// The socket.
        /// </param>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <param name="server">
        /// The server.
        /// </param>
        private void ConnectSocket(IWebSocketConnection socket, WebSocketMessage<AuthMessage> data, IServer server)
        {
            server.ConnectedSockets.Add(socket, data.Payload.UserName);

            var authMessage = new AuthMessage()
                                  {
                                      UserName = data.Payload.UserName,
                                      Status = AuthMessage.StatusCode.Success
                                  };
            socket.Send(Helper.BuildMessage(authMessage));

            var chatMessage = new ChatMessage()
                                  {
                                      UserName = data.Payload.UserName,
                                      Message = "has joined the chat!"
                                  };
            Successor.ChatHandler.SendToAll(chatMessage, server);
        }

        /// <summary>
        /// The connect socket.
        /// </summary>
        /// <param name="socket">
        /// The socket.
        /// </param>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <param name="server">
        /// The server.
        /// </param>
        private void DisconnectSocket(IWebSocketConnection socket, WebSocketMessage<AuthMessage> data, IServer server)
        {
            var authMessage = new AuthMessage()
                                  {
                                      UserName = data.Payload.UserName,
                                      Message = "Error: the user name <" + data.Payload.UserName + "> is already in use!",
                                      Status = AuthMessage.StatusCode.Error
                                  };
            socket.Send(Helper.BuildMessage(authMessage));

            socket.Close();
            if (server.ConnectedSockets.ContainsKey(socket))
            {
                server.ConnectedSockets.Remove(socket);
            }
        }
    }
}
