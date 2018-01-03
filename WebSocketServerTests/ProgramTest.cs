// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProgramTest.cs" company="mpgp">
//   Multiplayer Game Platform
// </copyright>
// <summary>
//   Defines the ProgramTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WebSocketServerTests
{
    using System;
    using NUnit.Framework;
    using WebSocketServer;

    /// <summary>
    /// The program test.
    /// </summary>
    [TestFixture]
    public class ProgramTest
    {
        /// <summary>
        /// Запуск программы без параметров
        /// </summary>
        [Test]
        public void StartServerWithoutParamsExpectArgumentException()
        {
            string[] args = new string[] { };
            var ex = Assert.Throws<ArgumentException>(() => { Program.StartServer(args); });
            Assert.That(ex.Message, Is.EqualTo("Invalid arguments. Usage: <hostname> <port> <protocol>"));
        }

        /// <summary>
        /// Запуск программы с одним параметром: localhost
        /// </summary>
        [Test]
        public void StartServerLocalhostExpectArgumentException()
        {
            string[] args = new string[] { "localhost" };
            var ex = Assert.Throws<ArgumentException>(() => { Program.StartServer(args); });
            Assert.That(ex.Message, Is.EqualTo("Invalid arguments. Usage: <hostname> <port> <protocol>"));
        }

        /// <summary>
        /// Запуск программы с двумя параметрами: localhost 8118
        /// </summary>
        [Test]
        public void StartServerLocalhost8118ExpectArgumentException()
        {
            string[] args = new string[] { "localhost", "8118" };
            var ex = Assert.Throws<ArgumentException>(() => { Program.StartServer(args); });
            Assert.That(ex.Message, Is.EqualTo("Invalid arguments. Usage: <hostname> <port> <protocol>"));
        }

        /// <summary>
        /// Запуск программы с тремя параметрами: localhost 8118 ws
        /// </summary>
        [Test]
        public void StartServerLocalhost8118WsExpectNoThrows()
        {
            string[] args = new string[] { "localhost", "8118", "ws" };
            Assert.That(() => Program.StartServer(args), Throws.Nothing);
        }

        /// <summary>
        /// Запуск программы с тремя параметрами: localhost NaN ws
        /// </summary>
        [Test]
        public void StartServerLocalhostNaNWsExpectFormatException()
        {
            string[] args = new string[] { "localhost", "NaN", "ws" };
            var ex = Assert.Throws<FormatException>(() => { Program.StartServer(args); });
            Assert.That(ex.Message, Is.EqualTo("Входная строка имела неверный формат."));
        }
    }
}
