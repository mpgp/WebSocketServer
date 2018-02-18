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
    using System.Linq;
    using Models;
    using IWebSocketConnection = Fleck2.Interfaces.IWebSocketConnection;

    /// <inheritdoc />
    public class AuthHandler : BaseHandler
    {
        /// <inheritdoc />
        protected override string Target => Payloads.Types.AuthMessage;

        /// <inheritdoc />
        public override bool CanHandle(IWebSocketConnection socket, string messageType, IServer server)
        {
            return base.CanHandle(socket, messageType, server) && !server.ConnectedSockets.ContainsKey(socket);
        }

        /// <inheritdoc />
        public override void Handle(IWebSocketConnection socket, string webSocketMessage, IServer server)
        {
            Authorize(socket, webSocketMessage, server);
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
                var data =
                    Newtonsoft.Json.JsonConvert.DeserializeObject<WebSocketMessage<Payloads.Client.AuthMessage>>(webSocketMessage);

                var tokenData = AppContext.Instance.UsersTokens.FirstOrDefault(userToken => data.Payload.Token == userToken.Token);
                
                if (tokenData != null)
                {
                    var userData = AppContext.Instance.Users.FirstOrDefault(user => user.Id == tokenData.UserId);
                    if (userData != null && !server.ConnectedSockets.ContainsValue(userData))
                    {
                        ConnectSocket(socket, userData, server);
                    }
                    else
                    {
                        var authMessage = new Payloads.Server.AuthMessage()
                        {
                            Message = "ERROR.ALREADY_CONNECTED"
                        };
                        socket.Send(authMessage);
                        DisconnectSocket(socket, server);
                    }
                }
                else
                {
                    var authMessage = new Payloads.Server.AuthMessage()
                    {
                        Message = "ERROR.NOT_FOUND"
                    };
                    socket.Send(authMessage);
                    DisconnectSocket(socket, server);
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
        /// <param name="userData">
        /// The userData.
        /// </param>
        /// <param name="server">
        /// The server.
        /// </param>
        private void ConnectSocket(IWebSocketConnection socket, UserModel userData, IServer server)
        {
            server.ConnectedSockets.Add(socket, userData);
            var authMessage = new Payloads.Server.AuthMessage()
            {
                Message = "SUCCESS",
                UsersList = server.ConnectedSockets.Values.Select(user => user.Login).ToArray()
            };
            
            socket.Send(authMessage);

            var userConnectionMessage = new Payloads.Server.UserConnectionMessage()
                                  {
                                      Login = server.ConnectedSockets[socket].Login,
                                      Status = "CONNECT"
                                  };
            server.SendToAllExcludeOne(userConnectionMessage, exclude: socket);
        }

        /// <summary>
        /// The connect socket.
        /// </summary>
        /// <param name="socket">
        /// The socket.
        /// </param>
        /// <param name="server">
        /// The server.
        /// </param>
        private void DisconnectSocket(IWebSocketConnection socket, IServer server)
        {
            socket.Close();
            if (server.ConnectedSockets.ContainsKey(socket))
            {
                server.ConnectedSockets.Remove(socket);
            }
        }
    }
}
