using System.Net.Http.Json;
using System.Text.Json.Nodes;
using Xunit.Abstractions;
using YF.EnpalChallange.Api.Dto;

namespace YF.EnpalChallange.Tests;

// Ported from nodejs tests provided by Enpal.
public class PostCalendarEnpalApiTests(ApiTestingClientFactory factory, ITestOutputHelper output)
    : IClassFixture<ApiTestingClientFactory>
{
    private readonly HttpClient _client = factory.CreateClient();

    public static IEnumerable<object[]> TestData =>
        new List<object[]>
        {
            new object[]
            {
                new
                {
                    Date = "2024-05-03",
                    Products = new[]
                    {
                        "SolarPanels",
                        "Heatpumps"
                    },
                    Language = "German",
                    Rating = "Gold"
                },
                new[]
                {
                    new
                    {
                        StartDateFormatted = "2024-05-03T10:30:00.000Z",
                        AvailableCount = 1
                    },
                    new
                    {
                        StartDateFormatted = "2024-05-03T11:00:00.000Z",
                        AvailableCount = 1
                    },
                    new
                    {
                        StartDateFormatted = "2024-05-03T11:30:00.000Z",
                        AvailableCount = 1
                    }
                }
            },
            new object[]
            {
                new
                {
                    Date = "2024-05-03",
                    Products = new[]
                    {
                        "Heatpumps"
                    },
                    Language = "English",
                    Rating = "Silver"
                },
                new[]
                {
                    new
                    {
                        StartDateFormatted = "2024-05-03T10:30:00.000Z",
                        AvailableCount = 1
                    },
                    new
                    {
                        StartDateFormatted = "2024-05-03T11:00:00.000Z",
                        AvailableCount = 1
                    },
                    new
                    {
                        StartDateFormatted = "2024-05-03T11:30:00.000Z",
                        AvailableCount = 2
                    }
                }
            },
            new object[]
            {
                new
                {
                    Date = "2024-05-03",
                    Products = new[]
                    {
                        "SolarPanels"
                    },
                    Language = "German",
                    Rating = "Bronze"
                },
                new[]
                {
                    new
                    {
                        StartDateFormatted = "2024-05-03T10:30:00.000Z",
                        AvailableCount = 1
                    },
                    new
                    {
                        StartDateFormatted = "2024-05-03T11:00:00.000Z",
                        AvailableCount = 1
                    },
                    new
                    {
                        StartDateFormatted = "2024-05-03T11:30:00.000Z",
                        AvailableCount = 1
                    }
                }
            },
            new object[]
            {
                new
                {
                    Date = "2024-05-04",
                    Products = new[]
                    {
                        "SolarPanels",
                        "Heatpumps"
                    },
                    Language = "German",
                    Rating = "Gold"
                },
                Array.Empty<CalendarQueryResponseItem>()
            },
            new object[]
            {
                new
                {
                    Date = "2024-05-04",
                    Products = new[]
                    {
                        "Heatpumps"
                    },
                    Language = "English",
                    Rating = "Silver"
                },
                new[]
                {
                    new
                    {
                        StartDateFormatted = "2024-05-04T11:30:00.000Z",
                        AvailableCount = 1
                    }
                }
            },
            new object[]
            {
                new
                {
                    Date = "2024-05-04",
                    Products = new[]
                    {
                        "SolarPanels"
                    },
                    Language = "German",
                    Rating = "Bronze"
                },
                new[]
                {
                    new
                    {
                        StartDateFormatted = "2024-05-04T10:30:00.000Z",
                        AvailableCount = 1
                    }
                }
            }
        };

    [Theory]
    [MemberData(nameof(TestData))]
    public async Task PostCalendarApiTests_ShouldReturnExpectedResults(object input, dynamic[] expectedResult)
    {
        // Act
        var response = await _client.PostAsJsonAsync("/calendar/query", input);
        var responseJson = await response.Content.ReadAsStringAsync();
        output.WriteLine(responseJson);
        response.EnsureSuccessStatusCode();

        var jarray = JsonNode.Parse(responseJson)!.AsArray();

        // Assert
        Assert.Equal(200, (int)response.StatusCode);
        Assert.Equal(expectedResult.Length, jarray.Count);

        for (int i = 0; i < expectedResult.Length; i++)
        {
            Assert.Equal(expectedResult[i].AvailableCount, jarray[i]!["available_count"]!.GetValue<int>());
            Assert.Equal(expectedResult[i].StartDateFormatted, jarray[i]!["start_date"]!.GetValue<string>());
        }
    }
}