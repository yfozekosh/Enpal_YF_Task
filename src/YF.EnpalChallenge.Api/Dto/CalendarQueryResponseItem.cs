using System.Text.Json.Serialization;

namespace YF.EnpalChallenge.Api.Dto;

public class CalendarQueryResponseItem
{
    [JsonPropertyName("available_count")]
    public required int AvailableCount { get; init; }

    [JsonIgnore]
    public DateTime Start { get; init; }

    [JsonPropertyName("start_date")]
    public string StartDateFormatted => Start.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
}