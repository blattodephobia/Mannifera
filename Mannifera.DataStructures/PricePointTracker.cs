namespace Mannifera.DataStructures
{
    public class PricePointTracker
    {
        private readonly PriorityQueueBase<double> _bestPriceTracker;
        private readonly Dictionary<double, PricePoint> _pricePoints = new Dictionary<double, PricePoint>();

        public void Add(PricePoint pricePoint)
        {
            _pricePoints.Add(pricePoint.PriceLevel, pricePoint);
            _bestPriceTracker.Enqueue(pricePoint.PriceLevel);
        }

        public void Remove(PricePoint pricePoint)
        {
            _pricePoints.Remove(pricePoint.PriceLevel);
            _bestPriceTracker.Remove(pricePoint.PriceLevel);
        }

        public void UpdatePriceQuantity(PricePoint pricePoint)
        {
            if (!_pricePoints.ContainsKey(pricePoint.PriceLevel))
            {
                Add(pricePoint);
            }
            else
            {
                _pricePoints[pricePoint.PriceLevel] = pricePoint;
            }
        }

        public PricePoint GetBestPrice() => _pricePoints[_bestPriceTracker.Peek()];

        public PricePointTracker(PriorityQueueBase<double> bestPriceTracker)
        {
            _bestPriceTracker = bestPriceTracker;
        }
    }
}
