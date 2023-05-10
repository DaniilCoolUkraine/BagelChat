using System;
using System.IO;
using System.Net.Sockets;
using BagelChat.General;
using BagelChat.ScriptableObjects;
using UnityEngine;

namespace BagelChat.Clients
{
    public class Client : Controllable
    {
        [SerializeField] private StringEventSO _onMessageReceived;
        [SerializeField] private EventSO _onConnectedToServer;

        private string _clientName;

        private bool _isConnected = false;

        private TcpClient _socket;

        private NetworkStream _stream;
        private StreamReader _reader;
        private StreamWriter _writer;

        private string _host = "127.0.0.1";
        private int _port = 6321;

        private string _data;

        private void Update()
        {
            if (!_isConnected)
                return;

            if (_stream.DataAvailable)
            {
                _data = _reader.ReadLine();

                if (_data != null)
                    OnIncomingData(_data);
            }
        }

        private void OnApplicationQuit()
        {
            CloseSocket();
        }

        private void OnDisable()
        {
            CloseSocket();
        }

        public override void DoAction()
        {
            ConnectToServer();
        }

        public void SetHost(string host) => _host = host;

        public void SetPort(string port) => int.TryParse(port, out _port);

        public void SetName(string clientName) => _clientName = clientName;

        public void SendData(string data)
        {
            if (!_isConnected)
                return;

            _writer.WriteLine(data);
            _writer.Flush();
        }

        private void ConnectToServer()
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

        private void OnIncomingData(string data)
        {
            if (data.Contains(SpecialCommands.NameRequest))
            {
                SendData($"{SpecialCommands.NameResponse}{_clientName}");
                return;
            }

            try
            {
                _data = data.Split('|')[1];
            }
            catch (Exception e)
            {
                Debug.Log($"Read error: {e.Message}");
            }
            _onMessageReceived.ChangeValue(_data);
        }

        private void CloseSocket()
        {
            if (!_isConnected)
                return;

            _writer.Close();
            _reader.Close();
            _socket.Close();

            _isConnected = false;
        }
    }
}