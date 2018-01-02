using System;
using System.Linq;
using NUnit.Framework;
using System.Threading;

namespace WebSocketServer.Tests
{
    [TestFixture]
    public class ServerTest
    {
        const string _hostname = "localhost";
        const int _port = 8811;
        const string _protocol = "ws";

        string ResponseFromMyServer { get; set; }
        string PreviousResponseFromMyServer { get; set; }
        Server MyServer { get; set; }
        WebSocket4Net.WebSocket MyClient { get; set; }

        private void OnReceiveMessageFromServer(object sender, WebSocket4Net.MessageReceivedEventArgs args)
        {
            PreviousResponseFromMyServer = ResponseFromMyServer;
            ResponseFromMyServer = args.Message;
        }

        private void Log<T>(T expected)
        {
            Console.WriteLine("expected:" + expected);
            System.Diagnostics.Debug.WriteLine("expected:" + expected);

            Console.WriteLine("PreviousResponseFromMyServer:" + PreviousResponseFromMyServer);
            System.Diagnostics.Debug.WriteLine("PreviousResponseFromMyServer:" + PreviousResponseFromMyServer);

            Console.WriteLine("ResponseFromMyServer:" + ResponseFromMyServer);
            System.Diagnostics.Debug.WriteLine("ResponseFromMyServer:" + ResponseFromMyServer);
        }

        [OneTimeSetUp]
        public void RunBeforeAll()
        {
            MyServer = new Server(_hostname, _port, _protocol).Start();
            Thread.Sleep(1000);

            MyClient = new WebSocket4Net.WebSocket($"{_protocol}://{_hostname}:{_port}");
            MyClient.MessageReceived += new EventHandler<WebSocket4Net.MessageReceivedEventArgs>(OnReceiveMessageFromServer);
            MyClient.Open();
            Thread.Sleep(3000);
        }

        [SetUp]
        public void RunBeforeEach()
        {
            PreviousResponseFromMyServer = null;
            ResponseFromMyServer = null;
        }

        /// <summary>
        /// Тестирование авторизации на успех
        /// </summary>
        [Test, Order(1)]
        public void Test_Authorization_Success()
        {
            var expected = MyServer.BuildMessage(
                AuthMessage.Type,
                new AuthMessage()
                {
                    UserName = "admin2018",
                    Status = AuthMessage.StatusCode.SUCCESS
                }
            );
            MyClient.Send(
                MyServer.BuildMessage(
                    AuthMessage.Type,
                    new AuthMessage()
                    {
                        UserName = "admin2018"
                    }
                )
            );

            for (var i = 0; i < 50 && ResponseFromMyServer == null; ++i)
            {
                Thread.Sleep(100);
            }

            Log(expected);
            var possibleValues = new[] { PreviousResponseFromMyServer, ResponseFromMyServer };
            Assert.IsTrue(possibleValues.Contains(expected));
        }

        /// <summary>
        /// Тестирование авторизации другого клиента под уже существующим ником
        /// </summary>
        [Test, Order(2)]
        public void Test_Authorization_Error()
        {
            var anotherClient = new WebSocket4Net.WebSocket($"{_protocol}://{_hostname}:{_port}");
            anotherClient.MessageReceived += new EventHandler<WebSocket4Net.MessageReceivedEventArgs>(OnReceiveMessageFromServer);
            anotherClient.Open();
            Thread.Sleep(3000);

            var expected = MyServer.BuildMessage(
                AuthMessage.Type,
                new AuthMessage()
                {
                    Message = "Error: the user name <admin2018> is already in use!",
                    UserName = "admin2018",
                    Status = AuthMessage.StatusCode.ERROR
                }
            );
            anotherClient.Send(
                MyServer.BuildMessage(
                    AuthMessage.Type,
                    new AuthMessage()
                    {
                        UserName = "admin2018"
                    }
                )
            );

            for (var i = 0; i < 50 && ResponseFromMyServer == null; ++i)
            {
                Thread.Sleep(100);
            }

            Log(expected);
            Assert.AreEqual(expected, ResponseFromMyServer);
        }
    }
}
