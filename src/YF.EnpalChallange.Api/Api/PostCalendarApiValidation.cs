using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using YF.EnpalChallange.Api.Dto;
using YF.EnpalChallange.Api.Services;

namespace YF.EnpalChallange.Api.Api;

public static class PostCalendarApiValidation
{
    public static void Map(WebApplication application)
    {
        application.MapPost("/calendar/query", async ([FromBody] CalendarQueryInputDto input, CalendarService calendarService) =>
            {
                var validationContext = new ValidationContext(input);
                var validationResults = new List<ValidationResult>();
                bool isValid = Validator.TryValidateObject(input, validationContext, validationResults, true);

                if (!isValid)
                {
                    return Results.BadRequest(validationResults);
                }

                var availableSlots = await calendarService.GetAvailableSlots(input.Date, input.Products, input.Language, input.Rating);

                // Process the input as needed
                return Results.Ok(availableSlots.Select(a => new CalendarQueryResponseItem
                {
                    AvailableCount = a.Value,
                    Start = a.Key
                }));
            })
            .WithName("PostCalendarQuery")
            .WithOpenApi();
    }
}