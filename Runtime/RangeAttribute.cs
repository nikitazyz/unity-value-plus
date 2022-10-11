using UnityEngine;

namespace ValuePlus
{
    public class RangeAttribute : PropertyAttribute
    {
        public float min;
        public float max;
        
#if UNITY_EDITOR
        public bool isOpened;
#endif

        public RangeAttribute(float min, float max)
        {
            this.min = min;
            this.max = max;
        }
    }
}