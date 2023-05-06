using BagelChat.ScriptableObjects;
using UnityEngine;

namespace BagelChat.UI
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private ScreenController[] _controllers;
        [SerializeField] private EventSO _onConnectedToServer;

        private void OnEnable()
        {
            foreach (var controller in _controllers)
            {
                _onConnectedToServer.OnInvoked += controller.SwitchScreen;
            }
        }
    }
}