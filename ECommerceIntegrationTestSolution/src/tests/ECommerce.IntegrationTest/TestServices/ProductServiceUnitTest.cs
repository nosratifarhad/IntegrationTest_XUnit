using ECommerce.Domain.Products;
using ECommerce.Service.Services;
using ECommerce.Domain.Products.Dtos.ProductDtos;
using ECommerce.UnitTest.MockDatas;
using ECommerce.Domain.Products.Entitys;
using FluentAssertions;
using Moq;

namespace ECommerce.UnitTest.TestServices
{
    public class ProductServiceUnitTest
    {
        #region [ GetProductAsync ]

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void When_ValidProductIdInGetProductAsync_Then_ReturnedProductViewModel(int productId)
        {
            var moqProductReadRepository = new Mock<IProductReadRepository>();
            var moqProductWriteRepository = new Mock<IProductWriteRepository>();

            moqProductReadRepository.Setup(p => p.GetProductAsync(It.IsAny<int>()))
                .ReturnsAsync(new ProductDto()
                {
                    ProductId = productId,
                    ProductName = "test",
                    ProductTitle = "tst"
                });

            var productService = new ProductService(moqProductReadRepository.Object, moqProductWriteRepository.Object);

            var productViewModel = await productService.GetProductAsync(productId).ConfigureAwait(false);

            productViewModel.ProductId.Should().Be(productId);
            productViewModel.ProductName.Should().NotBeNull();
            productViewModel.ProductTitle.Should().NotBeNull();
            productViewModel.Should().NotBeNull();
        }

        [Theory]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        public async void When_InValidProductIdInGetProductAsync_Then_ReturnedProductViewModel(int productId)
        {
            var moqProductReadRepository = new Mock<IProductReadRepository>();
            var moqProductWriteRepository = new Mock<IProductWriteRepository>();

            moqProductReadRepository.Setup(p => p.GetProductAsync(It.IsAny<int>()))
                .ReturnsAsync(new ProductDto());

            var productService = new ProductService(moqProductReadRepository.Object, moqProductWriteRepository.Object);

            var productViewModel = await productService.GetProductAsync(productId).ConfigureAwait(false);

            productViewModel.ProductId.Should().Be(0);
            productViewModel.ProductName.Should().BeNull();
            productViewModel.ProductTitle.Should().BeNull();
            productViewModel.Should().NotBeNull();
        }

        [Theory]
        [InlineData(0)]
        public async void When_ZeroProductIdInGetProductAsync_Then_ThrowArgumentExceptionBeMessageProductIdIsInvalid(int productId)
        {
            var moqProductReadRepository = new Mock<IProductReadRepository>();
            var moqProductWriteRepository = new Mock<IProductWriteRepository>();

            var productService = new ProductService(moqProductReadRepository.Object, moqProductWriteRepository.Object);

            var exception = await Assert.ThrowsAsync<ArgumentException>(
            async () =>
            {
                await productService.GetProductAsync(productId).ConfigureAwait(false);
            });

            exception.Message.Should().Be("Product Id Is Invalid");
        }

        #endregion [ GetProductAsync ]

        #region [ GetProductsAsync ]

        [Fact]
        public async void When_CallGetProductsAsync_Then_ReturnedCountZeroProductViewModelList()
        {
            var moqProductReadRepository = new Mock<IProductReadRepository>();
            var moqProductWriteRepository = new Mock<IProductWriteRepository>();

            moqProductReadRepository.Setup(p => p.GetProductsAsync())
                .ReturnsAsync(new List<ProductDto>());

            var productService = new ProductService(moqProductReadRepository.Object, moqProductWriteRepository.Object);

            var productViewModel = await productService.GetProductsAsync().ConfigureAwait(false);

            productViewModel.Should().NotBeNull();
            productViewModel.Should().HaveCount(0);
        }

        [Fact]
        public async void When_CallGetProductsAsync_Then_ReturnedProductViewModelList()
        {
            var moqProductReadRepository = new Mock<IProductReadRepository>();
            var moqProductWriteRepository = new Mock<IProductWriteRepository>();

            moqProductReadRepository.Setup(p => p.GetProductsAsync())
                .ReturnsAsync(new List<ProductDto>() { new ProductDto(), new ProductDto(), new ProductDto(), new ProductDto(), new ProductDto() });

            var productService = new ProductService(moqProductReadRepository.Object, moqProductWriteRepository.Object);

            var productViewModel = await productService.GetProductsAsync().ConfigureAwait(false);

            productViewModel.Should().NotBeNull();
            productViewModel.Should().HaveCount(5);
        }


        #endregion [ GetProductsAsync ]

        #region [ CreateProductAsync ]

        [Fact]
        public async void When_ValidCreateProductInputModelInCreateProductAsync_Then_ReturnCreatedProductId()
        {
            var moqProductReadRepository = new Mock<IProductReadRepository>();
            var moqProductWriteRepository = new Mock<IProductWriteRepository>();

            moqProductWriteRepository.Setup(p => p.CreateProductAsync(It.IsAny<Product>())).ReturnsAsync(1);

            var productService = new ProductService(moqProductReadRepository.Object, moqProductWriteRepository.Object);

            var validC1reateProductInputModel = ProductMockData.ValidCreateProductInputModel();

            var response = await productService.CreateProductAsync(validC1reateProductInputModel).ConfigureAwait(false);

            response.Should().Be(1);
            response.Should().NotBe(0);
        }

        [Fact]
        public async void When_ProductNameIsNullInC1reateProductInputModelInCreateProductAsync_Then_ProductNameMustNotBeEmptyThrowException()
        {
            var moqProductReadRepository = new Mock<IProductReadRepository>();
            var moqProductWriteRepository = new Mock<IProductWriteRepository>();

            var productService = new ProductService(moqProductReadRepository.Object, moqProductWriteRepository.Object);

            var invalidC1reateProductInputModel = ProductMockData.ProductNameIsNullInC1reateProductInputModel();

            var exception = await Assert.ThrowsAsync<ArgumentNullException>(
            async () =>
            {
                await productService.CreateProductAsync(invalidC1reateProductInputModel).ConfigureAwait(false);
            });

            exception.Message.Should().Be("Product Name must not be empty (Parameter 'productName')");
        }

        [Fact]
        public async void When_ProductTitleIsNullInCreateProductInputModelInCreateProductAsync_Then_ProductTitleMustNotBeEmptyThrowException()
        {
            var moqProductReadRepository = new Mock<IProductReadRepository>();
            var moqProductWriteRepository = new Mock<IProductWriteRepository>();

            var productService = new ProductService(moqProductReadRepository.Object, moqProductWriteRepository.Object);

            var invalidC1reateProductInputModel = ProductMockData.ProductTitleIsNullInCreateProductInputModel();

            var exception = await Assert.ThrowsAsync<ArgumentNullException>(
            async () =>
            {
                await productService.CreateProductAsync(invalidC1reateProductInputModel).ConfigureAwait(false);
            });

            exception.Message.Should().Be("Product Title must not be empty (Parameter 'productTitle')");
        }

        #endregion [ CreateProductAsync ]

        #region [ UpdateProductAsync ]

        [Fact]
        public async void When_ValidUpdateProductInputModelInUpdateProductAsync_Then_UpdatedDatabase()
        {
            var moqProductReadRepository = new Mock<IProductReadRepository>();
            var moqProductWriteRepository = new Mock<IProductWriteRepository>();

            var validUpdateProductInputModel = ProductMockData.ValidUpdateProductInputModel();

            moqProductReadRepository.Setup(p => p.GetProductAsync(It.IsAny<int>()))
                .ReturnsAsync(
                new ProductDto() 
                { 
                    ProductId = validUpdateProductInputModel.ProductId ,
                    ProductName = validUpdateProductInputModel.ProductName ,
                    ProductTitle = validUpdateProductInputModel.ProductTitle ,
                });

            moqProductReadRepository.Setup(p => p.IsExistProductAsync(It.IsAny<int>())).ReturnsAsync(true);
                
            moqProductWriteRepository.Setup(p => p.UpdateProductAsync(It.IsAny<Product>()));

            var productService = new ProductService(moqProductReadRepository.Object, moqProductWriteRepository.Object);


            await productService.UpdateProductAsync(validUpdateProductInputModel).ConfigureAwait(false);

            var productViewModel = await productService.GetProductAsync(validUpdateProductInputModel.ProductId).ConfigureAwait(false);

            productViewModel.ProductId.Should().Be(validUpdateProductInputModel.ProductId);
            productViewModel.ProductName.Should().Be(validUpdateProductInputModel.ProductName);
            productViewModel.ProductTitle.Should().Be(validUpdateProductInputModel.ProductTitle);
        }

        [Fact]
        public async void When_InValidProductIdInUpdateProductInputModelInUpdateProductAsync_Then_productIdIsNotFoundThrowException()
        {
            var moqProductReadRepository = new Mock<IProductReadRepository>();
            var moqProductWriteRepository = new Mock<IProductWriteRepository>();

            moqProductReadRepository.Setup(p => p.IsExistProductAsync(It.IsAny<int>())).ReturnsAsync(false);

            moqProductWriteRepository.Setup(p => p.UpdateProductAsync(It.IsAny<Product>()));

            var productService = new ProductService(moqProductReadRepository.Object, moqProductWriteRepository.Object);

            var invalidUpdateProductInputModel = ProductMockData.InValidProductIdInUpdateProductInputModel();
 
            var exception = await Assert.ThrowsAsync<ArgumentException>(
                async () =>
                {
                    await productService.UpdateProductAsync(invalidUpdateProductInputModel).ConfigureAwait(false);
                });

            exception.Message.Should().Be("productId Is Not Found.");
        }

        [Fact]
        public async void When_ZeroProductIdInUpdateProductInputModelInUpdateProductAsync_Then_ProductIdIsInValidThrowException()
        {
            var moqProductReadRepository = new Mock<IProductReadRepository>();
            var moqProductWriteRepository = new Mock<IProductWriteRepository>();

            moqProductWriteRepository.Setup(p => p.UpdateProductAsync(It.IsAny<Product>()));

            var productService = new ProductService(moqProductReadRepository.Object, moqProductWriteRepository.Object);

            var invalidUpdateProductInputModel = ProductMockData.ZeroProductIdInUpdateProductInputModel();

            var exception = await Assert.ThrowsAsync<ArgumentException>(
                async () =>
                {
                    await productService.UpdateProductAsync(invalidUpdateProductInputModel).ConfigureAwait(false);
                });

            exception.Message.Should().Be("ProductId Is Invalid.");
        }

        [Fact]
        public async void When_NegativeOneProductIdInUpdateProductInputModelInUpdateProductAsync_Then_ProductIdIsInValidThrowException()
        {
            var moqProductReadRepository = new Mock<IProductReadRepository>();
            var moqProductWriteRepository = new Mock<IProductWriteRepository>();

            moqProductWriteRepository.Setup(p => p.UpdateProductAsync(It.IsAny<Product>()));

            var productService = new ProductService(moqProductReadRepository.Object, moqProductWriteRepository.Object);

            var invalidUpdateProductInputModel = ProductMockData.NegativeOneProductIdInUpdateProductInputModel();

            var exception = await Assert.ThrowsAsync<ArgumentException>(
                async () =>
                {
                    await productService.UpdateProductAsync(invalidUpdateProductInputModel).ConfigureAwait(false);
                });

            exception.Message.Should().Be("ProductId Is Invalid.");
        }

        [Fact]
        public async void When_ProductNameIsNullInUpdateProductInputModelInUpdateProductAsync_Then_ProductNameMustNotBeEmptyThrowException()
        {
            var moqProductReadRepository = new Mock<IProductReadRepository>();
            var moqProductWriteRepository = new Mock<IProductWriteRepository>();

            moqProductWriteRepository.Setup(p => p.UpdateProductAsync(It.IsAny<Product>()));

            var productService = new ProductService(moqProductReadRepository.Object, moqProductWriteRepository.Object);

            var invalidUpdateProductInputModel = ProductMockData.ProductNameIsNullInUpdateProductInputModel();

            var exception = await Assert.ThrowsAsync<ArgumentNullException>(
            async () =>
            {
                await productService.UpdateProductAsync(invalidUpdateProductInputModel).ConfigureAwait(false);
            });

            exception.Message.Should().Be("Product Name must not be empty (Parameter 'productName')");
        }

        [Fact]
        public async void When_ProductTitleIsNullInUpdateProductInputModelInUpdateProductAsync_Then_ProductTitleMustNotBeEmptyThrowException()
        {
            var moqProductReadRepository = new Mock<IProductReadRepository>();
            var moqProductWriteRepository = new Mock<IProductWriteRepository>();

            moqProductWriteRepository.Setup(p => p.UpdateProductAsync(It.IsAny<Product>()));

            var productService = new ProductService(moqProductReadRepository.Object, moqProductWriteRepository.Object);

            var invalidUpdateProductInputModel = ProductMockData.ProductTitleIsNullInUpdateProductInputModel();

            var exception = await Assert.ThrowsAsync<ArgumentNullException>(
            async () =>
            {
                await productService.UpdateProductAsync(invalidUpdateProductInputModel).ConfigureAwait(false);
            });

            exception.Message.Should().Be("Product Title must not be empty (Parameter 'productTitle')");
        }

        #endregion [ UpdateProductAsync ]

        #region [ DeleteProductAsync ]

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void When_ValidProductIdInDeleteProductAsync_Then_RemoveFromDataBase(int productId)
        {
            var moqProductReadRepository = new Mock<IProductReadRepository>();
            var moqProductWriteRepository = new Mock<IProductWriteRepository>();

            moqProductReadRepository.Setup(p => p.GetProductAsync(It.IsAny<int>())).ReturnsAsync(new ProductDto());

            moqProductReadRepository.Setup(p => p.IsExistProductAsync(It.IsAny<int>())).ReturnsAsync(true);

            var productService = new ProductService(moqProductReadRepository.Object, moqProductWriteRepository.Object);

            await productService.DeleteProductAsync(productId).ConfigureAwait(false);

            var productViewModel = await productService.GetProductAsync(productId).ConfigureAwait(false);

            productViewModel.ProductId.Should().Be(0);
            productViewModel.ProductName.Should().Be(null);
            productViewModel.ProductTitle.Should().Be(null);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async void When_ZeroProductIdInDeleteProductAsync_Then_ProductIdIsInValidThrowException(int productId)
        {
            var moqProductReadRepository = new Mock<IProductReadRepository>();
            var moqProductWriteRepository = new Mock<IProductWriteRepository>();

            var productService = new ProductService(moqProductReadRepository.Object, moqProductWriteRepository.Object);

            var exception = await Assert.ThrowsAsync<ArgumentException>(
                async () =>
                {
                    await productService.DeleteProductAsync(productId).ConfigureAwait(false);
                });

            exception.Message.Should().Be("ProductId Is Invalid.");
        }

        [Theory]
        [InlineData(8)]
        [InlineData(9)]
        public async void When_InValidProductIdInDeleteProductAsync_Then_ProductIdIsNotFoundThrowException(int productId)
        {
            var moqProductReadRepository = new Mock<IProductReadRepository>();
            var moqProductWriteRepository = new Mock<IProductWriteRepository>();

            moqProductReadRepository.Setup(p => p.IsExistProductAsync(It.IsAny<int>())).ReturnsAsync(false);

            var productService = new ProductService(moqProductReadRepository.Object, moqProductWriteRepository.Object);

            var exception = await Assert.ThrowsAsync<ArgumentException>(
                async () =>
                {
                    await productService.DeleteProductAsync(productId).ConfigureAwait(false);
                });

            exception.Message.Should().Be("productId Is Not Found.");
        }

        #endregion [ DeleteProductAsync ]
    }
}