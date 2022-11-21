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
                MaxPriorityQueue<int> heap = new MaxPriorityQueue<int>();
                heap.Enqueue(3);
                heap.Enqueue(5);

                Assert.AreEqual(5, heap.Peek());
            }

            [Test]
            public void ShouldAddTwoItemsInReverseOrder()
            {
                MaxPriorityQueue<int> heap = new MaxPriorityQueue<int>();
                heap.Enqueue(5);
                heap.Enqueue(3);

                Assert.AreEqual(5, heap.Peek());
            }

            [Test]
            public void ShouldAddMultipleItemsRandomly()
            {
                MaxPriorityQueue<int> heap = new MaxPriorityQueue<int>();
                heap.Enqueue(5);
                heap.Enqueue(3);
                heap.Enqueue(6);
                heap.Enqueue(2);
                heap.Enqueue(1);

                Assert.AreEqual(6, heap.Peek());
            }

            [Test]
            public void ShouldAddMultipleItemsWithCustomIComparer()
            {
                MaxPriorityQueue<CustomPoco> heap = new MaxPriorityQueue<CustomPoco>(new CustomComparer());
                heap.Enqueue(new CustomPoco(5));
                heap.Enqueue(new CustomPoco(3));
                heap.Enqueue(new CustomPoco(6));
                heap.Enqueue(new CustomPoco(2));
                heap.Enqueue(new CustomPoco(1));

                Assert.AreEqual(6, heap.Peek().Value);
            }

            [Test]
            public void ShouldAddMultipleItemsWithCustomComparisonDelegate()
            {
                MaxPriorityQueue<CustomPoco> heap = new MaxPriorityQueue<CustomPoco>((x, y) => x.Value.CompareTo(y.Value));
                heap.Enqueue(new CustomPoco(5));
                heap.Enqueue(new CustomPoco(3));
                heap.Enqueue(new CustomPoco(6));
                heap.Enqueue(new CustomPoco(2));
                heap.Enqueue(new CustomPoco(1));

                Assert.AreEqual(6, heap.Peek().Value);
            }
        }

        [TestFixture]
        public class RemoveTests
        {
            [Test]
            public void ShouldRemoveCorrectly1()
            {
                MaxPriorityQueue<int> heap = new MaxPriorityQueue<int>();
                heap.Enqueue(3);
                heap.Enqueue(5);

                Assert.AreEqual(5, heap.Dequeue());
                Assert.AreEqual(3, heap.Peek());
            }

            [Test]
            public void ShouldRemoveCorrectly2()
            {
                MaxPriorityQueue<int> heap = new MaxPriorityQueue<int>();
                heap.Enqueue(5);
                heap.Enqueue(3);
                heap.Enqueue(6);
                heap.Enqueue(2);
                heap.Enqueue(1);

                Assert.AreEqual(6, heap.Dequeue());
                Assert.AreEqual(5, heap.Dequeue());
                Assert.AreEqual(3, heap.Dequeue());
                Assert.AreEqual(2, heap.Dequeue());
                Assert.AreEqual(1, heap.Dequeue());
            }

            [Test]
            public void ShouldRemoveCorrectlyWithCustomComparer()
            {
                MaxPriorityQueue<CustomPoco> heap = new MaxPriorityQueue<CustomPoco>(new CustomComparer());
                heap.Enqueue(new CustomPoco(5));
                heap.Enqueue(new CustomPoco(3));
                heap.Enqueue(new CustomPoco(6));
                heap.Enqueue(new CustomPoco(2));
                heap.Enqueue(new CustomPoco(1));

                Assert.AreEqual(6, heap.Dequeue().Value);
                Assert.AreEqual(5, heap.Dequeue().Value);
                Assert.AreEqual(3, heap.Dequeue().Value);
                Assert.AreEqual(2, heap.Dequeue().Value);
                Assert.AreEqual(1, heap.Dequeue().Value);
            }

            [Test]
            public void ShouldRemoveCorrectlyWithCustomComparisonDelegate()
            {
                MaxPriorityQueue<CustomPoco> heap = new MaxPriorityQueue<CustomPoco>((x, y) => x.Value.CompareTo(y.Value));
                heap.Enqueue(new CustomPoco(5));
                heap.Enqueue(new CustomPoco(3));
                heap.Enqueue(new CustomPoco(6));
                heap.Enqueue(new CustomPoco(2));
                heap.Enqueue(new CustomPoco(1));

                Assert.AreEqual(6, heap.Dequeue().Value);
                Assert.AreEqual(5, heap.Dequeue().Value);
                Assert.AreEqual(3, heap.Dequeue().Value);
                Assert.AreEqual(2, heap.Dequeue().Value);
                Assert.AreEqual(1, heap.Dequeue().Value);
            }
        }

        [TestFixture]
        public class CountTests
        {
            [Test]
            public void SetsCountCorrectly1()
            {
                MaxPriorityQueue<int> heap = new MaxPriorityQueue<int>();
                Assert.AreEqual(0, heap.Count);

                heap.Enqueue(5);
                Assert.AreEqual(1, heap.Count);
            }

            [Test]
            public void SetsCountCorrectly2()
            {
                MaxPriorityQueue<int> heap = new MaxPriorityQueue<int>();

                heap.Enqueue(5);
                Assert.AreEqual(1, heap.Count);

                heap.Dequeue();
                Assert.AreEqual(0, heap.Count);
            }
        }

        [TestFixture]
        public class CtorTests
        {
            [Test]
            public void ShouldAcceptCustomIComparables()
            {
                MaxPriorityQueue<CustomComparable> heap = new MaxPriorityQueue<CustomComparable>();
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
