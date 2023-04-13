using ECommerce.Infra.Repositorys;
using ECommerce.UnitTest.MockDatas;
using FluentAssertions;

namespace ECommerce.UnitTest.TestRepositorys
{
    public class ProductWriteRepositoryUnitTest
    {
        #region [ CreateProductAsync ]

        [Fact]
        public async void When_ValidProductInCreateProductAsync_Then_CreatedInDatabase()
        {
            var validProduct = ProductMockData.CreateProduct();

            var productWriteRepository = new ProductWriteRepository();

            var productViewModel = await productWriteRepository.CreateProductAsync(validProduct).ConfigureAwait(false);

            productViewModel.Should().BeGreaterThan(1);
            productViewModel.Should().BeLessThanOrEqualTo(20);
        }

        #endregion [ CreateProductAsync ]

        #region [ DeleteProductAsync ]

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void When_ValidProductIdInDeleteProductAsync_Then_CreatedInDatabase(int productId)
        {
            var validProduct = ProductMockData.CreateProduct();

            var productWriteRepository = new ProductWriteRepository();

             await productWriteRepository.DeleteProductAsync(productId).ConfigureAwait(false);
        }

        #endregion [ DeleteProductAsync ]

        #region [ UpdateProductAsync ]

        [Fact]
        public async void When_ValidProductInUpdateProductAsync_Then_CreatedInDatabase()
        {
            var validProduct = ProductMockData.UpdateProduct();

            var productWriteRepository = new ProductWriteRepository();

            await productWriteRepository.UpdateProductAsync(validProduct).ConfigureAwait(false);
        }

        #endregion [ UpdateProductAsync ]
    }
}
