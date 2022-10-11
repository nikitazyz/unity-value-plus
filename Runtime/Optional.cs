using System;
using UnityEngine;

namespace ValuePlus
{
    [Serializable]
    public class Optional<T>
    {
        [SerializeField] private bool _isEnabled;
        [SerializeField] private T _value;

        public bool isEnabled
        {
            get => _isEnabled;
            set => _isEnabled = value;
        }

        public T value
        {
            get => _value;
            set => _value = value;
        }

        public static implicit operator Optional<T>(T value) => new() { value = value };
    }
}