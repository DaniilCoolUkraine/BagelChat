﻿using BagelChat.ScriptableObjects;
using TMPro;
using UnityEngine;

namespace BagelChat.Clients
{
    public class ClientMessageShower : MonoBehaviour
    {
        [SerializeField] private StringEventSO _onMessageReceived;

        [SerializeField] private GameObject _chatWindow;
        [SerializeField] private GameObject _messagePrefab;

        private GameObject _lastMessage;

        private void OnEnable()
        {
            _onMessageReceived.OnValueChanged += ShowMessage;
        }

        private void OnDisable()
        {
            _onMessageReceived.OnValueChanged -= ShowMessage;
        }

        private void ShowMessage(string message)
        {
            _lastMessage = Instantiate(_messagePrefab, _chatWindow.transform);
            _lastMessage.GetComponentInChildren<TextMeshProUGUI>().text = message;
        }
    }
}