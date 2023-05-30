using BagelChat.General;
using BagelChat.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BagelChat.UI
{
    public class ClientsListShower : MonoBehaviour
    {
        [SerializeField] private StringEventSO _onClientsListReceived;
        
        [SerializeField] private GameObject _clientsWindow;
        [SerializeField] private GameObject _clientPrefab;
        
        [SerializeField] private TMP_InputField _messageInput;

        private GameObject _lastClient;
        private string[] _clientsNickNames;
        
        private void OnEnable()
        {
            _onClientsListReceived.OnValueChanged += UpdateClientsList;
        }

        private void OnDisable()
        {
            _onClientsListReceived.OnValueChanged -= UpdateClientsList;
        }

        private void UpdateClientsList(string clientsNickNames)
        {
            _clientsNickNames = clientsNickNames.Split(' ');

            foreach (Transform child in _clientsWindow.transform)
            {
                Destroy(child.gameObject);
            }
            
            foreach (string nickName in _clientsNickNames)
            {
                if (string.IsNullOrEmpty(nickName))
                    continue;
                
                _lastClient = Instantiate(_clientPrefab, _clientsWindow.transform);
                
                TextMeshProUGUI text = _lastClient.GetComponentInChildren<TextMeshProUGUI>();
                text.ClearMesh();
                text.text = nickName;

                Button button = _lastClient.GetComponent<Button>();
                
                button.onClick.AddListener((() =>
                {
                    _messageInput.text = $"{SpecialCommands.PrivateTag}{nickName}|";
                }));
            }
        }
    }
}