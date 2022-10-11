using System;
using UnityEngine;

namespace ValuePlus
{
    [Serializable]
    public struct FloatRange
    {
        [SerializeField] private float _min;
        [SerializeField] private float _max;
        
        public float min
        {
            get => _min;
            set => _min = value;
        }

        public float max
        {
            get => _max;
            set => _max = value;
        }

        public FloatRange(float min, float max)
        {
            _min = min;
            _max = max;
        }

        public static implicit operator FloatRange(IntRange intRange) => new(intRange.min, intRange.max);

        public override string ToString()
        {
            return $"{{{_min}; {_max}}}";
        }

        public bool Equals(FloatRange other)
        {
            return _min.Equals(other._min) && _max.Equals(other._max);
        }

        public override bool Equals(object obj)
        {
            return obj is FloatRange other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_min, _max);
        }
    }
}