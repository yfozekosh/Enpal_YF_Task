using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using YF.EnpalChallenge.Api.Dto.Converter;
using YF.EnpalChallenge.Api.Pipeline;

namespace YF.EnpalChallenge.Api.Dto;

public class CalendarQueryInputDto
{
    // Expected format is defined in DateAsStringDateTimeConverter.
    [Required]
    [JsonConverter(typeof(DateAsStringDateTimeConverter))]
    public required DateTime Date{ get; set; }

    [Required]
    [EachMaxLengthAndRequired(50, 128)]
    public required string[] Products { get; set; }

    [Required]
    [MaxLength(128)]
    public required string Language { get; set; }

    [Required]
    [MaxLength(128)]
    public required string Rating { get; set; }
}