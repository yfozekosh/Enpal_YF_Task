﻿namespace YF.EnpalChallenge.Api.Services;

public static class CalendarModuleDefinition
{
    public static IServiceCollection AddCalendarModule(this IServiceCollection services)
    {
        services.AddScoped<CalendarService>();
        return services;
    }
}