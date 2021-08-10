using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CoinSpotDotNet.Common
{
#pragma warning disable CS1591
    internal class CoinSpotDateTimeJsonConverter : JsonConverter<DateTime?>
    {
        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => 
            reader.TryGetDateTime(out var dt) ? dt : DateTime.ParseExact(reader.GetString(), "yyyy-MM-ddThh:mm:ss.fffZ", CultureInfo.InvariantCulture);

        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            if (!value.HasValue)
            {
                writer.WriteNullValue();
                return;
            }
            writer.WriteStringValue(value?.ToString("yyyy-MM-dd"));
        }
    }
#pragma warning restore CS1591
}
