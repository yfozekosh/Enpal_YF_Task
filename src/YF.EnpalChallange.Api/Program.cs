using System.Data;
using Dapper;
using Npgsql;
using YF.EnpalChallange.Api.Model;
using YF.EnpalChallange.Api.Model.Contract;
using YF.EnpalChallange.Api.Persistence;

namespace YF.EnpalChallange.Api;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        ConfigureDb(builder);
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        Api.Map(app);

        app.Run();
    }

    private static void ConfigureDb(WebApplicationBuilder builder)
    {
        // Add the database connection
        builder.Services.AddScoped<IDbConnection>(sp =>
            new NpgsqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

        SqlMapper.AddTypeHandler(new IdTypeHandler<SalesManagerId>(id => new SalesManagerId(id)));
        SqlMapper.AddTypeHandler(new IdTypeHandler<SlotId>(id => new SlotId(id)));

        ValidateTypeHandlers();
    }

    private static void ValidateTypeHandlers()
    {
        var idTypeInterface = typeof(IIdType);
        var idTypes = typeof(IIdType).Assembly.GetTypes()
            .Where(t => idTypeInterface.IsAssignableFrom(t) && t is { IsInterface: false, IsAbstract: false })
            .ToList();

        foreach (var idType in idTypes)
        {
            if (!SqlMapper.HasTypeHandler(idType))
            {
                throw new InvalidOperationException($"No type handler registered for {idType.Name}");
            }
        }
    }
}

public static class Api
{
    public static void Map(WebApplication application)
    {
        application.MapGet("/", () => { })
            .WithName("Root")
            .WithOpenApi();

        application.MapGet("/salesmanagers", async (IDbConnection dbConnection) =>
            {
                var salesManagers = await dbConnection.QueryAsync<SalesManager>("SELECT * FROM sales_managers");
                return Results.Ok(salesManagers);
            })
            .WithName("GetSalesManagers")
            .WithOpenApi();
    }
}