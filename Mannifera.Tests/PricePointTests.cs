namespace Mannifera.DataStructures.Tests
{
    public class PricePointTests
    {
        [TestFixture]
        public class EqualityTests
        {
            [Test]
            public void ShouldTreatValuesOutsidePrecisionAsEqual()
            {
                var x = new PricePoint(10.00000000123, 999.00000000777);
                var y = new PricePoint(10.00000000456, 999.00000000888);

                Assert.That(x == y, Is.True);
                Assert.That(x != y, Is.False);
                Assert.That(x.Equals(y), Is.True);
            }

            [Test]
            public void ShouldTreatValuesWithinPrecisionAsNotEqual()
            {
                var x = new PricePoint(10.0000123, 999.0000777);
                var y = new PricePoint(10.0000456, 999.0000888);

                Assert.That(x == y, Is.False);
                Assert.That(x != y, Is.True);
                Assert.That(x.Equals(y), Is.False);
            }
        }

        [TestFixture]
        public class GetHashCodeTest
        {
            [Test]
            public void ShouldGenerateSameHashCodeForValuesOutsidePrecision()
            {
                var x = new PricePoint(10.00000000123, 999.00000000777);
                var y = new PricePoint(10.00000000456, 999.00000000888);

                Assert.That(x.GetHashCode(), Is.EqualTo(y.GetHashCode()));
            }

            [Test]
            public void ShouldGenerateDifferentHashCodeForValuesWithinPrecision()
            {
                var x = new PricePoint(10.0000123, 999.0000777);
                var y = new PricePoint(10.0000456, 999.0000888);

                Assert.That(x.GetHashCode(), Is.Not.EqualTo(y.GetHashCode()));
            }
        }
    }
}
