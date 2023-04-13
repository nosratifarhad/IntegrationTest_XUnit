using ECommerce.Domain.Products.Dtos.ProductDtos;
using ECommerce.Domain.Products;
using Moq;
using ECommerce.Infra.Repositorys;
using FluentAssertions;

namespace ECommerce.UnitTest.TestRepositorys
{
    public class ProductReadRepositoryUnitTest
    {
        #region [ GetProductAsync ]

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void When_ValidProductIdInGetProductAsync_Then_ReturnedProductViewModel(int productId)
        {
            var productReadRepository = new ProductReadRepository();

            var productViewModel = await productReadRepository.GetProductAsync(productId).ConfigureAwait(false);

            productViewModel.ProductId.Should().Be(productId);
            productViewModel.ProductName.Should().NotBeNull();
            productViewModel.ProductTitle.Should().NotBeNull();
            productViewModel.Should().NotBeNull();
        }

        [Theory]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        public async void When_InValidProductIdInGetProductAsync_Then_ReturnedNullProductViewModel(int productId)
        {
            var productReadRepository = new ProductReadRepository();

            var productViewModel = await productReadRepository.GetProductAsync(productId).ConfigureAwait(false);

            productViewModel.ProductId.Should().Be(0);
            productViewModel.ProductName.Should().BeNull();
            productViewModel.ProductTitle.Should().BeNull();
            productViewModel.Should().NotBeNull();
        }

        #endregion [ GetProductAsync ]

        #region [ GetProductsAsync ]

        [Fact]
        public async void When_CallGetProductsAsync_Then_ReturnedProductViewModelLisr()
        {
            var productReadRepository = new ProductReadRepository();

            var productViewModels = await productReadRepository.GetProductsAsync().ConfigureAwait(false);

            productViewModels.Should().HaveCount(5);
            productViewModels.Should().NotBeNull();
        }

        #endregion [ GetProductsAsync ]

        #region [ IsExistProductAsync ]

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void When_ValidProductIdInIsExistProductAsync_Then_ReturnedTrue(int productId)
        {
            var productReadRepository = new ProductReadRepository();

            var productViewModel = await productReadRepository.IsExistProductAsync(productId).ConfigureAwait(false);

            productViewModel.Should().BeTrue();
        }

        [Theory]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        public async void When_InValidProductIdInIsExistProductAsync_Then_ReturnedFalse(int productId)
        {
            var productReadRepository = new ProductReadRepository();

            var productViewModel = await productReadRepository.IsExistProductAsync(productId).ConfigureAwait(false);

            productViewModel.Should().BeFalse();
        }

        #endregion [ IsExistProductAsync ]

    }
}
