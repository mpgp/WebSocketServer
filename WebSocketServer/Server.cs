using System;
using System.Collections.Generic;
using Fleck2.Interfaces;
using Newtonsoft.Json;

namespace WebSocketServer
{
    public class Server
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

        public void ListenMessages()
        {
            while (Console.ReadLine() != "exit")
            {
            }
        }

        public Server Start()
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

        public void Authorize(IWebSocketConnection socket, string webSocketMessage)
        {
            try
            {
                var data = JsonConvert.DeserializeObject<WebSocketMessage<AuthMessage>>(webSocketMessage);
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

        public string BuildMessage<T>(string type, T payload)
        {
            return JsonConvert.SerializeObject(
                new WebSocketMessage<T>()
                {
                    Type = type,
                    Payload = payload
                }
            );
        }

        public void SendMessage(IWebSocketConnection socket, string webSocketMessage)
        {
            try
            {
                var data = JsonConvert.DeserializeObject<WebSocketMessage<ChatMessage>>(webSocketMessage);
                var chatMessage = new ChatMessage()
                {
                    UserName = ConnectedSockets[socket],
                    Message = data.Payload.Message
                };

                var message = BuildMessage(ChatMessage.Type, chatMessage);
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

        private void ConnectSocket(IWebSocketConnection socket, WebSocketMessage<AuthMessage> data)
        {
            ConnectedSockets.Add(socket, data.Payload.UserName);

            var authMessage = new AuthMessage()
            {
                UserName = data.Payload.UserName,
                Status = AuthMessage.StatusCode.SUCCESS
            };
            socket.Send(BuildMessage(AuthMessage.Type, authMessage));

            var chatMessage = new ChatMessage()
            {
                UserName = data.Payload.UserName,
                Message = "has joined the chat!"
            };
            SendMessage(socket, BuildMessage(ChatMessage.Type, chatMessage));
        }

        private void DisconnectSocket(IWebSocketConnection socket, WebSocketMessage<AuthMessage> data)
        {
            var authMessage = new AuthMessage()
            {
                UserName = data.Payload.UserName,
                Message = "Error: the user name <" + data.Payload.UserName + "> is already in use!",
                Status = AuthMessage.StatusCode.ERROR
            };
            socket.Send(BuildMessage(AuthMessage.Type, authMessage));

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
                SendMessage(socket, BuildMessage(ChatMessage.Type, chatMessage));
                ConnectedSockets.Remove(socket);
            }
        }

        private void OnMessage(IWebSocketConnection socket, string webSocketMessage)
        {
            if (ConnectedSockets.ContainsKey(socket))
            {
                SendMessage(socket, webSocketMessage);
            }
            else
            {
                Authorize(socket, webSocketMessage);
            }
        }
    }
}
