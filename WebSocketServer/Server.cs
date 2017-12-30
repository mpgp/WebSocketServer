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
                socket.OnOpen = () => OnOpen(socket);
                socket.OnClose = () => OnClose(socket);
                socket.OnMessage = message => OnMessage(socket, message);
            });

            while (Console.ReadLine() != "exit")
            {
            }
        }

        private void DisconnectSocket(IWebSocketConnection socket, string userName)
        {
            var chatMessage = new ChatMessage()
            {
                UserName = userName,
                Message = "Error: the user name <" + userName + "> is already in use!"
            };
            socket.Send(JsonConvert.SerializeObject(chatMessage));
            socket.Close();
            if (ConnectedSockets.ContainsKey(socket))
            {
                ConnectedSockets.Remove(socket);
            }
        }

        private void OnClose(IWebSocketConnection socket)
        {
            Console.WriteLine("Close! $$$$$");
            ConnectedSockets.Remove(socket);
        }

        private void OnMessage(IWebSocketConnection socket, string message)
        {
            if (ConnectedSockets.ContainsKey(socket))
            {
                SendMessage(socket, message);
            }
            else
            {
                if (ConnectedSockets.ContainsValue(message))
                {
                    DisconnectSocket(socket, message);
                }
                else
                {
                    ConnectedSockets.Add(socket, message);
                }
            }
        }

        private void OnOpen(IWebSocketConnection socket)
        {
            Console.WriteLine("Open! $$$$");
        }

        private void SendMessage(IWebSocketConnection socket, string message)
        {
            var chatMessage = new ChatMessage()
            {
                UserName = ConnectedSockets[socket],
                Message = message
            };

            foreach (KeyValuePair<IWebSocketConnection, string> client in ConnectedSockets)
            {
                client.Key.Send(JsonConvert.SerializeObject(chatMessage));
            }
        }
    }
}
