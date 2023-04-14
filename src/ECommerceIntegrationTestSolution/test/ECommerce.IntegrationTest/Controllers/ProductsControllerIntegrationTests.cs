using ECommerce.Api;
using ECommerce.IntegrationTest.Extensions;
using ECommerce.Service.ViewModels.ProductViewModels;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace ECommerce.IntegrationTest.Controllers
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

        #region [ GetProducts ] 
        
        [Fact]
        public async Task When_CallGetProducts_Then_ReturnedproductViewModelList()
        {
            var response = await _client.GetAsync("/api/products").ConfigureAwait(false);

            var responseModel = await response.ResponseToViewModel<IEnumerable<ProductViewModel>>();

            responseModel.Should().NotBeNull();
            responseModel.Should().HaveCount(5);
        }

        #endregion [ GetProducts ] 
    }
}