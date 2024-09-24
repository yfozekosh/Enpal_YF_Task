using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using YF.EnpalChallange.Api.Dto;
using Program = YF.EnpalChallange.Api.Program;

namespace YF.EnpalChallange.Tests;

public class PostCalendarApiTests(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();
    private static readonly DateTime DefaultDate = new(2024, 09, 24, 0, 0, 0, DateTimeKind.Utc);

    public static IEnumerable<object[]> InvalidInputs =>
        new List<object[]>
        {
            new object[]
            {
                new CalendarQueryInputDto
                {
                    Date = DefaultDate,
                    // Exceeds the array length of 50.
                    Products = Enumerable.Repeat("a", 51).ToArray(),
                    Language = "English",
                    Rating = "5"
                }
            },
            new object[]
            {
                new CalendarQueryInputDto
                {
                    Date = DefaultDate,
                    // Exceeds the item length of 128.
                    Products = [new string('a', 129)],
                    Language = "English",
                    Rating = "5"
                }
            },
            new object[]
            {
                new CalendarQueryInputDto
                {
                    Date = DefaultDate,
                    // Contains a null item.
                    Products = ["product", null!],
                    Language = "English",
                    Rating = "5"
                }
            },
            new object[]
            {
                new CalendarQueryInputDto
                {
                    Date = DefaultDate,
                    // Null for a required field.
                    Products = null!,
                    Language = "English",
                    Rating = "5"
                }
            },
            new object[]
            {
                new CalendarQueryInputDto
                {
                    Date = DefaultDate,
                    Products = ["product"],
                    // Null for a required field.
                    Language = null!,
                    Rating = "5"
                }
            },
            new object[]
            {
                new CalendarQueryInputDto
                {
                    Date = DefaultDate,
                    Products = ["product"],
                    Language = "English",
                    // Null for a required field.
                    Rating = null!
                }
            }
        };

    [Theory]
    [MemberData(nameof(InvalidInputs))]
    public async Task PostCalendar_InvalidInput_ReturnsBadRequest(CalendarQueryInputDto input)
    {
        // Act.
        var response = await _client.PostAsJsonAsync("/calendar", input);

        // Assert.
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
    }
}