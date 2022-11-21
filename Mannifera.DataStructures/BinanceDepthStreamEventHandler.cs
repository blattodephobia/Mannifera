using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mannifera.DataStructures
{
    public class BinanceDepthStreamEventHandler
    {
        public PricePointTracker Bids { get; } = new PricePointTracker(new MinPriorityQueue<double>());
        public PricePointTracker Asks { get; } = new PricePointTracker(new MaxPriorityQueue<double>());

        public BinanceDepthStreamEventHandler(SnapshotDto? snapshot = null)
        {
            if (snapshot?.Asks != null)
            {
                for (int i = 0; i < snapshot.Asks.Length; i++)
                {
                    Asks.Add(snapshot.Asks[i]);
                }
            }

            if (snapshot?.Bids != null)
            {
                for (int i = 0; i < snapshot.Bids.Length; i++)
                {
                    Bids.Add(snapshot.Bids[i]);
                }
            }
        }

        public void HandleEvent(BookDiffDto diff)
        {
            for (int i = 0; i < diff.Bids?.Length; i++)
            {
                PricePoint bid = diff.Bids[i];
                if (bid.Quantity != 0)
                {
                    Bids.UpdatePriceQuantity(bid);
                }
                else
                {
                    Bids.Remove(bid);
                }
            }

            for (int i = 0; i < diff.Asks?.Length; i++)
            {
                PricePoint ask = diff.Asks[i];
                if (ask.Quantity != 0)
                {
                    Asks.UpdatePriceQuantity(ask);
                }
                else
                {
                    Asks.Remove(ask);
                }
            }
        }
    }
}
