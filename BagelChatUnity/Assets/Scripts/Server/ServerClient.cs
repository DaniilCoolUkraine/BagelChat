using System.Net.Sockets;

namespace BagelChat.Server
{
    public class ServerClient
    {
        public ServerClient(TcpClient client)
        {
            Client = client;
            Name = "guest";
        }

        public TcpClient Client { get; private set; }
        public string Name { get; private set; }
    }
}