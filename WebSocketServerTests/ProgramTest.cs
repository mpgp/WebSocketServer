using NUnit.Framework;
using System;

namespace WebSocketServer.Tests
{
    [TestFixture]
    public class ProgramTest
    {
        [Test]
        public void PassTestMethod()
        {
            Assert.Pass("Your first passing test");
        }

        /// <summary>
        /// Запуск программы без параметров
        /// </summary>
        [Test]
        public void StartServer_WithoutParams_expectArgumentException()
        {
            string[] args = new string[] { };
            var ex = Assert.Throws<ArgumentException>(() => { Program.StartServer(args); });
            Assert.That(ex.Message, Is.EqualTo("Invalid arguments. Usage: <hostname> <port> <protocol>"));
        }

        /// <summary>
        /// Запуск программы с одним параметром: localhost
        /// </summary>
        [Test]
        public void StartServer_localhost_expectArgumentException()
        {
            string[] args = new string[] { "localhost" };
            var ex = Assert.Throws<ArgumentException>(() => { Program.StartServer(args); });
            Assert.That(ex.Message, Is.EqualTo("Invalid arguments. Usage: <hostname> <port> <protocol>"));
        }

        /// <summary>
        /// Запуск программы с двумя параметрами: localhost 8118
        /// </summary>
        [Test]
        public void StartServer_localhost__8181_expectArgumentException()
        {
            string[] args = new string[] { "localhost", "8181" };
            var ex = Assert.Throws<ArgumentException>(() => { Program.StartServer(args); });
            Assert.That(ex.Message, Is.EqualTo("Invalid arguments. Usage: <hostname> <port> <protocol>"));
        }

        /// <summary>
        /// Запуск программы с тремя параметрами: localhost 8118 ws
        /// </summary>
        [Test]
        public void StartServer_localhost__8181__ws_expectNoThrows()
        {
            string[] args = new string[] { "localhost", "8181", "ws" };
            Assert.That(() => Program.StartServer(args), Throws.Nothing);
        }

        /// <summary>
        /// Запуск программы с тремя параметрами: localhost NaN ws
        /// </summary>
        [Test]
        public void StartServer_localhost__NaN__ws_expectFormatException()
        {
            string[] args = new string[] { "localhost", "NaN", "8181" };
            var ex = Assert.Throws<FormatException>(() => { Program.StartServer(args); });
            Assert.That(ex.Message, Is.EqualTo("Входная строка имела неверный формат."));
        }
    }
}
