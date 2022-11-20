namespace Mannifera.DataStructures
{
    public class MinPriorityQueueTests
    {
        class CustomComparable : IComparable<CustomComparable>
        {
            public int Value { get; private set; }

            public CustomComparable(int value)
            {
                this.Value = value;
            }

            public int CompareTo(CustomComparable other)
            {
                return this.Value.CompareTo(other.Value);
            }

            public int Compare(CustomComparable x, CustomComparable y)
            {
                return x.CompareTo(y);
            }
        }

        class CustomPoco
        {
            public int Value { get; private set; }

            public CustomPoco(int value)
            {
                this.Value = value;
            }
        }

        class CustomComparer : IComparer<CustomPoco>
        {
            public int Compare(CustomPoco x, CustomPoco y)
            {
                return x.Value.CompareTo(y.Value);
            }
        }

        [TestFixture]
        public class EnqueueTests
        {
            [Test]
            public void ShouldAddTwoItemsInOrder()
            {
                var queue = new MinPriorityQueue<int>();
                queue.Enqueue(3);
                queue.Enqueue(5);

                Assert.AreEqual(3, queue.Peek());
            }

            [Test]
            public void ShouldAddTwoItemsInReverseOrder()
            {
                var queue = new MinPriorityQueue<int>();
                queue.Enqueue(5);
                queue.Enqueue(3);

                Assert.AreEqual(3, queue.Peek());
            }

            [Test]
            public void ShouldAddMultipleItemsRandomly()
            {
                var queue = new MinPriorityQueue<int>();
                queue.Enqueue(5);
                queue.Enqueue(3);
                queue.Enqueue(6);
                queue.Enqueue(2);
                queue.Enqueue(1);

                Assert.AreEqual(1, queue.Peek());
            }

            [Test]
            public void ShouldAddMultipleItemsWithCustomIComparer()
            {
                MinPriorityQueue<CustomPoco> queue = new MinPriorityQueue<CustomPoco>(new CustomComparer());
                queue.Enqueue(new CustomPoco(5));
                queue.Enqueue(new CustomPoco(3));
                queue.Enqueue(new CustomPoco(6));
                queue.Enqueue(new CustomPoco(2));
                queue.Enqueue(new CustomPoco(1));

                Assert.AreEqual(1, queue.Peek().Value);
            }

            [Test]
            public void ShouldAddMultipleItemsWithCustomComparisonDelegate()
            {
                MinPriorityQueue<CustomPoco> queue = new MinPriorityQueue<CustomPoco>((x, y) => x.Value.CompareTo(y.Value));
                queue.Enqueue(new CustomPoco(5));
                queue.Enqueue(new CustomPoco(3));
                queue.Enqueue(new CustomPoco(6));
                queue.Enqueue(new CustomPoco(2));
                queue.Enqueue(new CustomPoco(1));

                Assert.AreEqual(1, queue.Peek().Value);
            }
        }

        [TestFixture]
        public class DequeueTests
        {
            [Test]
            public void ShouldRemoveCorrectly1()
            {
                var queue = new MinPriorityQueue<int>();
                queue.Enqueue(3);
                queue.Enqueue(5);

                Assert.AreEqual(3, queue.Dequeue());
                Assert.AreEqual(5, queue.Peek());
            }

            [Test]
            public void ShouldRemoveCorrectly2()
            {
                var queue = new MinPriorityQueue<int>();
                queue.Enqueue(5);
                queue.Enqueue(3);
                queue.Enqueue(6);
                queue.Enqueue(2);
                queue.Enqueue(1);

                Assert.AreEqual(1, queue.Dequeue());
                Assert.AreEqual(2, queue.Dequeue());
                Assert.AreEqual(3, queue.Dequeue());
                Assert.AreEqual(5, queue.Dequeue());
                Assert.AreEqual(6, queue.Dequeue());
            }

            [Test]
            public void ShouldRemoveCorrectlyWithCustomComparer()
            {
                MinPriorityQueue<CustomPoco> queue = new MinPriorityQueue<CustomPoco>(new CustomComparer());
                queue.Enqueue(new CustomPoco(5));
                queue.Enqueue(new CustomPoco(3));
                queue.Enqueue(new CustomPoco(6));
                queue.Enqueue(new CustomPoco(2));
                queue.Enqueue(new CustomPoco(1));

                Assert.AreEqual(1, queue.Dequeue().Value);
                Assert.AreEqual(2, queue.Dequeue().Value);
                Assert.AreEqual(3, queue.Dequeue().Value);
                Assert.AreEqual(5, queue.Dequeue().Value);
                Assert.AreEqual(6, queue.Dequeue().Value);
            }

            [Test]
            public void ShouldRemoveCorrectlyWithCustomComparisonDelegate()
            {
                MinPriorityQueue<CustomPoco> queue = new MinPriorityQueue<CustomPoco>((x, y) => x.Value.CompareTo(y.Value));
                queue.Enqueue(new CustomPoco(5));
                queue.Enqueue(new CustomPoco(3));
                queue.Enqueue(new CustomPoco(6));
                queue.Enqueue(new CustomPoco(2));
                queue.Enqueue(new CustomPoco(1));

                Assert.AreEqual(1, queue.Dequeue().Value);
                Assert.AreEqual(2, queue.Dequeue().Value);
                Assert.AreEqual(3, queue.Dequeue().Value);
                Assert.AreEqual(5, queue.Dequeue().Value);
                Assert.AreEqual(6, queue.Dequeue().Value);
            }
        }

        [TestFixture]
        public class CountTests
        {
            [Test]
            public void SetsCountCorrectly1()
            {
                var queue = new MinPriorityQueue<int>();
                Assert.AreEqual(0, queue.Count);

                queue.Enqueue(5);
                Assert.AreEqual(1, queue.Count);
            }

            [Test]
            public void SetsCountCorrectly2()
            {
                var queue = new MinPriorityQueue<int>();

                queue.Enqueue(5);
                Assert.AreEqual(1, queue.Count);

                queue.Dequeue();
                Assert.AreEqual(0, queue.Count);
            }
        }

        [TestFixture]
        public class RemoveTests
        {
            [Test]
            public void ShouldRemoveItemCorrectly()
            {
                var queue = new MinPriorityQueue<int>();
                queue.Enqueue(5);
                queue.Enqueue(3);
                queue.Enqueue(6);
                queue.Enqueue(2);
                queue.Enqueue(1);

                queue.Remove(3);

                Assert.That(queue.Count, Is.EqualTo(4));
                CollectionAssert.AreEquivalent(new[] { 1, 2, 5, 6 }, new[] { queue.Dequeue(), queue.Dequeue(), queue.Dequeue(), queue.Dequeue() });
            }

            [Test]
            public void ShouldDoNothingIfItemIsMissing()
            {
                var queue = new MinPriorityQueue<int>();
                queue.Enqueue(5);
                queue.Enqueue(3);
                queue.Enqueue(6);
                queue.Enqueue(2);
                queue.Enqueue(1);

                queue.Remove(999);

                Assert.That(queue.Count, Is.EqualTo(5));
                CollectionAssert.AreEquivalent(new[] { 1, 2, 3, 5, 6 }, new[] { queue.Dequeue(), queue.Dequeue(), queue.Dequeue(), queue.Dequeue(), queue.Dequeue() });

            }
        }

        [TestFixture]
        public class CtorTests
        {
            [Test]
            public void ShouldAcceptCustomIComparables()
            {
                MinPriorityQueue<CustomComparable> queue = new MinPriorityQueue<CustomComparable>();
            }

            [Test]
            public void ShouldThrowOnNonIComparables()
            {
                Assert.Throws<InvalidOperationException>(() => new MinPriorityQueue<object>());
            }

            [Test]
            public void ShouldThrowOnNullIComparer()
            {
                Assert.Throws<ArgumentNullException>(() => new MinPriorityQueue<CustomPoco>(comparer: null));
            }

            [Test]
            public void ShouldThrowOnNullComparison()
            {
                Assert.Throws<ArgumentNullException>(() => new MinPriorityQueue<CustomPoco>(comparison: null));
            }
        }
    }
}
