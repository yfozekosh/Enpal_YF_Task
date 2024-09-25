using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using YF.EnpalChallange.Api.Dto.Converter;
using YF.EnpalChallange.Api.Pipeline;

namespace YF.EnpalChallange.Api.Dto;

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