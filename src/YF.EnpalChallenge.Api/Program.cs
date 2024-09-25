using YF.EnpalChallenge.Api.Api;
using YF.EnpalChallenge.Api.Persistence;
using YF.EnpalChallenge.Api.Services;

namespace YF.EnpalChallenge.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services
            .AddPersistenceModule(builder.Configuration)
            .AddCalendarModule();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        PostCalendarApiValidation.Map(app);

        app.Run();
    }
}