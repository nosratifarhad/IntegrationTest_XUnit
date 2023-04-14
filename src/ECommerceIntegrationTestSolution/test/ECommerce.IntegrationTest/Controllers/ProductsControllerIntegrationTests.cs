using Bogus;
using ECommerce.Api;
using ECommerce.IntegrationTest.Extensions;
using ECommerce.IntegrationTest.MockDatas;
using ECommerce.Service.InputModels.ProductInputModels;
using ECommerce.Service.ViewModels.ProductViewModels;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Text;
using Assert = Xunit.Assert;

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

            var responseModel = await response.ToResponseModel<IEnumerable<ProductViewModel>>();

            responseModel.Should().NotBeNull();
            responseModel.Should().HaveCount(5);
        }

        #endregion [ GetProducts ] 

        #region [ CreateProduct ]

        [Fact]
        public async Task When_ValidCreateProductInputModelInCreateProduct_Then_CreatestProductInDataBase()
        {
            var mockInputModel = ProductMockData.ValidCreateProductInputModel().ToRequestModel();

            var response = await _client.PostAsync("/api/product", mockInputModel).ConfigureAwait(false);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
            response.IsSuccessStatusCode.Should().BeTrue();

            var productId = await response.ToRequestItem("ProductId");

            var newProductViewModel = await GetProductByResponseHeadersLocation(response.Headers.Location.AbsoluteUri).ConfigureAwait(false);

            newProductViewModel.Should().NotBeNull();
            newProductViewModel.ProductId.Should().Be(productId);
        }


        [Fact]
        [ExpectedException(typeof(ArgumentException),"Product Name cannot be nul")]
        public async Task When_ProductNameIsNullInC1reateProductInputModelInCreateProduct_Then_ProductNameCannotBeNullThrowException()
        {
            var mockInputModel = ProductMockData.ProductNameIsNullInC1reateProductInputModel().ToRequestModel();

            Action action = () => _client.PostAsync("/api/product", mockInputModel).GetAwaiter().GetResult();

            action.Should().Throw<ArgumentException>();

            //Assert.ThrowsAsync<ArgumentException>(async () => await _client.PostAsync("/api/product", mockInputModel));

            //_client.Invoking(a => a.PostAsync("/api/product", mockInputModel).GetAwaiter().GetResult())
            //    .Should().Throw<ArgumentException>().WithMessage("Product Name cannot be null");

            //Action action = async () => await _client.PostAsync("/api/product", mockInputModel);
            //action.Should().Throw<ArgumentException>();

            // Asserts that calling GetId() will throw
            //FluentActions.Invoking(() => _client.PostAsync("/api/product", mockInputModel)).Should().ThrowAsync<NullReferenceException>();

            //Func<Task> act = async () => await _client.PostAsync("/api/product", mockInputModel);
            //act.Should().ThrowAsync<NullReferenceException>();

            //Action action = async () => await _client.PostAsync("/api/product", mockInputModel);
            //action.Should().Throw<NullReferenceException>().Where(ex => ex.Message != null).Where(ex => ex.Data.Count > 0);
        }

        [Fact]
        [ExpectedException(typeof(ArgumentException), "Product Title cannot be null.")]
        public async Task When_ProductTitleIsNullInCreateProductInputModelInCreateProduct_Then_ProductTitleCannotBeNullThrowException()
        {
            var mockInputModel = ProductMockData.ProductTitleIsNullInCreateProductInputModel().ToRequestModel();

            Func<Task> f = async () => { await _client.PostAsync("/api/product", mockInputModel).ConfigureAwait(false); };

            f.Should()?.ThrowAsync<ArgumentException>()
                .WithParameterName("Product Title cannot be null.");

        }


        #endregion [ CreateProduct ]


        #region [ Private ]

        private async Task<ProductViewModel> GetProductByResponseHeadersLocation(string location)
        {
            var responseProductViewModel = await _client.GetAsync(location).ConfigureAwait(false);

            var newProductViewModel = await responseProductViewModel.ToResponseModel<ProductViewModel>();

            return newProductViewModel;
        }

        #endregion [ Private ]

    }
}