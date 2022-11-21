using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mannifera.DataStructures.Tests
{
    public class BinanceDepthStreamEventHandlerTests
    {
        [TestFixture]
        public class HandleEventTests
        {
            [Test]
            public void ShouldAddBidsCorrectly()
            {
                var handler = new BinanceDepthStreamEventHandler();

                handler.HandleEvent(new BookDiffDto() { Bids = new[] { new PricePoint(10, 1), new PricePoint(12, 2) } });

                Assert.That(handler.Bids.GetBestPrice().PriceLevel, Is.EqualTo(10));
            }

            [Test]
            public void ShouldAddBidsCorrectlyWithInitialSnapshot()
            {
                var handler = new BinanceDepthStreamEventHandler(new SnapshotDto() { Bids = new[] { new PricePoint(20, 1) } });

                handler.HandleEvent(new BookDiffDto() { Bids = new[] { new PricePoint(10, 1), new PricePoint(12, 2) } });

                Assert.That(handler.Bids.GetBestPrice().PriceLevel, Is.EqualTo(10));
            }

            [Test]
            public void ShouldAddAsksCorrectly()
            {
                var handler = new BinanceDepthStreamEventHandler();

                handler.HandleEvent(new BookDiffDto() { Asks = new[] { new PricePoint(10, 1), new PricePoint(12, 2) } });

                Assert.That(handler.Asks.GetBestPrice().PriceLevel, Is.EqualTo(12));
            }

            [Test]
            public void ShouldAddAsksCorrectlyWithInitialSnapshot()
            {
                var handler = new BinanceDepthStreamEventHandler(new SnapshotDto() { Asks = new[] { new PricePoint(10, 1) } });

                handler.HandleEvent(new BookDiffDto() { Asks = new[] { new PricePoint(12, 21), new PricePoint(20, 2) } });

                Assert.That(handler.Asks.GetBestPrice().PriceLevel, Is.EqualTo(20));
            }

            [Test]
            public void ShouldRemovePricePointsWhenQuantityIsZero()
            {
                IEnumerable<PricePoint> testPoints = Enumerable.Range(1, 100).Select(i => new PricePoint(i, 10.7 + i/10.0));
                var handler = new BinanceDepthStreamEventHandler(new SnapshotDto() { Asks = testPoints.ToArray(), Bids = testPoints.ToArray() });

                handler.HandleEvent(new BookDiffDto()
                                    {
                                        Asks = new[] { new PricePoint(100, 0) },
                                        Bids = new[] { new PricePoint(1, 0) }
                                    });

                Assert.That(handler.Asks.GetBestPrice().PriceLevel, Is.EqualTo(99.0));
                Assert.That(handler.Bids.GetBestPrice().PriceLevel, Is.EqualTo(2.0));
            }

            [Test]
            public void ShouldUpdatePriceQuantities()
            {
                IEnumerable<PricePoint> testPoints = Enumerable.Range(1, 100).Select(i => new PricePoint(i, 10.7 + i/10.0));
                var handler = new BinanceDepthStreamEventHandler(new SnapshotDto() { Asks = testPoints.ToArray(), Bids = testPoints.ToArray() });

                handler.HandleEvent(new BookDiffDto()
                                    {
                                        Asks = new[] { new PricePoint(100, 999) },
                                        Bids = new[] { new PricePoint(1, 888) }
                                    });

                Assert.That(handler.Asks.GetBestPrice().Quantity, Is.EqualTo(999));
                Assert.That(handler.Bids.GetBestPrice().Quantity, Is.EqualTo(888));
            }
        }
    }
}
