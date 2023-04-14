using ECommerce.Api;
using Microsoft.AspNetCore.Mvc.Testing;

namespace ECommerce.IntegrationTest
{
    public class ProductsControllerIntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        #region Fields
        private readonly HttpClient _client;

        #endregion Fields

        #region Ctor
        public ProductsControllerIntegrationTests(WebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }
        #endregion Ctor

        [Fact]
        public async Task Test1()
        {
            var response = await _client.GetAsync("/api/products");

            //var responseString = await response.Content.ReadAsStringAsync();

            //// Assert
            //Assert.Equal("Hello World!",
            //    responseString);
        }
    }
}