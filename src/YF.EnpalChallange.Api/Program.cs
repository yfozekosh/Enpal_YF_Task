using System.Data;
using Dapper;
using Npgsql;
using YF.EnpalChallange.Api.Api;
using YF.EnpalChallange.Api.Model;
using YF.EnpalChallange.Api.Model.Contract;
using YF.EnpalChallange.Api.Persistence;

namespace YF.EnpalChallange.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddPersistenceModule(builder.Configuration);

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        PostCalendarApi.Map(app);

        app.Run();
    }
}