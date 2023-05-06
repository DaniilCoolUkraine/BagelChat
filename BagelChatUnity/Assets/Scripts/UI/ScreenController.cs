using UnityEngine;

namespace BagelChat.UI
{
    public class ScreenController : MonoBehaviour
    {
        [SerializeField] private GameObject _screen;

        public void SwitchScreen()
        {
            _screen.SetActive(!_screen.activeSelf);
        }
    }
}