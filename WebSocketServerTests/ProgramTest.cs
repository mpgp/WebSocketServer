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
    using System.Linq;
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
            var args = new string[] { };
            var ex = Assert.Throws<ArgumentException>(() => { Program.StartServer(args); });
            Assert.That(ex.Message, Is.EqualTo("Invalid arguments. Usage: <hostname> <port> <protocol>"));
        }

        /// <summary>
        /// Запуск программы с одним параметром: localhost
        /// </summary>
        [Test]
        public void StartServerLocalhostExpectArgumentException()
        {
            var args = new [] { "localhost" };
            var ex = Assert.Throws<ArgumentException>(() => { Program.StartServer(args); });
            Assert.That(ex.Message, Is.EqualTo("Invalid arguments. Usage: <hostname> <port> <protocol>"));
        }

        /// <summary>
        /// Запуск программы с двумя параметрами: localhost 8118
        /// </summary>
        [Test]
        public void StartServerLocalhost8118ExpectArgumentException()
        {
            var args = new [] { "localhost", "8118" };
            var ex = Assert.Throws<ArgumentException>(() => { Program.StartServer(args); });
            Assert.That(ex.Message, Is.EqualTo("Invalid arguments. Usage: <hostname> <port> <protocol>"));
        }

        /// <summary>
        /// Запуск программы с тремя параметрами: localhost 8118 ws
        /// </summary>
        [Test]
        public void StartServerLocalhost8118WsExpectNoThrows()
        {
            var args = new [] { "localhost", "8118", "ws" };
            Assert.That(() => Program.StartServer(args), Throws.Nothing);
        }

        /// <summary>
        /// Запуск программы с тремя параметрами: localhost NaN ws
        /// </summary>
        [Test]
        public void StartServerLocalhostNaNWsExpectFormatException()
        {
            var args = new [] { "localhost", "NaN", "ws" };
            var ex = Assert.Throws<FormatException>(() => { Program.StartServer(args); });
            
            var possibleValues = new[]
            {
                "Input string was not in a correct format.",
                "Входная строка имела неверный формат."
            };
            Assert.IsTrue(possibleValues.Contains(ex.Message));
        }
    }
}
