using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using YF.EnpalChallenge.Api;

namespace YF.EnpalChallenge.Tests;

public class ApiTestingClientFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("ApiTesting");
        // Read appsettings.ApiTesting.json and add to builder

    }
}