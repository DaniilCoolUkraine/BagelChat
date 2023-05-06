using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

namespace BagelChat.Server
{
    public class Server : MonoBehaviour
    {
        [SerializeField] private int _port = 6321;
        public int Port => _port;

        private List<ServerClient> _clients;
        private List<ServerClient> _disconnectClients;

        private TcpListener _server;
        private bool _isStarted;

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
            }
            catch (Exception e)
            {
                Debug.Log("Socket error " + e.Message);
            }
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
    }
}