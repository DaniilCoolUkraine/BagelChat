using System.IO;
using System.Net.Sockets;
using UnityEngine;

namespace BagelChat.Clients
{
    public class Client : MonoBehaviour
    {
        private bool _socketIsReady = false;

        private TcpClient _socket;

        private NetworkStream _stream;
        private StreamReader _reader;
        private StreamWriter _writer;

        public void ConnectToServer()
        {
            
        }
    }
}