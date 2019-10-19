using System;

namespace DataStructures.FibonacciHeap
{
    /// <summary>
    /// Fibonacci Heap realization. Uses generic type T for data storage and TKey as a key type.
    /// </summary>
    /// <typeparam name="T">Type of the stored objects.</typeparam>
    /// <typeparam name="TKey">Type of the object key. Should implement IComparable.</typeparam>
    public interface IFibonacciHeap<T, TKey> where TKey : IComparable<TKey>
    {
        /// <summary>
        /// Removes all the elements from the heap.
        /// </summary>
        void Clear();

        /// <summary>
        /// Decreases the key of a node.
        /// O(1) amortized.
        /// </summary>
        void DecreaseKey(FibonacciHeapNode<T, TKey> x, TKey k);

        /// <summary>
        /// Deletes a node from the heap.
        /// O(log n)
        /// </summary>
        void Delete(FibonacciHeapNode<T, TKey> x);

        /// <summary>
        /// Inserts a new node with its key.
        /// O(1)
        /// </summary>
        void Insert(FibonacciHeapNode<T, TKey> node);

        /// <summary>
        /// Identifies whatever heap is empty.
        /// </summary>
        /// <returns>true if heap is empty - contains no elements.</returns>
        bool IsEmpty();

        /// <summary>
        /// Returns the smalles node of the heap.
        /// O(1)
        /// </summary>
        /// <returns></returns>
        FibonacciHeapNode<T, TKey> Min();

        /// <summary>
        /// Removes the smalles node of the heap.
        /// O(log n) amortized
        /// </summary>
        /// <returns></returns>
        FibonacciHeapNode<T, TKey> RemoveMin();

        /// <summary>
        /// The number of nodes. O(1)
        /// </summary>
        /// <returns></returns>
        int Size();
    }
}