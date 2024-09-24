using System.ComponentModel.DataAnnotations;
using YF.EnpalChallange.Api.Pipeline;

namespace YF.EnpalChallange.Api.Dto;

public class CalendarQueryInputDto
{
    public required DateTime Date { get; set; }

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