namespace WebSocketServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new Server("localhost", 8181);
            server.Start();
        }
    }
}
