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

    using IWebSocketConnection = Fleck2.Interfaces.IWebSocketConnection;

    /// <inheritdoc />
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
            Hostname = hostname;
            Port = port;
            Protocol = protocol;
        }

        /// <inheritdoc />
        public Dictionary<IWebSocketConnection, Models.UserModel> ConnectedSockets { get; set; }

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
        public void ListenMessages()
        {
            while (Console.ReadLine() != "exit")
            {
            }
        }
        
        /// <inheritdoc />
        public void SendToAll(Messages.Payloads.BaseMessage message)
        {
            var msg = Helper.BuildMessage(message);
            foreach (var client in ConnectedSockets)
            {
                client.Key.Send(msg);
            }
        }

        /// <inheritdoc />
        public IServer Start()
        {
            Fleck2.FleckLog.Level = Fleck2.LogLevel.Debug;
            ConnectedSockets = new Dictionary<IWebSocketConnection, Models.UserModel>();
            WsServer = new Fleck2.WebSocketServer($"{Protocol}://{Hostname}:{Port}");
            WsServer.Start(socket =>
            {
                socket.OnClose = () => OnClose(socket);
                socket.OnMessage = webSocketMessage => Messages.Successor.Handle(socket, webSocketMessage, this);
            });

            return this;
        }

        /// <summary>
        /// The on close.
        /// </summary>
        /// <param name="socket">
        /// The socket.
        /// </param>
        private void OnClose(IWebSocketConnection socket)
        {
            if (ConnectedSockets.ContainsKey(socket))
            {
                var userConnectionMessage = new Messages.Payloads.Server.UserConnectionMessage()
                {
                    Login = ConnectedSockets[socket].Login,
                    Status = "DISCONNECT"
                };
                SendToAll(userConnectionMessage);
                ConnectedSockets.Remove(socket);
            }
        }
    }
}
