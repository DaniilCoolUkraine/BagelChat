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
        
        private void OnEnable()
        {
            foreach (var controller in _controllers)
            {
                _onConnectedToServer.OnInvoked += controller.SwitchScreen;
            }
            
            _hostInput.onEndEdit.AddListener(_client.SetHost);
            _portInput.onEndEdit.AddListener(_client.SetPort);
        }
        
        private void OnDisable()
        {
            foreach (var controller in _controllers)
            {
                _onConnectedToServer.OnInvoked -= controller.SwitchScreen;
            }
            
            _hostInput.onEndEdit.RemoveListener(_client.SetHost);
            _portInput.onEndEdit.RemoveListener(_client.SetPort);
        }
    }
}