using System.Net.Sockets;

namespace BagelChat.Server
{
    public class ServerClient
    {
        public TcpClient Client { get; }
        public string Name { get; set; }

        public ServerClient(TcpClient client)
        {
            Client = client;
            Name = "guest";
        }
    }
}