using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CoinSpotDotNet.Common
{
#pragma warning disable CS1591
    public class CoinSpotStringToDoubleJsonConverter : JsonConverter<double>
    {
        public override double Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => double.Parse(reader.GetString());

        public override void Write(Utf8JsonWriter writer, double value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value);
        }
    }
#pragma warning restore CS1591
}
