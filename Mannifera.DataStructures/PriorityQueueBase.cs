using System.Diagnostics;

namespace Mannifera.DataStructures
{
    public abstract class PriorityQueueBase<T> where T : notnull
    {
        [DebuggerStepThrough]
        private static int GetLeftChildIndex(int currentElementIndex)
        {
            return currentElementIndex * 2 + 1;
        }

        [DebuggerStepThrough]
        private static int GetRightChildIndex(int currentElementIndex)
        {
            return currentElementIndex * 2 + 2;
        }

        [DebuggerStepThrough]
        private static int GetParentIndex(int currentElementIndex)
        {
            return (currentElementIndex - 1) / 2;
        }

        private readonly List<T> _internalList = new List<T>();
        private readonly Dictionary<T, int> _indexCache = new Dictionary<T, int>();

        protected PriorityQueueBase()
        {
            if (!typeof(IComparable<T>).IsAssignableFrom(typeof(T)))
            {
                throw new InvalidOperationException($"The default constructor of this type expects that the generic type argument T implements {typeof(IComparable<>)}.");
            }

            Comparer = (x, y) => ((IComparable<T>)x).CompareTo(y);
        }

        protected PriorityQueueBase(IComparer<T> comparer)
        {
            if (comparer == null) throw new ArgumentNullException(nameof(comparer));

            Comparer = (x, y) => comparer.Compare(x, y);
        }

        protected PriorityQueueBase(Func<T, T, int> comparison)
        {
            if (comparison == null) throw new ArgumentNullException(nameof(comparison));

            Comparer = comparison;
        }

        public void Enqueue(T item)
        {
            _internalList.Add(item);
            UpdateIndex(item, Count - 1);
            PercolateUp(_internalList.Count - 1);
        }

        public T Dequeue()
        {
            T result = _internalList.First();
            RemoveFromInternalList(0);

            return result;
        }

        private void RemoveFromInternalList(int index)
        {
            Swap(index, _internalList.Count - 1);
            _internalList.RemoveAt(_internalList.Count - 1);
            PercolateDown(index);
        }

        public void Remove(T item)
        {
            int itemIndex = IndexOf(item);
            if (itemIndex >= 0)
            {
                RemoveFromInternalList(itemIndex);
            }
        }

        private void UpdateIndex(T item, int index)
        {
            if (_indexCache.ContainsKey(item))
            {
                _indexCache[item] = index;
            }
            else
            {
                _indexCache.Add(item, index);
            }
        }

        public T Peek() => _internalList[0];

        public int Count => _internalList.Count;

        /// <summary>
        /// Gets an integer indicating the order in which the parameters should appear in an ordered sequence.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns>-1 if <paramref name="first"/> should come before <paramref name="second"/>, 0 if they have equal values, or 1 if <paramref name="first"/> should come after <paramref name="second"/>.</returns>
        protected abstract int PriorityCompare(T first, T second);

        protected Func<T, T, int> Comparer { get; private set; }

        private void PercolateUp(int index)
        {
            int parentIndex = GetParentIndex(index);
            if (PriorityCompare(_internalList[index], _internalList[parentIndex]) < 0)
            {
                Swap(index, parentIndex);
                PercolateUp(parentIndex);
            }
        }

        private void PercolateDown(int index)
        {
            int bottomElementIndex = index;
            int leftChildIndex = GetLeftChildIndex(index);
            int rightChildIndex = GetRightChildIndex(index);

            bottomElementIndex = IsInRange(leftChildIndex) && PriorityCompare(_internalList[bottomElementIndex], _internalList[leftChildIndex]) >= 0
                ? leftChildIndex
                : bottomElementIndex;
            bottomElementIndex = IsInRange(rightChildIndex) && PriorityCompare(_internalList[bottomElementIndex], _internalList[rightChildIndex]) >= 0
                ? rightChildIndex
                : bottomElementIndex;

            if (bottomElementIndex != index)
            {
                Swap(bottomElementIndex, index);
                PercolateDown(bottomElementIndex);
            }
        }

        private void Swap(int elementIndex1, int elementIndex2)
        {
            if (elementIndex1 != elementIndex2)
            {
                UpdateIndex(_internalList[elementIndex1], elementIndex2);
                UpdateIndex(_internalList[elementIndex2], elementIndex1);

                T tmp = _internalList[elementIndex1];
                _internalList[elementIndex1] = _internalList[elementIndex2];
                _internalList[elementIndex2] = tmp;
            }
        }

        private int IndexOf(T item) => _indexCache.TryGetValue(item, out int result) ? result : -1;

        private bool IsInRange(int index)
        {
            return 0 <= index && index < _internalList.Count;
        }
    }
}
