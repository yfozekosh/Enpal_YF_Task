using YF.EnpalChallange.Api.Api;
using YF.EnpalChallange.Api.Persistence;
using YF.EnpalChallange.Api.Services;

namespace YF.EnpalChallange.Api;

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