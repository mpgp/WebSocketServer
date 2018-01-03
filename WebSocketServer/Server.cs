// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Server.cs" company="mpgp">
//   Multiplayer Game Platform
// </copyright>
// <summary>
//   Defines the Server type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WebSocketServer
{
    using System;
    using System.Collections.Generic;

    using Fleck2.Interfaces;
    using Newtonsoft.Json;
    using WebSocketServer.Messages;
    using WebSocketServer.Messages.Payloads;

    /// <inheritdoc />
    /// <summary>
    /// The server.
    /// </summary>
    public class Server : IServer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Server"/> class.
        /// </summary>
        /// <param name="hostname">
        /// The hostname.
        /// </param>
        /// <param name="port">
        /// The port.
        /// </param>
        /// <param name="protocol">
        /// The protocol.
        /// </param>
        public Server(string hostname = "localhost", int port = 8181, string protocol = "ws")
        {
            this.Hostname = hostname;
            this.Port = port;
            this.Protocol = protocol;
        }

        /// <summary>
        /// Gets or sets the connected sockets.
        /// </summary>
        private Dictionary<IWebSocketConnection, string> ConnectedSockets { get; set; }

        /// <summary>
        /// Gets the hostname.
        /// </summary>
        private string Hostname { get; }

        /// <summary>
        /// Gets the port.
        /// </summary>
        private int Port { get; }

        /// <summary>
        /// Gets the protocol.
        /// </summary>
        private string Protocol { get; }

        /// <summary>
        /// Gets or sets the ws server.
        /// </summary>
        private Fleck2.WebSocketServer WsServer { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// The authorize.
        /// </summary>
        /// <param name="socket">
        /// The socket.
        /// </param>
        /// <param name="data">
        /// The data.
        /// </param>
        public void Authorize(IWebSocketConnection socket, WebSocketMessage<AuthMessage> data)
        {
            try
            {
                if (this.ConnectedSockets.ContainsValue(data.Payload.UserName))
                {
                    this.DisconnectSocket(socket, data);
                }
                else
                {
                    this.ConnectSocket(socket, data);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// The listen messages.
        /// </summary>
        public void ListenMessages()
        {
            while (Console.ReadLine() != "exit")
            {
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// The send message.
        /// </summary>
        /// <param name="socket">
        /// The socket.
        /// </param>
        /// <param name="data">
        /// The data.
        /// </param>
        public void SendMessage(IWebSocketConnection socket, WebSocketMessage<ChatMessage> data)
        {
            try
            {
                var chatMessage = new ChatMessage()
                {
                    UserName = this.ConnectedSockets[socket],
                    Message = data.Payload.Message
                };

                var message = Helper.BuildMessage(chatMessage);
                foreach (KeyValuePair<IWebSocketConnection, string> client in this.ConnectedSockets)
                {
                    client.Key.Send(message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// The start.
        /// </summary>
        /// <returns>
        /// The <see cref="T:WebSocketServer.IServer" />.
        /// </returns>
        public IServer Start()
        {
            Fleck2.FleckLog.Level = Fleck2.LogLevel.Debug;
            this.ConnectedSockets = new Dictionary<IWebSocketConnection, string>();
            this.WsServer = new Fleck2.WebSocketServer($"{this.Protocol}://{this.Hostname}:{this.Port}");
            this.WsServer.Start(socket =>
            {
                socket.OnClose = () => this.OnClose(socket);
                socket.OnMessage = message => this.OnMessage(socket, message);
            });

            return this;
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
        private void ConnectSocket(IWebSocketConnection socket, WebSocketMessage<AuthMessage> data)
        {
            this.ConnectedSockets.Add(socket, data.Payload.UserName);

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
            this.SendMessage(socket, Helper.BuildMessage(chatMessage));
        }

        /// <summary>
        /// The disconnect socket.
        /// </summary>
        /// <param name="socket">
        /// The socket.
        /// </param>
        /// <param name="data">
        /// The data.
        /// </param>
        private void DisconnectSocket(IWebSocketConnection socket, WebSocketMessage<AuthMessage> data)
        {
            var authMessage = new AuthMessage()
            {
                UserName = data.Payload.UserName,
                Message = "Error: the user name <" + data.Payload.UserName + "> is already in use!",
                Status = AuthMessage.StatusCode.Error
            };
            socket.Send(Helper.BuildMessage(authMessage));

            socket.Close();
            if (this.ConnectedSockets.ContainsKey(socket))
            {
                this.ConnectedSockets.Remove(socket);
            }
        }

        /// <summary>
        /// The on close.
        /// </summary>
        /// <param name="socket">
        /// The socket.
        /// </param>
        private void OnClose(IWebSocketConnection socket)
        {
            if (this.ConnectedSockets.ContainsKey(socket))
            {
                var chatMessage = new ChatMessage()
                {
                    UserName = this.ConnectedSockets[socket],
                    Message = "has left from chat!"
                };
                this.SendMessage(socket, Helper.BuildMessage(chatMessage));
                this.ConnectedSockets.Remove(socket);
            }
        }

        /// <summary>
        /// The on message.
        /// </summary>
        /// <param name="socket">
        /// The socket.
        /// </param>
        /// <param name="webSocketMessage">
        /// The web socket message.
        /// </param>
        private void OnMessage(IWebSocketConnection socket, string webSocketMessage)
        {
            if (this.ConnectedSockets.ContainsKey(socket))
            {
                this.SendMessage(socket, JsonConvert.DeserializeObject<WebSocketMessage<ChatMessage>>(webSocketMessage));
            }
            else
            {
                this.Authorize(socket, JsonConvert.DeserializeObject<WebSocketMessage<AuthMessage>>(webSocketMessage));
            }
        }
    }
}
