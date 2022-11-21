namespace Mannifera.DataStructures
{
    public class MaxPriorityQueueTests
    {
        class CustomComparable : IComparable<CustomComparable>
        {
            public int Value { get; private set; }

            public CustomComparable(int value)
            {
                Value = value;
            }

            public int CompareTo(CustomComparable? other)
            {
                if (other == null) throw new ArgumentNullException(nameof(other));

                return Value.CompareTo(other.Value);
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
                Value = value;
            }
        }

        class CustomComparer : IComparer<CustomPoco>
        {
            public int Compare(CustomPoco? x, CustomPoco? y)
            {
                if (x == null || y == null) throw new ArgumentNullException();

                return x.Value.CompareTo(y.Value);
            }
        }

        [TestFixture]
        public class AddTests
        {
            [Test]
            public void ShouldAddTwoItemsInOrder()
            {
                var queue = new MaxPriorityQueue<int>();
                queue.Enqueue(3);
                queue.Enqueue(5);

                Assert.AreEqual(5, queue.Peek());
            }

            [Test]
            public void ShouldAddTwoItemsInReverseOrder()
            {
                var queue = new MaxPriorityQueue<int>();
                queue.Enqueue(5);
                queue.Enqueue(3);

                Assert.AreEqual(5, queue.Peek());
            }

            [Test]
            public void ShouldAddMultipleItemsRandomly()
            {
                var queue = new MaxPriorityQueue<int>();
                queue.Enqueue(5);
                queue.Enqueue(3);
                queue.Enqueue(6);
                queue.Enqueue(2);
                queue.Enqueue(1);

                Assert.AreEqual(6, queue.Peek());
            }

            [Test]
            public void ShouldAddMultipleItemsWithCustomIComparer()
            {
                MaxPriorityQueue<CustomPoco> queue = new MaxPriorityQueue<CustomPoco>(new CustomComparer());
                queue.Enqueue(new CustomPoco(5));
                queue.Enqueue(new CustomPoco(3));
                queue.Enqueue(new CustomPoco(6));
                queue.Enqueue(new CustomPoco(2));
                queue.Enqueue(new CustomPoco(1));

                Assert.AreEqual(6, queue.Peek().Value);
            }

            [Test]
            public void ShouldAddMultipleItemsWithCustomComparisonDelegate()
            {
                var queue = new MaxPriorityQueue<CustomPoco>((x, y) => x.Value.CompareTo(y.Value));
                queue.Enqueue(new CustomPoco(5));
                queue.Enqueue(new CustomPoco(3));
                queue.Enqueue(new CustomPoco(6));
                queue.Enqueue(new CustomPoco(2));
                queue.Enqueue(new CustomPoco(1));

                Assert.AreEqual(6, queue.Peek().Value);
            }
        }

        [TestFixture]
        public class RemoveTests
        {
            [Test]
            public void ShouldRemoveCorrectly1()
            {
                var queue = new MaxPriorityQueue<int>();
                queue.Enqueue(3);
                queue.Enqueue(5);

                Assert.AreEqual(5, queue.Dequeue());
                Assert.AreEqual(3, queue.Peek());
            }

            [Test]
            public void ShouldRemoveCorrectly2()
            {
                var queue = new MaxPriorityQueue<int>();
                queue.Enqueue(5);
                queue.Enqueue(3);
                queue.Enqueue(6);
                queue.Enqueue(2);
                queue.Enqueue(1);

                Assert.AreEqual(6, queue.Dequeue());
                Assert.AreEqual(5, queue.Dequeue());
                Assert.AreEqual(3, queue.Dequeue());
                Assert.AreEqual(2, queue.Dequeue());
                Assert.AreEqual(1, queue.Dequeue());
            }

            [Test]
            public void ShouldRemoveCorrectlyWithCustomComparer()
            {
                var queue = new MaxPriorityQueue<CustomPoco>(new CustomComparer());
                queue.Enqueue(new CustomPoco(5));
                queue.Enqueue(new CustomPoco(3));
                queue.Enqueue(new CustomPoco(6));
                queue.Enqueue(new CustomPoco(2));
                queue.Enqueue(new CustomPoco(1));

                Assert.AreEqual(6, queue.Dequeue().Value);
                Assert.AreEqual(5, queue.Dequeue().Value);
                Assert.AreEqual(3, queue.Dequeue().Value);
                Assert.AreEqual(2, queue.Dequeue().Value);
                Assert.AreEqual(1, queue.Dequeue().Value);
            }

            [Test]
            public void ShouldRemoveCorrectlyWithCustomComparisonDelegate()
            {
                var queue = new MaxPriorityQueue<CustomPoco>((x, y) => x.Value.CompareTo(y.Value));
                queue.Enqueue(new CustomPoco(5));
                queue.Enqueue(new CustomPoco(3));
                queue.Enqueue(new CustomPoco(6));
                queue.Enqueue(new CustomPoco(2));
                queue.Enqueue(new CustomPoco(1));

                Assert.AreEqual(6, queue.Dequeue().Value);
                Assert.AreEqual(5, queue.Dequeue().Value);
                Assert.AreEqual(3, queue.Dequeue().Value);
                Assert.AreEqual(2, queue.Dequeue().Value);
                Assert.AreEqual(1, queue.Dequeue().Value);
            }
        }

        [TestFixture]
        public class CountTests
        {
            [Test]
            public void SetsCountCorrectly1()
            {
                var queue = new MaxPriorityQueue<int>();
                Assert.AreEqual(0, queue.Count);

                queue.Enqueue(5);
                Assert.AreEqual(1, queue.Count);
            }

            [Test]
            public void SetsCountCorrectly2()
            {
                var queue = new MaxPriorityQueue<int>();

                queue.Enqueue(5);
                Assert.AreEqual(1, queue.Count);

                queue.Dequeue();
                Assert.AreEqual(0, queue.Count);
            }
        }

        [TestFixture]
        public class CtorTests
        {
            [Test]
            public void ShouldAcceptCustomIComparables()
            {
                var queue = new MaxPriorityQueue<CustomComparable>();
            }

            [Test]
            public void ShouldThrowOnNonIComparables()
            {
                Assert.Throws<InvalidOperationException>(() => new MaxPriorityQueue<object>());
            }

            [Test]
            public void ShouldThrowOnNullIComparer()
            {
                 Assert.Throws<ArgumentNullException>(() => new MaxPriorityQueue<CustomPoco>(comparer: null!));
            }

            [Test]
            public void ShouldThrowOnNullComparison()
            {
                Assert.Throws<ArgumentNullException>(() => new MaxPriorityQueue<CustomPoco>(comparison: null!));
            }
        }
    }
}
