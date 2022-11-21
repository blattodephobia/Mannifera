namespace Mannifera.DataStructures
{
    public class MinPriorityQueue<T> : PriorityQueueBase<T> where T : notnull
    {
        public MinPriorityQueue() :
            base()
        {
        }

        public MinPriorityQueue(IComparer<T> comparer) :
            base(comparer)
        {
        }

        public MinPriorityQueue(Func<T, T, int> comparison) : 
            base(comparison)
        {
        }

        protected override int PriorityCompare(T first, T second)
        {
            return Comparer.Invoke(first, second);
        }
    }
}
