using System.Data;
using Dapper;
using Npgsql;
using YF.EnpalChallange.Api.Model;
using YF.EnpalChallange.Api.Model.Contract;

namespace YF.EnpalChallange.Api.Persistence;

public static class PersistenceModuleDefinition
{
    public static IServiceCollection AddPersistenceModule(this IServiceCollection services, IConfiguration configuration)
    {
        ConfigureDb(services, configuration);
        services.AddScoped<ManagerRepository>();
        return services;
    }

    private static void ConfigureDb(IServiceCollection services, IConfiguration configuration)
    {
        // Add the database connection
        services.AddScoped<IDbConnection>(_ =>
            new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")));

        SqlMapper.AddTypeHandler(new IdTypeHandler<SalesManagerId>(id => new SalesManagerId(id)));
        SqlMapper.AddTypeHandler(new IdTypeHandler<SlotId>(id => new SlotId(id)));
        DefaultTypeMap.MatchNamesWithUnderscores = true;

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