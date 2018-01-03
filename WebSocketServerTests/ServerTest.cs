// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServerTest.cs" company="mpgp">
//   Multiplayer Game Platform
// </copyright>
// <summary>
//   Defines the ServerTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WebSocketServerTests
{
    using System;
    using System.Linq;
    using System.Threading;
    using NUnit.Framework;
    using WebSocketServer;
    using WebSocketServer.Messages;

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

            this.MyClient = new WebSocket4Net.WebSocket($"{Protocol}://{Hostname}:{Port}");
            this.MyClient.MessageReceived += this.OnReceiveMessageFromServer;
            this.MyClient.Open();
            Thread.Sleep(3000);
        }

        /// <summary>
        /// The run before each.
        /// </summary>
        [SetUp]
        public void RunBeforeEach()
        {
            this.PreviousResponseFromMyServer = null;
            this.ResponseFromMyServer = null;
        }

        /// <summary>
        /// Тестирование авторизации на успех
        /// </summary>
        [Test, Order(1)]
        public void TestAuthorizationSuccess()
        {
            this.MyClient.Send(Helper.Serialize(Helper.BuildMessage(new AuthMessage() { UserName = "admin2018" })));

            for (var i = 0; i < 50 && this.ResponseFromMyServer == null; ++i)
            {
                Thread.Sleep(100);
            }

            var expected = Helper.Serialize(
                Helper.BuildMessage(
                    new AuthMessage()
                        {
                            UserName = "admin2018",
                            Status = AuthMessage.StatusCode.Success
                        }));
            this.Log(expected);
            var possibleValues = new[] { this.PreviousResponseFromMyServer, this.ResponseFromMyServer };
            Assert.IsTrue(possibleValues.Contains(expected));
        }

        /// <summary>
        /// Тестирование авторизации другого клиента под уже существующим ником
        /// </summary>
        [Test, Order(2)]
        public void TestAuthorizationError()
        {
            var anotherClient = new WebSocket4Net.WebSocket($"{Protocol}://{Hostname}:{Port}");
            anotherClient.MessageReceived += this.OnReceiveMessageFromServer;
            anotherClient.Open();
            Thread.Sleep(3000);

            anotherClient.Send(
                Helper.Serialize(
                    Helper.BuildMessage(
                        new AuthMessage()
                        {
                            UserName = "admin2018"
                        })));

            for (var i = 0; i < 50 && this.ResponseFromMyServer == null; ++i)
            {
                Thread.Sleep(100);
            }

            var expected = Helper.Serialize(
                Helper.BuildMessage(
                    new AuthMessage()
                        {
                            Message = "Error: the user name <admin2018> is already in use!",
                            UserName = "admin2018",
                            Status = AuthMessage.StatusCode.Error
                        }));
            this.Log(expected);
            Assert.AreEqual(expected, this.ResponseFromMyServer);
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
            this.PreviousResponseFromMyServer = this.ResponseFromMyServer;
            this.ResponseFromMyServer = args.Message;
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

            Console.WriteLine("PreviousResponseFromMyServer:" + this.PreviousResponseFromMyServer);
            System.Diagnostics.Debug.WriteLine("PreviousResponseFromMyServer:" + this.PreviousResponseFromMyServer);

            Console.WriteLine("ResponseFromMyServer:" + this.ResponseFromMyServer);
            System.Diagnostics.Debug.WriteLine("ResponseFromMyServer:" + this.ResponseFromMyServer);
        }
    }
}
