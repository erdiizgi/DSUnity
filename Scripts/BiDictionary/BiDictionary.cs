using System.Collections;
using System.Collections.Generic;

namespace DataStructures.BiDictionary
{
    public class BiDictionary<TKey, TValue> : IBiDictionary<TKey, TValue>
    {
        private readonly Dictionary<TKey, TValue> forward = new Dictionary<TKey, TValue>();
        private readonly Dictionary<TValue, TKey> reverse = new Dictionary<TValue, TKey>();

        public BiDictionary()
        {
            KeyMap = forward;
            ValueMap = reverse;
        }

        public Dictionary<TKey, TValue> KeyMap { get; private set; }
        public Dictionary<TValue, TKey> ValueMap { get; private set; }

        public int Count() => forward.Count;

        public void Add(TKey key, TValue value)
        {
            if (key != null && value != null)
            {
                forward.Add(key, value);
                reverse.Add(value, key);
            }
        }

        public bool ContainsKey(TKey key)
        {
            return forward.ContainsKey(key);
        }

        public bool ContainsValue(TValue value)
        {
            return reverse.ContainsKey(value);
        }

        public bool RemoveKey(TKey key)
        {
            var value = forward[key];
            return value != null && reverse.ContainsKey(value)
                && reverse.Remove(value) && forward.Remove(key);
        }

        public bool RemoveValue(TValue value)
        {
            var key = reverse[value];
            return key != null && forward.ContainsKey(key)
                && forward.Remove(key) && reverse.Remove(value);
        }

        public IEnumerator<KeyValuePair<TValue, TKey>> GetValueEnumerator()
        {
            return reverse.GetEnumerator();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return forward.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return forward.GetEnumerator();
        }

        public void Clear()
        {
            forward.Clear();
            reverse.Clear();
        }
    }
}
