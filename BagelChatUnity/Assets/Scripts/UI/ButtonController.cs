using BagelChat.Clients;
using UnityEngine;
using UnityEngine.UI;

namespace BagelChat.UI
{
    public class ButtonController : MonoBehaviour
    {
        [SerializeField] private Client _client;
        [SerializeField] private Button _button;

        private void OnEnable()
        {
            _button.onClick.AddListener(_client.ConnectToServer);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(_client.ConnectToServer);
        }
    }
}