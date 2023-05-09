using UnityEngine;

namespace BagelChat.Clients
{
    public abstract class Controllable : MonoBehaviour, IControllable
    {
        public abstract void DoAction();
    }
}