using System;
using System.Collections.Generic;
using Fleck2.Interfaces;
using Newtonsoft.Json;

namespace WebSocketServer
{
    public class Server
    {
        private Dictionary<IWebSocketConnection, string> ConnectedSockets { get; set; }
        private string Hostname { get; set; }
        private int Port { get; set; }
        private Fleck2.WebSocketServer WSServer { get; set; }

        public Server(string hostname, int port)
        {
            Hostname = hostname;
            Port = port;
        }

        public void Start()
        {
            Fleck2.FleckLog.Level = Fleck2.LogLevel.Debug;
            ConnectedSockets = new Dictionary<IWebSocketConnection, string>();
            WSServer = new Fleck2.WebSocketServer($"ws://{Hostname}:{Port}");
            WSServer.Start(socket =>
            {
                socket.OnClose = () => OnClose(socket);
                socket.OnMessage = message => OnMessage(socket, message);
            });

            while (Console.ReadLine() != "exit")
            {
            }
        }

        private string BuildMessage<T>(string type, T payload)
        {
            return JsonConvert.SerializeObject(
                new WebSocketMessage<T>()
                {
                    Type = type,
                    Payload = payload
                }
            );
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
        }

        private void SendMessage(IWebSocketConnection socket, string webSocketMessage)
        {
            var data = JsonConvert.DeserializeObject<WebSocketMessage<ChatMessage>>(webSocketMessage);
            var chatMessage = new ChatMessage()
            {
                UserName = ConnectedSockets[socket],
                Message = data.Payload.Message
            };

            foreach (KeyValuePair<IWebSocketConnection, string> client in ConnectedSockets)
            {
                client.Key.Send(BuildMessage(ChatMessage.Type, chatMessage));
            }
        }
    }
}
