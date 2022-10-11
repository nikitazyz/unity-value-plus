using System;
using UnityEngine;

namespace ValuePlus
{
    [Serializable]
    public struct IntRange
    {
        [SerializeField] private int _min;
        [SerializeField] private int _max;


        public int min
        {
            get => _min;
            set => _min = value;
        }

        public int max
        {
            get => _max;
            set => _max = value;
        }

        public IntRange(int min, int max)
        {
            _min = min;
            _max = max;
        }

        public static explicit operator IntRange(FloatRange floatRange) => new((int)floatRange.min, (int)floatRange.max);

        public override string ToString()
        {
            return $"{{{_min}, {_max}}}";
        }

        public bool Equals(IntRange other)
        {
            return _min.Equals(other._min) && _max.Equals(other._max);
        }

        public override bool Equals(object obj)
        {
            return obj is IntRange other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_min, _max);
        }
    }
}