using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using YF.EnpalChallange.Api.Dto;
using YF.EnpalChallange.Api.Persistence;

namespace YF.EnpalChallange.Api.Api;

public static class PostCalendarApi
{
    public static void Map(WebApplication application)
    {
        application.MapGet("/salesmanagers", async (ManagerRepository dbConnection) =>
            {
                var salesManagers = await dbConnection.FindManagersByFeatures(["SolarPanels", "Heatpumps"], "English", "Gold");
                return Results.Ok(salesManagers);
            })
            .WithName("GetSalesManagers")
            .WithOpenApi();

        application.MapPost("/calendar", async ([FromBody] CalendarQueryInputDto input) =>
            {
                var validationContext = new ValidationContext(input);
                var validationResults = new List<ValidationResult>();
                bool isValid = Validator.TryValidateObject(input, validationContext, validationResults, true);

                if (!isValid)
                {
                    return Results.BadRequest(validationResults);
                }

                // Process the input as needed
                return Results.Ok(input);
            })
            .WithName("PostCalendarQuery")
            .WithOpenApi();
    }
}