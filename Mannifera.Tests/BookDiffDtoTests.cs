using Newtonsoft.Json;

namespace Mannifera.DataStructures.Tests
{
    public class BookDiffDtoTests
    {
        [TestFixture]
        public class Deserialization
        {
            [Test]
            public void ShouldDeserializeAllPropetiesCorrectly()
            {
                var sampleJson = @"
{
 ""e"": ""depthUpdate"", ""s"": ""BNBBTC"", ""b"": [ [""0.0024"",""10""] ], ""a"": [
[""0.0026"", ""100""] ]
}
";
                BookDiffDto result = JsonConvert.DeserializeObject<BookDiffDto>(sampleJson, new PricePointJsonConverter()) ?? new BookDiffDto();

                Assert.That(result!.Event, Is.EqualTo(EventKind.DepthUpdate));
                Assert.That(result!.Symbol, Is.EqualTo("BNBBTC"));
                CollectionAssert.AreEqual(new[] { new PricePoint(0.0024, 10) }, result!.Bids);
                CollectionAssert.AreEqual(new[] { new PricePoint(0.0026, 100) }, result!.Asks);
            }
        }
    }
}
