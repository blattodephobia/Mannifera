using Newtonsoft.Json;

namespace Mannifera.DataStructures.Tests
{
    public class SnapshotDtoTests
    {
        [TestFixture]
        public class Deserialization
        {
            [Test]
            public void ShouldDeserializeAllPropetiesCorrectly()
            {
                var sampleJson = @"
{
    ""bids"" : [
         [""0.0024"", ""14.70000000""],
         [""0.0022"",""6.40000000""],
         [""0.0020"",""9.70000000""]
     ],
    ""asks"" : [
         [""0.0024"",""14.90000000""],
         [""0.0026"",""3.60000000""],
         [""0.0028"",""1.00000000""]
     ]
}
";
                SnapshotDto result = JsonConvert.DeserializeObject<SnapshotDto>(sampleJson, new PricePointJsonConverter());
                Assert.That(result.Asks, Is.Not.Null);
                Assert.That(result.Asks!.Count, Is.EqualTo(3));
                Assert.That(result.Bids, Is.Not.Null);
                Assert.That(result.Bids!.Count, Is.EqualTo(3));

                var expectedBids = new[]
                {
                    new PricePoint(0.0024, 14.70000000),
                    new PricePoint(0.0022, 6.40000000),
                    new PricePoint(0.0020, 9.70000000)
                };

                var expectedAsks = new[]
                {
                    new PricePoint(0.0024, 14.90000000),
                    new PricePoint(0.0026, 3.60000000),
                    new PricePoint(0.0028, 1.00000000)
                };

                CollectionAssert.AreEqual(expectedBids, result.Bids);
                CollectionAssert.AreEqual(expectedAsks, result.Asks);
            }
        }
    }
}
