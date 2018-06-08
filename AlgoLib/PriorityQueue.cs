using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoLib
{
    public enum PriorityOrder
    {
        Min,
        Max
    }
    /// <summary>
    /// Priority queue based on binary heap,
    /// Elements with minimum priority dequeued first
    /// </summary>
    /// <typeparam name="T">Type of values</typeparam>
    public class PriorityQueue<T> : IEnumerable<T>, ICollection, IEnumerable where T : IComparable
    {
        private readonly PriorityOrder _order;
        private readonly IList<T> _baseHeap;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of priority queue with default initial capacity and default priority comparer
        /// </summary>
        public PriorityQueue(PriorityOrder order)
        {
            _baseHeap = new List<T>();
            _order = order;
        }

        /// <summary>
        /// Initializes a new instance of priority queue with specified initial capacity and default priority comparer
        /// </summary>
        /// <param name="data">data to initialise the queue with</param>
        /// <param name="order">priority order of the queue (min or max)</param>
        public PriorityQueue(IEnumerable<T> data, PriorityOrder order)
        {
            _baseHeap = new List<T>();
            _order = order;
            foreach (var d in data)
            {
                Enqueue(d);
            }
        }

        #endregion

        #region Priority queue operations


        /// <summary>
        /// Enqueues element into priority queue
        /// </summary>
        /// <param name="value">element value</param>
        public void Enqueue(T value)
        {
            Insert(value);
        }

        /// <summary>
        /// Dequeues element with minimum priority and return its priority and value as <see cref="KeyValuePair{TPriority,TValue}"/> 
        /// </summary>
        /// <returns>value of the dequeued element</returns>
        /// <remarks>
        /// Method throws <see cref="InvalidOperationException"/> if priority queue is empty
        /// </remarks>
        public T Dequeue()
        {
            if (!IsEmpty)
            {
                T result = _baseHeap[0];
                if (_baseHeap.Count <= 1)
                {
                    _baseHeap.Clear();
                }
                else
                {
                    // copy the last item in the heap to the top
                    _baseHeap[0] = _baseHeap[_baseHeap.Count - 1];
                    // delete the last item in the heap, shrinking it
                    _baseHeap.RemoveAt(_baseHeap.Count - 1);
                    // heapify
                    BubbleDown(0);
                }
                return result;
            }
            else
                throw new InvalidOperationException("Priority queue is empty");
        }

        /// <summary>
        /// Returns priority and value of the element with minimun priority, without removing it from the queue
        /// </summary>
        /// <returns>priority and value of the element with minimum priority</returns>
        /// <remarks>
        /// Method throws <see cref="InvalidOperationException"/> if priority queue is empty
        /// </remarks>
        public T Peek()
        {
            if (!IsEmpty)
                return _baseHeap[0];
            else
                throw new InvalidOperationException("Priority queue is empty");
        }

        /// <summary>
        /// Gets whether priority queue is empty
        /// </summary>
        public bool IsEmpty
        {
            get { return _baseHeap.Count == 0; }
        }

        #endregion

        #region Heap operations

        /// <summary>
        /// Swaps 2 values in the heap using a temporary variable.
        /// </summary>
        /// <param name="pos1"></param>
        /// <param name="pos2"></param>
        private void Swap(int pos1, int pos2)
        {
            T temp = _baseHeap[pos1];
            _baseHeap[pos1] = _baseHeap[pos2];
            _baseHeap[pos2] = temp;
        }

        /// <summary>
        /// Insert a value into the heap.
        /// </summary>
        /// <param name="value"></param>
        private void Insert(T value)
        {
            _baseHeap.Add(value);
            // heap[i] has 2 children located at heap[2*i + 1] and heap[2*i + 2] 
            // and 1 parent located at heap[(i-1)/ 2];
            // heapify after insert, from end to beginning
            BubbleUp(_baseHeap.Count - 1);
        }


        private int BubbleUp(int pos)
        {
            if (pos >= _baseHeap.Count)
            {
                throw new ArgumentOutOfRangeException("pos", string.Format("pos={0}", pos));
            }

            if (pos > 0)
            {
                int parentPos = (pos - 1) >> 1;
                if (_order == PriorityOrder.Min)
                {
                    if (_baseHeap[parentPos].CompareTo(_baseHeap[pos]) > 0)
                    {
                        Swap(parentPos, pos);
                        pos = BubbleUp(parentPos);
                    }
                }
                else
                {
                    if (_baseHeap[pos].CompareTo(_baseHeap[parentPos]) > 0)
                    {
                        Swap(parentPos, pos);
                        pos = BubbleUp(parentPos);
                    }
                }
            }
            return pos;
        }

        private void BubbleDown(int pos)
        {
            if (pos >= _baseHeap.Count)
            {
                return;
            }

            // heap[i] have children heap[2*i + 1] and heap[2*i + 2] and parent heap[(i-1)/ 2];

            // on each iteration exchange element with its smallest child
            int smallest = pos;
            int left = (pos<<1) + 1;
            int right = (pos<<1) + 2;
            if (_order == PriorityOrder.Min)
            {
                if (left < _baseHeap.Count && _baseHeap[smallest].CompareTo(_baseHeap[left]) > 0)
                    smallest = left;
                if (right < _baseHeap.Count && _baseHeap[smallest].CompareTo(_baseHeap[right]) > 0)
                    smallest = right;
            }
            else
            {
                if (left < _baseHeap.Count && _baseHeap[smallest].CompareTo(_baseHeap[left]) < 0)
                    smallest = left;
                if (right < _baseHeap.Count && _baseHeap[smallest].CompareTo(_baseHeap[right]) < 0)
                    smallest = right;
            }

            if (smallest != pos)
            {
                Swap(smallest, pos);
                BubbleDown(smallest);
            }
        }

        #endregion


        /// <summary>
        /// Gets number of elements in the priority queue
        /// </summary>
        public int Count
        {
            get { return _baseHeap.Count; }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _baseHeap.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>Enumerator</returns>
        /// <remarks>
        /// Returned enumerator does not iterate elements in sorted order.</remarks>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _baseHeap.GetEnumerator();
        }

        public void CopyTo(Array array, int index)
        {
            _baseHeap.CopyTo((T[])array, index);
        }
        public bool IsSynchronized
        {
            get { return false; }
        }

        public object SyncRoot
        {
            get { return _baseHeap; }
        }
    }
}
