using System;
using ExitGames.Client.Photon;
using Photon.Chat;
using Photon.Pun;
using UnityEngine;

namespace BagelChat
{
    public class PhotonChatManager : MonoBehaviour, IChatClientListener
    {
        [SerializeField] private string _userId;
        
        private ChatClient _chatClient;

        private void Start()
        {
            _chatClient = new ChatClient(this);
            _chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, PhotonNetwork.AppVersion,
                new AuthenticationValues(_userId));
        }

        private void Update()
        {
            _chatClient.Service();
        }

        public void DebugReturn(DebugLevel level, string message)
        {
            throw new System.NotImplementedException();
        }

        public void OnDisconnected()
        {
            throw new System.NotImplementedException();
        }

        public void OnConnected()
        {
            throw new System.NotImplementedException();
        }

        public void OnChatStateChange(ChatState state)
        {
            throw new System.NotImplementedException();
        }

        public void OnGetMessages(string channelName, string[] senders, object[] messages)
        {
            throw new System.NotImplementedException();
        }

        public void OnPrivateMessage(string sender, object message, string channelName)
        {
            throw new System.NotImplementedException();
        }

        public void OnSubscribed(string[] channels, bool[] results)
        {
            throw new System.NotImplementedException();
        }

        public void OnUnsubscribed(string[] channels)
        {
            throw new System.NotImplementedException();
        }

        public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
        {
            throw new System.NotImplementedException();
        }

        public void OnUserSubscribed(string channel, string user)
        {
            throw new System.NotImplementedException();
        }

        public void OnUserUnsubscribed(string channel, string user)
        {
            throw new System.NotImplementedException();
        }
    }
}
