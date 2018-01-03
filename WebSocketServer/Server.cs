using System;
using System.Collections.Generic;
using Fleck2.Interfaces;
using Newtonsoft.Json;

namespace WebSocketServer
{
    public class Server : IServer
    {
        private Dictionary<IWebSocketConnection, string> ConnectedSockets { get; set; }
        private string Hostname { get; }
        private int Port { get; }
        private string Protocol { get; }
        private Fleck2.WebSocketServer WSServer { get; set; }

        public Server(string hostname, int port, string protocol = "ws")
        {
            Hostname = hostname;
            Port = port;
            Protocol = protocol;
        }

        public void Authorize(IWebSocketConnection socket, WebSocketMessage<AuthMessage> data)
        {
            try
            {
                if (ConnectedSockets.ContainsValue(data.Payload.UserName))
                {
                    DisconnectSocket(socket, data);
                }
                else
                {
                    ConnectSocket(socket, data);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void ListenMessages()
        {
            while (Console.ReadLine() != "exit")
            {
            }
        }

        public void SendMessage(IWebSocketConnection socket, WebSocketMessage<ChatMessage> data)
        {
            try
            {
                var chatMessage = new ChatMessage()
                {
                    UserName = ConnectedSockets[socket],
                    Message = data.Payload.Message
                };

                var message = WebSocketMessage.BuildMessage(chatMessage);
                foreach (KeyValuePair<IWebSocketConnection, string> client in ConnectedSockets)
                {
                    client.Key.Send(message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public IServer Start()
        {
            Fleck2.FleckLog.Level = Fleck2.LogLevel.Debug;
            ConnectedSockets = new Dictionary<IWebSocketConnection, string>();
            WSServer = new Fleck2.WebSocketServer($"{Protocol}://{Hostname}:{Port}");
            WSServer.Start(socket =>
            {
                socket.OnClose = () => OnClose(socket);
                socket.OnMessage = message => OnMessage(socket, message);
            });

            return this;
        }

        private void ConnectSocket(IWebSocketConnection socket, WebSocketMessage<AuthMessage> data)
        {
            ConnectedSockets.Add(socket, data.Payload.UserName);

            var authMessage = new AuthMessage()
            {
                UserName = data.Payload.UserName,
                Status = AuthMessage.StatusCode.SUCCESS
            };
            socket.Send(WebSocketMessage.BuildMessage(authMessage));

            var chatMessage = new ChatMessage()
            {
                UserName = data.Payload.UserName,
                Message = "has joined the chat!"
            };
            SendMessage(socket, WebSocketMessage.BuildMessage(chatMessage));
        }

        private void DisconnectSocket(IWebSocketConnection socket, WebSocketMessage<AuthMessage> data)
        {
            var authMessage = new AuthMessage()
            {
                UserName = data.Payload.UserName,
                Message = "Error: the user name <" + data.Payload.UserName + "> is already in use!",
                Status = AuthMessage.StatusCode.ERROR
            };
            socket.Send(WebSocketMessage.BuildMessage(authMessage));

            socket.Close();
            if (ConnectedSockets.ContainsKey(socket))
            {
                ConnectedSockets.Remove(socket);
            }
        }

        private void OnClose(IWebSocketConnection socket)
        {
            if (ConnectedSockets.ContainsKey(socket))
            {
                var chatMessage = new ChatMessage()
                {
                    UserName = ConnectedSockets[socket],
                    Message = "has left from chat!"
                };
                SendMessage(socket, WebSocketMessage.BuildMessage(chatMessage));
                ConnectedSockets.Remove(socket);
            }
        }

        private void OnMessage(IWebSocketConnection socket, string webSocketMessage)
        {
            if (ConnectedSockets.ContainsKey(socket))
            {
                SendMessage(socket, JsonConvert.DeserializeObject<WebSocketMessage<ChatMessage>>(webSocketMessage));
            }
            else
            {
                Authorize(socket, JsonConvert.DeserializeObject<WebSocketMessage<AuthMessage>>(webSocketMessage));
            }
        }
    }
}
