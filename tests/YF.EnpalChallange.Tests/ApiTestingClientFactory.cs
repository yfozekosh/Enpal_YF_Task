using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using YF.EnpalChallange.Api;

namespace YF.EnpalChallange.Tests;

public class ApiTestingClientFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("ApiTesting");
        // Read appsettings.ApiTesting.json and add to builder

    }
}