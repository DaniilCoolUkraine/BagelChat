using System.IO;
using System.Net.Sockets;
using BagelChat.ScriptableObjects;
using UnityEngine;

namespace BagelChat.Clients
{
    public class Client : MonoBehaviour
    {
        [SerializeField] private EventSO _onConnectedToServer;
        
        private bool _socketIsReady = false;

        private TcpClient _socket;

        private NetworkStream _stream;
        private StreamReader _reader;
        private StreamWriter _writer;

        private string _host = "127.0.0.1";
        private string _port = "6321";

        public void ConnectToServer()
        {
            if (_socketIsReady)
                return;

            _onConnectedToServer.Invoke();
        }

        public void SetHost(string host)
        {
            _host = host;
            Debug.Log(host);
        }
        
        public void SetPort(string port)
        {
            _port = port;
            Debug.Log(port);
        }
    }
}