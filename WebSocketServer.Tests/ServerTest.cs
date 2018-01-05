// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServerTest.cs" company="mpgp">
//   Multiplayer Game Platform
// </copyright>
// <summary>
//   Defines the ServerTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WebSocketServer.Tests
{
    using System;
    using System.Linq;
    using System.Threading;
    using NUnit.Framework;
    using WebSocketServer;

    /// <summary>
    /// The server test.
    /// </summary>
    [TestFixture]
    public class ServerTest
    {
        /// <summary>
        /// The Hostname.
        /// </summary>
        private const string Hostname = "localhost";

        /// <summary>
        /// The Port.
        /// </summary>
        private const int Port = 8811;

        /// <summary>
        /// The Protocol.
        /// </summary>
        private const string Protocol = "ws";

        /// <summary>
        /// Gets or sets the response from my server.
        /// </summary>
        private string ResponseFromMyServer { get; set; }

        /// <summary>
        /// Gets or sets the previous response from my server.
        /// </summary>
        private string PreviousResponseFromMyServer { get; set; }

        /// <summary>
        /// Gets or sets the my client.
        /// </summary>
        private WebSocket4Net.WebSocket MyClient { get; set; }

        /// <summary>
        /// The run before all.
        /// </summary>
        [OneTimeSetUp]
        public void RunBeforeAll()
        {
            // ReSharper disable once RedundantArgumentDefaultValue
            new Server(Hostname, Port, Protocol).Start();
            Thread.Sleep(1000);

            MyClient = new WebSocket4Net.WebSocket($"{Protocol}://{Hostname}:{Port}");
            MyClient.MessageReceived += OnReceiveMessageFromServer;
            MyClient.Open();
            Thread.Sleep(3000);
        }

        /// <summary>
        /// The run before each.
        /// </summary>
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
        public void TestAuthorizationSuccess()
        {
            MyClient.Send(Helper.Serialize(Helper.BuildMessage(new WebSocketServer.Messages.Payloads.Client.AuthMessage() { UserName = "admin2018" })));

            for (var i = 0; i < 50 && ResponseFromMyServer == null; ++i)
            {
                Thread.Sleep(100);
            }

            var expected = Helper.Serialize(
                Helper.BuildMessage(
                    new WebSocketServer.Messages.Payloads.Server.AuthMessage()
                        {
                            UserName = "admin2018",
                            Status = WebSocketServer.Messages.Payloads.Server.AuthMessage.StatusCode.Success
                        }));
            Log(expected);
            var possibleValues = new[] { PreviousResponseFromMyServer, ResponseFromMyServer };
            Assert.IsTrue(possibleValues.Contains(expected));
        }

        /// <summary>
        /// Тестирование авторизации другого клиента под уже существующим ником
        /// </summary>
        [Test, Order(2)]
        public void TestAuthorizationError()
        {
            var anotherClient = new WebSocket4Net.WebSocket($"{Protocol}://{Hostname}:{Port}");
            anotherClient.MessageReceived += OnReceiveMessageFromServer;
            anotherClient.Open();
            Thread.Sleep(3000);

            anotherClient.Send(
                Helper.Serialize(
                    Helper.BuildMessage(
                        new WebSocketServer.Messages.Payloads.Client.AuthMessage()
                        {
                            UserName = "admin2018"
                        })));

            for (var i = 0; i < 50 && ResponseFromMyServer == null; ++i)
            {
                Thread.Sleep(100);
            }

            var expected = Helper.Serialize(
                Helper.BuildMessage(
                    new WebSocketServer.Messages.Payloads.Server.AuthMessage()
                        {
                            Message = "Error: the user name <admin2018> is already in use!",
                            UserName = "admin2018",
                            Status = WebSocketServer.Messages.Payloads.Server.AuthMessage.StatusCode.Error
                        }));
            Log(expected);
            Assert.AreEqual(expected, ResponseFromMyServer);
        }
        
        /// <summary>
        /// The on receive message from server.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        private void OnReceiveMessageFromServer(object sender, WebSocket4Net.MessageReceivedEventArgs args)
        {
            PreviousResponseFromMyServer = ResponseFromMyServer;
            ResponseFromMyServer = args.Message;
        }

        /// <summary>
        /// The log.
        /// </summary>
        /// <param name="expected">
        /// The expected.
        /// </param>
        /// <typeparam name="T">Any type.</typeparam>
        private void Log<T>(T expected)
        {
            Console.WriteLine("expected:" + expected);
            System.Diagnostics.Debug.WriteLine("expected:" + expected);

            Console.WriteLine("PreviousResponseFromMyServer:" + PreviousResponseFromMyServer);
            System.Diagnostics.Debug.WriteLine("PreviousResponseFromMyServer:" + PreviousResponseFromMyServer);

            Console.WriteLine("ResponseFromMyServer:" + ResponseFromMyServer);
            System.Diagnostics.Debug.WriteLine("ResponseFromMyServer:" + ResponseFromMyServer);
        }
    }
}
