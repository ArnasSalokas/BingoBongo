using System;
using System.Collections.Generic;

namespace Template.Common.Comparers
{
    public class CustomEqualityComparer<T> : IEqualityComparer<T>
    {
        private Func<T, object> KeySelector { get; set; }

        public CustomEqualityComparer(Func<T, object> keySelector)
        {
            KeySelector = keySelector;
        }

        public bool Equals(T x, T y)
        {
            return KeySelector(x).Equals(KeySelector(y));
        }

        public int GetHashCode(T obj)
        {
            return KeySelector(obj).GetHashCode();
        }
    }
}
