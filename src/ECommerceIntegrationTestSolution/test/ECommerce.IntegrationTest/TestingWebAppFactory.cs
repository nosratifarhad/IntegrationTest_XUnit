using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace ECommerce.IntegrationTest
{
    public class TestingWebAppFactory<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {

        }
    }
}