using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace YF.EnpalChallenge.Api.Dto.Converter;

public class DateAsStringDateTimeConverter : JsonConverter<DateTime>
{
    private readonly string _dateFormat = "yyyy-MM-dd";

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var readOnlySpan = reader.GetString();
        if (reader.TokenType == JsonTokenType.String &&
            DateTime.TryParseExact(
                readOnlySpan,
                _dateFormat,
                CultureInfo.CurrentCulture,
                DateTimeStyles.AssumeUniversal,
                out var date))
        {
            return date.ToUniversalTime();
        }

        throw new JsonException($"Expected date format is {_dateFormat}");
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(_dateFormat));
    }
}