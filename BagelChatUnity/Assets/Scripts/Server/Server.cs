using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using BagelChat.ScriptableObjects;
using UnityEngine;

namespace BagelChat.Server
{
    public class Server : MonoBehaviour
    {
        [SerializeField] private EventSO _onServerStarted;
        
        [SerializeField] private int _port = 6321;
        public int Port => _port;

        private List<ServerClient> _clients;
        private List<ServerClient> _disconnectClients;

        private TcpListener _server;
        private bool _isStarted;

        private StringBuilder _stringBuilder;
        private string _data;
        
        private NetworkStream _stream;

        private void Start()
        {
            _clients = new List<ServerClient>();
            _disconnectClients = new List<ServerClient>();

            try
            {
                _server = new TcpListener(IPAddress.Any, _port);
                _server.Start();

                StartListening();
                
                _isStarted = true;
                Debug.Log("server has been started " + _port);
                //_serverStarted.Invoke();
                
                _stringBuilder = new StringBuilder();
            }
            catch (Exception e)
            {
                Debug.Log("Socket error " + e.Message);
            }
        }

        private void Update()
        {
            if (!_isStarted)
                return;

            foreach (ServerClient client in _clients)
            {
                if (!ClientIsConnected(client.Client))
                {
                    client.Client.Close();
                    _disconnectClients.Add(client);
                    continue;
                }

                _stringBuilder.Clear();
                _stream = client.Client.GetStream();
                
                if (_stream.DataAvailable)
                {
                    StreamReader reader = new StreamReader(_stream, true); 
                    
                    _stringBuilder.Append(reader.ReadLine());
                    _data = _stringBuilder.ToString();
                    
                    if (_data != null)
                    {
                        OnIncomingData(client, _data);
                    }
                }
            }
        }

        private void OnIncomingData(ServerClient client, string data)
        {
            Debug.Log($"{client.Name}: {data}");
        }

        private void StartListening()
        {
            _server.BeginAcceptTcpClient(AcceptTcpClient, _server);
        }

        private void AcceptTcpClient(IAsyncResult ar)
        {
            TcpListener listener = (TcpListener) ar.AsyncState;
            
            _clients.Add(new ServerClient(listener.EndAcceptTcpClient(ar)));
            StartListening();
        }

        private bool ClientIsConnected(TcpClient client)
        {
            try
            {
                if (client == null)
                    return false;
                if (client.Client == null)
                    return false;
                if (!client.Connected)
                    return false;

                if (client.Client.Poll(0, SelectMode.SelectRead))
                    return client.Client.Receive(new byte[1], SocketFlags.Peek) != 0;

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}