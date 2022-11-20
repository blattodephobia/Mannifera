using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mannifera.DataStructures
{
    public class PricePointJsonConverter : JsonConverter<PricePoint>
    {
        public override PricePoint ReadJson(JsonReader reader, Type objectType, PricePoint existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var result = new PricePoint(reader.ReadAsDouble().GetValueOrDefault(), reader.ReadAsDouble().GetValueOrDefault());
            while (reader.TokenType != JsonToken.EndArray)
            {
                reader.Read();
            }

            return result;
        }

        public override void WriteJson(JsonWriter writer, PricePoint value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
