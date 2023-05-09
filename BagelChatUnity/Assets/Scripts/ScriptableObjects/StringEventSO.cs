using System;
using UnityEngine;

namespace BagelChat.ScriptableObjects
{
    [CreateAssetMenu(fileName = "StringEventSO", menuName = "ScriptableObjects/StringEventSO", order = 0)]
    public class StringEventSO : ScriptableObject
    {
        public event Action<string> OnValueChanged;

        public void ChangeValue(string value)
        {
            OnValueChanged?.Invoke(value);
        }
    }
}