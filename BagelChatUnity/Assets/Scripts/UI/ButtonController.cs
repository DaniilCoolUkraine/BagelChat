using BagelChat.Clients;
using UnityEngine;
using UnityEngine.UI;

namespace BagelChat.UI
{
    public class ButtonController : MonoBehaviour
    {
        [SerializeField] private Controllable _controllable;
        [SerializeField] private Button _button;

        private void OnEnable()
        {
            _button.onClick.AddListener(_controllable.DoAction);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(_controllable.DoAction);
        }
    }
}