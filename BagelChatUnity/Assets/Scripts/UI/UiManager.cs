using BagelChat.Clients;
using BagelChat.ScriptableObjects;
using TMPro;
using UnityEngine;

namespace BagelChat.UI
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private EventSO _onConnectedToServer;
        [SerializeField] private Client _client;

        [SerializeField] private ScreenController[] _controllers;

        [SerializeField] private TMP_InputField _hostInput;
        [SerializeField] private TMP_InputField _portInput;
        [SerializeField] private TMP_InputField _nameInput;

        [SerializeField] private TMP_InputField _messageInput;

        private void OnEnable()
        {
            foreach (var controller in _controllers)
            {
                _onConnectedToServer.OnInvoked += controller.SwitchScreen;
            }

            _hostInput.onEndEdit.AddListener(_client.SetHost);
            _portInput.onEndEdit.AddListener(_client.SetPort);
            _nameInput.onEndEdit.AddListener(_client.SetName);

            _messageInput.onEndEdit.AddListener(_client.SendData);
        }

        private void OnDisable()
        {
            foreach (var controller in _controllers)
            {
                _onConnectedToServer.OnInvoked -= controller.SwitchScreen;
            }

            _hostInput.onEndEdit.RemoveListener(_client.SetHost);
            _portInput.onEndEdit.RemoveListener(_client.SetPort);
            _nameInput.onEndEdit.RemoveListener(_client.SetName);

            _messageInput.onEndEdit.RemoveListener(_client.SendData);
        }
    }
}