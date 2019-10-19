using System;
using System.Collections.Generic;

namespace DataStructures.FibonacciHeap
{
    public class FibonacciHeap<T, TKey> : IFibonacciHeap<T, TKey> where TKey : IComparable<TKey>
    {
        private readonly double OneOverLogPhi = 1.0 / Math.Log((1.0 + Math.Sqrt(5.0)) / 2.0);

        /// <summary>
        /// Minimum (starting) node of the heap.
        /// </summary>
        private FibonacciHeapNode<T, TKey> minNode;

        /// <summary>
        /// The nodes quantity.
        /// </summary>
        private int nodesCount;
        private readonly TKey minKeyValue;

        /// <summary>
        /// Initializes the new instance of the Heap.
        /// </summary>
        /// <param name="minKeyValue">Minimum value of the key - to be used for comparing.</param>
        public FibonacciHeap(TKey minKeyValue)
        {
            this.minKeyValue = minKeyValue;
        }

        public bool IsEmpty()
        {
            return minNode == null;
        }

        public void Clear()
        {
            minNode = null;
            nodesCount = 0;
        }

        public void DecreaseKey(FibonacciHeapNode<T, TKey> x, TKey k)
        {
            if (k.CompareTo(x.Key) > 0)
            {
                throw new ArgumentException("decreaseKey() got larger key value");
            }

            x.Key = k;

            FibonacciHeapNode<T, TKey> y = x.Parent;

            if ((y != null) && (x.Key.CompareTo(y.Key) < 0))
            {
                Cut(x, y);
                CascadingCut(y);
            }

            if (x.Key.CompareTo(minNode.Key) < 0)
            {
                minNode = x;
            }
        }

        public void Delete(FibonacciHeapNode<T, TKey> x)
        {
            // make newParent as small as possible
            DecreaseKey(x, minKeyValue);

            // remove the smallest, which decreases n also
            RemoveMin();
        }

        public void Insert(FibonacciHeapNode<T, TKey> node)
        {
            // concatenate node into min list
            if (minNode != null)
            {
                node.Left = minNode;
                node.Right = minNode.Right;
                minNode.Right = node;
                node.Right.Left = node;

                if (node.Key.CompareTo(minNode.Key) < 0)
                {
                    minNode = node;
                }
            }
            else
            {
                minNode = node;
            }

            nodesCount++;
        }

        public FibonacciHeapNode<T, TKey> Min()
        {
            return minNode;
        }

        public FibonacciHeapNode<T, TKey> RemoveMin()
        {
            FibonacciHeapNode<T, TKey> minNode = this.minNode;

            if (minNode != null)
            {
                int numKids = minNode.Degree;
                FibonacciHeapNode<T, TKey> oldMinChild = minNode.Child;

                // for each child of minNode do...
                while (numKids > 0)
                {
                    FibonacciHeapNode<T, TKey> tempRight = oldMinChild.Right;

                    // remove oldMinChild from child list
                    oldMinChild.Left.Right = oldMinChild.Right;
                    oldMinChild.Right.Left = oldMinChild.Left;

                    // add oldMinChild to root list of heap
                    oldMinChild.Left = this.minNode;
                    oldMinChild.Right = this.minNode.Right;
                    this.minNode.Right = oldMinChild;
                    oldMinChild.Right.Left = oldMinChild;

                    // set parent[oldMinChild] to null
                    oldMinChild.Parent = null;
                    oldMinChild = tempRight;
                    numKids--;
                }

                // remove minNode from root list of heap
                minNode.Left.Right = minNode.Right;
                minNode.Right.Left = minNode.Left;

                if (minNode == minNode.Right)
                {
                    this.minNode = null;
                }
                else
                {
                    this.minNode = minNode.Right;
                    Consolidate();
                }

                // decrement size of heap
                nodesCount--;
            }

            return minNode;
        }

        public int Size()
        {
            return nodesCount;
        }

        /// <summary>
        /// Joins two heaps. O(1)
        /// </summary>
        /// <param name="h1">Heap one</param>
        /// <param name="h2">Heap two</param>
        /// <returns></returns>
        public static FibonacciHeap<T, TKey> Union(FibonacciHeap<T, TKey> h1, FibonacciHeap<T, TKey> h2)
        {
            var h = new FibonacciHeap<T, TKey>(
                h1.minKeyValue.CompareTo(h2.minKeyValue) < 0
                ? h1.minKeyValue
                : h2.minKeyValue)
            {
                minNode = h1.minNode
            };

            if (h.minNode != null)
            {
                if (h2.minNode != null)
                {
                    h.minNode.Right.Left = h2.minNode.Left;
                    h2.minNode.Left.Right = h.minNode.Right;
                    h.minNode.Right = h2.minNode;
                    h2.minNode.Left = h.minNode;

                    if (h2.minNode.Key.CompareTo(h1.minNode.Key) < 0)
                    {
                        h.minNode = h2.minNode;
                    }
                }
            }
            else
            {
                h.minNode = h2.minNode;
            }

            h.nodesCount = h1.nodesCount + h2.nodesCount;


            return h;
        }

        /// <summary>
        /// Performs a cascading cut operation. This cuts newChild from its parent and then
        /// does the same for its parent, and so on up the tree.
        /// </summary>
        protected void CascadingCut(FibonacciHeapNode<T, TKey> y)
        {
            FibonacciHeapNode<T, TKey> z = y.Parent;

            // if there's a parent...
            if (z == null) return;

            // if newChild is unmarked, set it marked
            if (!y.Mark)
            {
                y.Mark = true;
            }
            else
            {
                // it's marked, cut it from parent
                Cut(y, z);

                // cut its parent as well
                CascadingCut(z);
            }
        }

        protected void Consolidate()
        {
            int arraySize = ((int)Math.Floor(Math.Log(nodesCount) * OneOverLogPhi)) + 1;

            var array = new List<FibonacciHeapNode<T, TKey>>(arraySize);

            // Initialize degree array
            for (var i = 0; i < arraySize; i++)
            {
                array.Add(null);
            }

            // Find the number of root nodes.
            var numRoots = 0;
            FibonacciHeapNode<T, TKey> x = minNode;

            if (x != null)
            {
                numRoots++;
                x = x.Right;

                while (x != minNode)
                {
                    numRoots++;
                    x = x.Right;
                }
            }

            // For each node in root list do...
            while (numRoots > 0)
            {
                // Access this node's degree..
                var d = x.Degree;
                FibonacciHeapNode<T, TKey> next = x.Right;

                // ..and see if there's another of the same degree.
                for (; ; )
                {
                    FibonacciHeapNode<T, TKey> y = array[d];
                    if (y == null)
                    {
                        // Nope.
                        break;
                    }

                    // There is, make one of the nodes a child of the other.
                    // Do this based on the key value.
                    if (x.Key.CompareTo(y.Key) > 0)
                    {
                        FibonacciHeapNode<T, TKey> temp = y;
                        y = x;
                        x = temp;
                    }

                    // FibonacciHeapNode<T> newChild disappears from root list.
                    Link(y, x);

                    // We've handled this degree, go to next one.
                    array[d] = null;
                    d++;
                }

                // Save this node for later when we might encounter another
                // of the same degree.
                array[d] = x;

                // Move forward through list.
                x = next;
                numRoots--;
            }

            // Set min to null (effectively losing the root list) and
            // reconstruct the root list from the array entries in array[].
            minNode = null;

            for (var i = 0; i < arraySize; i++)
            {
                FibonacciHeapNode<T, TKey> y = array[i];
                if (y == null)
                {
                    continue;
                }

                // We've got a live one, add it to root list.
                if (minNode != null)
                {
                    // First remove node from root list.
                    y.Left.Right = y.Right;
                    y.Right.Left = y.Left;

                    // Now add to root list, again.
                    y.Left = minNode;
                    y.Right = minNode.Right;
                    minNode.Right = y;
                    y.Right.Left = y;

                    // Check if this is a new min.
                    if (y.Key.CompareTo(minNode.Key) < 0)
                    {
                        minNode = y;
                    }
                }
                else
                {
                    minNode = y;
                }
            }
        }

        /// <summary>
        /// The reverse of the link operation: removes newParent from the child list of newChild.
        /// This method assumes that min is non-null.
        /// Running time: O(1)
        /// </summary>
        protected void Cut(FibonacciHeapNode<T, TKey> x, FibonacciHeapNode<T, TKey> y)
        {
            // remove newParent from childlist of newChild and decrement degree[newChild]
            x.Left.Right = x.Right;
            x.Right.Left = x.Left;
            y.Degree--;

            // reset newChild.child if necessary
            if (y.Child == x)
            {
                y.Child = x.Right;
            }

            if (y.Degree == 0)
            {
                y.Child = null;
            }

            // add newParent to root list of heap
            x.Left = minNode;
            x.Right = minNode.Right;
            minNode.Right = x;
            x.Right.Left = x;

            // set parent[newParent] to null
            x.Parent = null;

            // set mark[newParent] to false
            x.Mark = false;
        }

        /// <summary>
        /// Makes newChild a child of Node newParent.
        /// O(1)
        /// </summary>
        protected void Link(FibonacciHeapNode<T, TKey> newChild, FibonacciHeapNode<T, TKey> newParent)
        {
            // remove newChild from root list of heap
            newChild.Left.Right = newChild.Right;
            newChild.Right.Left = newChild.Left;

            // make newChild a child of newParent
            newChild.Parent = newParent;

            if (newParent.Child == null)
            {
                newParent.Child = newChild;
                newChild.Right = newChild;
                newChild.Left = newChild;
            }
            else
            {
                newChild.Left = newParent.Child;
                newChild.Right = newParent.Child.Right;
                newParent.Child.Right = newChild;
                newChild.Right.Left = newChild;
            }

            // increase degree[newParent]
            newParent.Degree++;

            // set mark[newChild] false
            newChild.Mark = false;
        }
    }
}