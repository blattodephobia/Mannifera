using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mannifera.DataStructures
{
    public class BookDiffDto
    {
        [JsonProperty("e")]
        public EventKind Event { get; init; }

        [JsonProperty("s")]
        public string? Symbol { get; init; }

        [JsonProperty("b")]
        public PricePoint[]? Bids { get; init; }

        [JsonProperty("a")]
        public PricePoint[]? Asks { get; init; }
    }
}
