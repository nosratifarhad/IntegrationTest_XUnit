using ECommerce.Api;
using ECommerce.IntegrationTest.Extensions;
using ECommerce.IntegrationTest.MockDatas;
using ECommerce.Service.ViewModels.ProductViewModels;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            response.IsSuccessStatusCode.Should().BeTrue();

            responseModel.Should().NotBeNull();
            responseModel.Should().HaveCount(5);
        }

        #endregion [ GetProducts ] 

        #region [ CreateProduct ]

        [Fact]
        public async Task When_ValidCreateProductInputModelInCreateProduct_Then_CreateProductInDataBase()
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
        [ExpectedException(typeof(ArgumentException), "Product Name cannot be nul")]
        public void When_ProductNameIsNullInC1reateProductInputModelInCreateProduct_Then_ProductNameCannotBeNullThrowException()
        {
            var mockInputModel = ProductMockData.ProductNameIsNullInC1reateProductInputModel().ToRequestModel();

            Func<Task> func = async () => await _client.PostAsync("/api/product", mockInputModel).ConfigureAwait(false); ;

            func.Should()?.ThrowAsync<ArgumentException>()
                .WithParameterName("Product Name cannot be null.");

            //Action action = () => _client.PostAsync("/api/product", mockInputModel).GetAwaiter().GetResult();
            //action.Should().Throw<ArgumentException>();

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
        public void When_ProductTitleIsNullInCreateProductInputModelInCreateProduct_Then_ProductTitleCannotBeNullThrowException()
        {
            var mockInputModel = ProductMockData.ProductTitleIsNullInCreateProductInputModel().ToRequestModel();

            Func<Task> func = async () => await _client.PostAsync("/api/product", mockInputModel).ConfigureAwait(false);

            func.Should()?.ThrowAsync<ArgumentException>()
                .WithParameterName("Product Title cannot be null.");

        }

        #endregion [ CreateProduct ]

        #region [ GetProduct ]

        [Fact]
        public async Task When_ValidProductIdInGetProduct_Then_ReturnedProductViewModel()
        {
            int productId = await GetValidProductId();

            var response = await _client.GetAsync($"/api/product/{productId}").ConfigureAwait(false);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            response.IsSuccessStatusCode.Should().BeTrue();

            var responseModel = await response.ToResponseModel<ProductViewModel>();

            responseModel.Should().NotBeNull();
            responseModel.ProductId.Should().Be(productId);
        }

        [Fact]
        public async Task When_InValidProductIdInGetProduct_Then_ReturnedProductViewModel()
        {
            int productId = await GetValidProductId();
            int invalidProductId = (productId + 10);

            var response = await _client.GetAsync($"/api/product/{invalidProductId}").ConfigureAwait(false);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            response.IsSuccessStatusCode.Should().BeTrue();

            var responseModel = await response.ToResponseModel<ProductViewModel>();

            responseModel.Should().NotBeNull();
            responseModel.ProductId.Should().Be(0);
            responseModel.ProductId.Should().NotBe(productId);
        }

        [Fact]
        [ExpectedException(typeof(ArgumentException), "Product Id Is Invalid")]
        public void When_ZeroProductIdInGetProduct_Then_ProductTitleCannotBeNullThrowException()
        {
            int productId = ProductMockData.Zero;

            Func<Task> func = async () => { await _client.GetAsync($"/api/product/{productId}"); };

            func.Should().ThrowAsync<ArgumentException>()
                .WithParameterName("Product Id Is Invalid");
        }

        [Fact]
        [ExpectedException(typeof(ArgumentException), "Product Id Is Invalid")]
        public void When_NegativeOneProductIdInGetProduct_Then_ProductTitleCannotBeNullThrowException()
        {
            int productId = ProductMockData.NegativeOne;

            Func<Task> func = () => _client.GetAsync($"/api/product/{productId}");

            func.Should()?.ThrowAsync<ArgumentException>()
                .WithParameterName("Product Id Is Invalid");

        }

        #endregion [ GetProduct ]

        #region [ UpdateProduct ]

        [Fact]
        public async Task When_ValidUpdateProductInputModelInUpdateProduct_Then_UpdateProductInDataBase()
        {
            var validUpdateProductInputModel = ProductMockData.ValidUpdateProductInputModel();

            int productId = validUpdateProductInputModel.ProductId;

            var mockInputModel = validUpdateProductInputModel.ToRequestModel();

            var response = await _client.PutAsync($"/api/product/{productId}", mockInputModel).ConfigureAwait(false);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
            response.IsSuccessStatusCode.Should().BeTrue();

            var newProductViewModel = await GetProductByProductId(productId).ConfigureAwait(false);

            newProductViewModel.Should().NotBeNull();
            newProductViewModel.ProductId.Should().Be(productId);
        }

        [Fact]
        [ExpectedException(typeof(ArgumentException), "Product Name cannot be nul")]
        public void When_ProductNameIsNullInUpdateProductInputModelInUpdateProduct_Then_ProductNameCannotBeNullThrowException()
        {
            var validUpdateProductInputModel = ProductMockData.ProductNameIsNullInUpdateProductInputModel();

            int productId = validUpdateProductInputModel.ProductId;

            var mockInputModel = validUpdateProductInputModel.ToRequestModel();

            Func<Task> func = async () => await _client.PutAsync($"/api/product/{productId}", mockInputModel).ConfigureAwait(false); ;

            func.Should()?.ThrowAsync<ArgumentException>()
                .WithParameterName("Product Name cannot be null.");
        }

        [Fact]
        [ExpectedException(typeof(ArgumentException), "Product Title cannot be null.")]
        public void When_ProductTitleIsNullInUpdateProductInputModelInUpdateProduct_Then_ProductTitleCannotBeNullThrowException()
        {
            var validUpdateProductInputModel = ProductMockData.ProductTitleIsNullInUpdateProductInputModel();

            int productId = validUpdateProductInputModel.ProductId;

            var mockInputModel = validUpdateProductInputModel.ToRequestModel();

            Func<Task> func = async () => await _client.PutAsync($"/api/product/{productId}", mockInputModel).ConfigureAwait(false); ;

            func.Should()?.ThrowAsync<ArgumentException>()
                .WithParameterName("Product Title cannot be null.");
        }

        [Fact]
        [ExpectedException(typeof(ArgumentException), "ProductId Is Not Found.")]
        public void When_InValidProductIdInUpdateProductInputModelInUpdateProduct_Then_ProductIdIsNotFoundThrowException()
        {
            var validUpdateProductInputModel = ProductMockData.InValidProductIdInUpdateProductInputModel();

            int productId = validUpdateProductInputModel.ProductId;

            var mockInputModel = validUpdateProductInputModel.ToRequestModel();

            Func<Task> func = async () => await _client.PutAsync($"/api/product/{productId}", mockInputModel).ConfigureAwait(false); ;

            func.Should()?.ThrowAsync<ArgumentException>()
                .WithParameterName("ProductId Is Not Found.");
        }

        [Fact]
        [ExpectedException(typeof(ArgumentException), "ProductId Is Invalid.")]
        public void When_ZeroProductIdInUpdateProductInputModelInUpdateProduct_Then_ProductIdIsInvalidThrowException()
        {
            var validUpdateProductInputModel = ProductMockData.ZeroProductIdInUpdateProductInputModel();

            int productId = validUpdateProductInputModel.ProductId;

            var mockInputModel = validUpdateProductInputModel.ToRequestModel();

            Func<Task> func = async () => await _client.PutAsync($"/api/product/{productId}", mockInputModel);

            func.Should()?.ThrowAsync<ArgumentException>()
                .WithParameterName("ProductId Is Invalid.");
        }

        [Fact]
        [ExpectedException(typeof(ArgumentException), "ProductId Is Invalid.")]
        public void When_NegativeOneProductIdInUpdateProductInputModelInUpdateProduct_Then_ProductIdIsInvalidThrowException()
        {
            var validUpdateProductInputModel = ProductMockData.NegativeOneProductIdInUpdateProductInputModel();

            int productId = validUpdateProductInputModel.ProductId;

            var mockInputModel = validUpdateProductInputModel.ToRequestModel();

            Func<Task> func = async () => await _client.PutAsync($"/api/product/{productId}", mockInputModel);

            func.Should()?.ThrowAsync<ArgumentException>()
                .WithParameterName("ProductId Is Invalid.");
        }

        #endregion [ UpdateProduct ]

        #region [ DeleteProduct ]

        [Fact]
        public async Task When_ValidProductIdInDeleteProduct_Then_RemoveProductFromDataBase()
        {
            int productId = await GetValidProductId();

            var response = await _client.DeleteAsync($"/api/product/{productId}").ConfigureAwait(false);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
            response.IsSuccessStatusCode.Should().BeTrue();

            var newProductViewModel = await GetProductByProductId(productId).ConfigureAwait(false);
            
            newProductViewModel.Should().NotBeNull();
            newProductViewModel.ProductId.Should().NotBe(0);
            newProductViewModel.ProductId.Should().Be(productId);
        }

        [Fact]
        [ExpectedException(typeof(ArgumentException), "ProductId Is Not Found.")]
        public async Task When_InValidProductIdInDeleteProduct_Then_ProductIdIsNotFoundThrowException()
        {
            int productId = await GetValidProductId();
            int invalidProductId = (productId + 10);

            Func<Task> func = async () =>  await _client.DeleteAsync($"/api/product/{productId}");

            func.Should()?.ThrowAsync<ArgumentException>()
                .WithParameterName("ProductId Is Not Found.");

        }

        [Fact]
        [ExpectedException(typeof(ArgumentException), "ProductId Is Invalid.")]
        public void When_ZeroProductIdInDeleteProduct_Then_ProductIdIsInvalidThrowException()
        {
            int productId = ProductMockData.Zero;

            Func<Task> func = async () => await _client.DeleteAsync($"/api/product/{productId}");

            func.Should().ThrowAsync<ArgumentException>()
                .WithParameterName("ProductId Is Invalid.");
        }

        [Fact]
        [ExpectedException(typeof(ArgumentException), "Product Id Is Invalid")]
        public void When_NegativeOneProductIdInDeleteProduct_Then_ProductIdIsInvalidThrowException()
        {
            int productId = ProductMockData.NegativeOne;

            Func<Task> func = async () => await _client.DeleteAsync($"/api/product/{productId}");

            func.Should()?.ThrowAsync<ArgumentException>()
                .WithParameterName("ProductId Is Invalid.");

        }

        #endregion [ DeleteProduct ]


        #region [ Private ]

        private async Task<ProductViewModel> GetProductByResponseHeadersLocation(string location)
        {
            var responseProductViewModel = await _client.GetAsync(location).ConfigureAwait(false);

            var newProductViewModel = await responseProductViewModel.ToResponseModel<ProductViewModel>();

            return newProductViewModel;
        }

        private async Task<ProductViewModel> GetProductByProductId(int productId)
        {
            var responseProductViewModel = await _client.GetAsync($"/api/product/{productId}").ConfigureAwait(false);

            var newProductViewModel = await responseProductViewModel.ToResponseModel<ProductViewModel>();

            return newProductViewModel;
        }

        [Fact]
        public async Task<int> GetValidProductId()
        {
            var mockInputModel = ProductMockData.ValidCreateProductInputModel().ToRequestModel();

            var response = await _client.PostAsync("/api/product", mockInputModel).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var productId = await response.ToRequestItem("ProductId");

            return productId;
        }


        #endregion [ Private ]

    }
}