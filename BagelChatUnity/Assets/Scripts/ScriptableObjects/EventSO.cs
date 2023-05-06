using System;
using UnityEngine;

namespace BagelChat.ScriptableObjects
{
    [CreateAssetMenu(fileName = "EventSO", menuName = "ScriptableObjects/EventSO", order = 0)]
    public class EventSO : ScriptableObject
    {
        public event Action OnInvoked;

        public void Invoke()
        {
            OnInvoked?.Invoke();
        }
    }
}