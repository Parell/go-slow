using UnityEngine;

namespace Unity_Utils
{
    public class ValueFrameCache<T>
    {
        private T value;
        private int frame;

        public bool isValid() {
            return this.value != null && Time.frameCount == this.frame;
        }

        public void setValue(T value) {
            this.frame = Time.frameCount;
            this.value = value;
        }

        public T getValue() {
            return this.value;
        }
    }
}