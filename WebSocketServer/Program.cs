// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="mpgp">
//   Multiplayer Game Platform
// </copyright>
// <summary>
//   Defines the Program type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WebSocketServer
{
    using System;

    using WebSocketServer.Messages;
    using WebSocketServer.Messages.Payloads;

    /// <summary>
    /// The program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        public static void Main(string[] args)
        {
#if DEBUG
            args = args.Length == 3 ? args : new[] { "localhost", "8181", "ws" };
#endif
            try
            {
                StartServer(args).ListenMessages();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        /// <summary>
        /// The start server.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        /// <returns>
        /// The <see cref="IServer"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Invalid arguments. Usage: hostname port protocol
        /// </exception>
        public static IServer StartServer(string[] args)
        {
            if (args.Length < 3 || args.Length > 3)
            {
                throw new ArgumentException("Invalid arguments. Usage: <hostname> <port> <protocol>");
            }

            var hostname = args[0];
            var port = int.Parse(args[1]);
            var protocol = args[2];

            var server = new Server(hostname, port, protocol);
            return server.Start();
        }
    }
}
