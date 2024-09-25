using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit.Abstractions;
using Program = YF.EnpalChallenge.Api.Program;

namespace YF.EnpalChallenge.Tests;

public class PostCalendarApiValidationTests(WebApplicationFactory<Program> factory, ITestOutputHelper testOutput)
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();
    private static readonly string DefaultDate = "2024-09-24";

    public static IEnumerable<object[]> InvalidInputs =>
        new List<object[]>
        {
            new object[]
            {
                new
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
                new
                {
                    Date = DefaultDate,
                    // Exceeds the item length of 128.
                    Products = (string[])[new string('a', 129)],
                    Language = "English",
                    Rating = "5"
                }
            },
            new object[]
            {
                new
                {
                    Date = DefaultDate,
                    // Contains a null item.
                    Products = (string[])["product", null!],
                    Language = "English",
                    Rating = "5"
                }
            },
            new object[]
            {
                new
                {
                    Date = DefaultDate,
                    // Null for a required field.
                    Products = (string[])null!,
                    Language = "English",
                    Rating = "5"
                }
            },
            new object[]
            {
                new
                {
                    Date = DefaultDate,
                    Products = (string[])["product"],
                    // Null for a required field.
                    Language = (string?)null,
                    Rating = "5"
                }
            },
            new object[]
            {
                new
                {
                    Date = DefaultDate,
                    Products = (string[])["product"],
                    Language = "English",
                    // Null for a required field.
                    Rating = (string)null!
                }
            },
            new object[]
            {
                new
                {
                    // Date not present.
                    Date = (string?)null,
                    Products = (string[]) ["product"],
                    Language = "English",
                    // Empty string for a required field.
                    Rating = string.Empty
                }
            },
            new object[]
            {
                new
                {
                    // Invalid date format.
                    Date = "2024-09-244",
                    Products = (string[]) ["product"],
                    // Empty string for a required field.
                    Language = "English",
                    Rating = "5"
                }
            },
            new object[]
            {
                new
                {
                    // Invalid date format.
                    Date = "2024-30-05",
                    Products = (string[]) ["product"],
                    // Empty string for a required field.
                    Language = "English",
                    Rating = "5"
                }
            },
        };

    [Theory]
    [MemberData(nameof(InvalidInputs))]
    public async Task PostCalendar_InvalidInput_ReturnsBadRequest(object input)
    {
        // Act.
        var response = await _client.PostAsJsonAsync("/calendar/query", input);
        var responseString = await response.Content.ReadAsStringAsync();

        // Assert.
        testOutput.WriteLine(responseString);
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
    }
}