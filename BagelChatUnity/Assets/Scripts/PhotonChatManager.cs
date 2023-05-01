using System;
using ExitGames.Client.Photon;
using Photon.Chat;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BagelChat
{
    public class PhotonChatManager : MonoBehaviour, IChatClientListener
    {
        [Header("Login")]
        [SerializeField] private Button _joinButton;
        [SerializeField] private InputField _usernameInput;

        [Header("Chatroom")]
        [SerializeField] private GameObject _chatPanel;
        
        [Space(10)]
        [SerializeField] private TMP_InputField _messageField;
        [SerializeField] private TMP_InputField _userField;

        [Space(10)]
        [SerializeField] private string _globalChat = "global";
        
        [Space(10)]
        [SerializeField] private TextMeshProUGUI _chatDisplay;

        private bool _isConnected = false;
        
        [SerializeField] private string _userName;
        private ChatClient _chatClient;

        private void Start()
        {
            ChatConnectOnClick();
        }

        private void Update()
        {
            if (_isConnected)
            {
                _chatClient.Service();
            }
        }
        
        public void ChatConnectOnClick()
        {
            _isConnected = true;
            _chatClient = new ChatClient(this);
            _chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, PhotonNetwork.AppVersion, new AuthenticationValues(_userName));
            
            Debug.Log("Connecting");
        }

        public void OnUsernameChanged(string username)
        {
            _userName = username;
        }
        
        public void SubmitMessage()
        {
            if (string.IsNullOrEmpty(_userField.text))
            {
                _chatClient.PublishMessage(_globalChat, _messageField.text);
            }
        }

        public void DebugReturn(DebugLevel level, string message)
        {
            Debug.Log($"{level}, {message}");
        }

        public void OnDisconnected()
        {
            _chatClient.Unsubscribe(new[] {_globalChat});
        }

        public void OnConnected()
        {
            _chatClient.Subscribe(_globalChat);
        }

        public void OnChatStateChange(ChatState state)
        {
            Debug.Log(state);
        }

        public void OnGetMessages(string channelName, string[] senders, object[] messages)
        {
            for (int i = 0; i < senders.Length; i++)
            {
                _chatDisplay.text += $"\n [{channelName}] {senders[i]}: {messages[i]}";
            }
        }

        public void OnPrivateMessage(string sender, object message, string channelName)
        {
            throw new System.NotImplementedException();
        }

        public void OnSubscribed(string[] channels, bool[] results)
        {
            _chatDisplay.text += "\n connected to new chanel ^_^";
        }

        public void OnUnsubscribed(string[] channels)
        {
            _chatDisplay.text += "\n disconnected from chanel :C";
        }

        public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
        {
            throw new System.NotImplementedException();
        }

        public void OnUserSubscribed(string channel, string user)
        {
            _chatDisplay.text += $"\n {channel}: meet new user {user} ^_^";
        }

        public void OnUserUnsubscribed(string channel, string user)
        {
            _chatDisplay.text += $"\n {channel}: say buy to user {user} :C";
        }
    }
}
