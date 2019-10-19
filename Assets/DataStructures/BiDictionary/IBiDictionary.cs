using System.Collections.Generic;

namespace DataStructures.BiDictionary
{
    /// <summary>
    /// A data type keeps key -> value and value -> key mappings and maintains them.
    /// </summary>
    /// <typeparam name="TKey">type of the key</typeparam>
    /// <typeparam name="TValue">type of the value</typeparam>
    public interface IBiDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        /// <summary>
        /// Get access to the key map
        /// </summary>
        Dictionary<TKey, TValue> KeyMap { get; }

        /// <summary>
        /// Get access to the value map
        /// </summary>
        Dictionary<TValue, TKey> ValueMap { get; }

        /// <summary>
        /// Adds a key value pair
        /// </summary>
        /// <param name="key">key to add</param>
        /// <param name="value">value to add</param>
        void Add(TKey key, TValue value);

        /// <summary>
        /// Removes the key and its correspondent value
        /// </summary>
        /// <param name="key">key to remove</param>
        /// <returns>true if the key exists and removed succesfully</returns>
        bool RemoveKey(TKey key);

        /// <summary>
        /// Removes the value and its correspondent key
        /// </summary>
        /// <param name="value">value to remove</param>
        /// <returns>true if the value exists and removed successfully</returns>
        bool RemoveValue(TValue value);

        /// <summary>
        /// Clears out all the elements
        /// </summary>
        void Clear();

        /// <summary>
        /// Checks if the key exists
        /// </summary>
        /// <param name="key">key to check</param>
        /// <returns>true if the key exists</returns>
        bool ContainsKey(TKey key);

        /// <summary>
        /// Checks if the value exists
        /// </summary>
        /// <param name="value">value to check</param>
        /// <returns>true if the value exists</returns>
        bool ContainsValue(TValue value);

        /// <summary>
        /// Gives the count of elements in the Bidictionary
        /// Remark: it counts the key -> value and value -> key elements as 1
        /// </summary>
        /// <returns>Number of elements in the dictionary</returns>
        int Count();

        /// <summary>
        /// Gets the enumerator for keys
        /// </summary>
        /// <returns>enumerator for key to values</returns>
        new IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator();

        /// <summary>
        /// Gets the enumerator for values
        /// </summary>
        /// <returns>enumerator for value to keys</returns>
        IEnumerator<KeyValuePair<TValue, TKey>> GetValueEnumerator();
    }
}