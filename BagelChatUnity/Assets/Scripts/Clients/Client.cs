using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using BagelChat.ScriptableObjects;
using UnityEngine;

namespace BagelChat.Clients
{
    public class Client : MonoBehaviour
    {
        [SerializeField] private EventSO _onConnectedToServer;
        
        private bool _isConnected = false;

        private TcpClient _socket;

        private NetworkStream _stream;
        private StreamReader _reader;
        private StreamWriter _writer;

        private string _host = "127.0.0.1";
        private int _port = 6321;

        private StringBuilder _stringBuilder;
        private string _data;
        
        private void Awake()
        {
            _stringBuilder = new StringBuilder();
        }

        private void Update()
        {
            if (!_isConnected)
                return;

            if (_stream.DataAvailable)
            {
                _stringBuilder.Append(_reader.ReadLine());
                _data = _stringBuilder.ToString();

                if (_data != null)
                    OnIncomingData(_data);
            }
        }

        public void ConnectToServer()
        {
            if (_isConnected)
                return;

            try
            {
                _socket = new TcpClient(_host, _port);

                _stream = _socket.GetStream();

                _reader = new StreamReader(_stream);
                _writer = new StreamWriter(_stream);

                _isConnected = true;
                
                _onConnectedToServer.Invoke();
            }
            catch (Exception e)
            {
                Debug.Log("Socket error " + e.Message);
            }
        }

        public void SetHost(string host) => _host = host;

        public void SetPort(string port) => int.TryParse(port, out _port);

        private void OnIncomingData(string data)
        {
            Debug.Log(data);
        }
    }
}