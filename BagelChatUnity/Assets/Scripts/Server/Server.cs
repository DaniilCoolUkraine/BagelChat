using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using BagelChat.General;
using BagelChat.ScriptableObjects;
using UnityEngine;

namespace BagelChat.Server
{
    public class Server : MonoBehaviour
    {
        [SerializeField] private EventSO _onServerStarted;
        
        [SerializeField] private int _port = 6321;

        private List<ServerClient> _clients;
        private List<ServerClient> _disconnectClients;

        private TcpListener _server;
        private bool _isStarted;

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
                _onServerStarted.Invoke();
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
                
                _stream = client.Client.GetStream();
                
                if (_stream.DataAvailable)
                {
                    StreamReader reader = new StreamReader(_stream, true);

                    _data = reader.ReadLine();
                    
                    if (_data != null)
                    {
                        OnIncomingData(client, _data);
                    }
                }
            }
        }

        private void OnIncomingData(ServerClient client, string data)
        {
            if (data.Contains(SpecialCommands.NameResponse))
            {
                client.Name = data.Split(':')[1];
                BroadCast($"{client.Name} has connected", _clients);
                return;
            }
            
            BroadCast($"{client.Name}: {data}", _clients);
        }

        private void BroadCast(string data, List<ServerClient> clients)
        {
            foreach (ServerClient client in clients)
            {
                try
                {
                    StreamWriter writer = new StreamWriter(client.Client.GetStream());
                    writer.WriteLine(data);
                    writer.Flush();
                }
                catch (Exception e)
                {
                    Debug.Log($"Write error: {e.Message}; to client {client.Name}");
                    throw;  
                }
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

            BroadCast(SpecialCommands.NameRequest, new List<ServerClient> {_clients[^1]});

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