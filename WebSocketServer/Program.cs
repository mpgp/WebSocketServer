using System;

namespace WebSocketServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                StartServer(args).ListenMessages();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static IServer StartServer(string[] args)
        {
            if (args.Length < 3 || args.Length > 3)
            {
                throw new ArgumentException("Invalid arguments. Usage: <hostname> <port> <protocol>");
            }

            string hostname = args[0];
            int port = int.Parse(args[1]);
            string protocol = args[2];

            var server = new Server(hostname, port, protocol);
            return server.Start();
        }
    }
}
