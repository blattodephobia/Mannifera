namespace Mannifera.DataStructures
{
    public class MaxPriorityQueue<T> : PriorityQueueBase<T> where T : notnull
    {
        public MaxPriorityQueue() :
            base()
        {
        }

        public MaxPriorityQueue(IComparer<T> comparer) :
            base(comparer)
        {
        }

        public MaxPriorityQueue(Func<T, T, int> comparison) : 
            base(comparison)
        {
        }

        protected override int PriorityCompare(T first, T second)
        {
            return -Comparer.Invoke(first, second);
        }
    }
}
