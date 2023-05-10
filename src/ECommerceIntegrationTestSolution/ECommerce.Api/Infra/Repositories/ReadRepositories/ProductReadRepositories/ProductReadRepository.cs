using Bogus;
using ECommerce.Api.Domain;
using ECommerce.Api.Domain.Entitys;
using ECommerce.Api.Domain.Enums;

namespace ECommerce.Api.Infra.Repositories.ReadRepositories.ProductReadRepositories
{
    public class ProductReadRepository : IProductReadRepository
    {
        public async Task<Product> GetProductAsync(int productId)
        {
            if (productId < 5)
            {
                return await Task.FromResult(CreateFakerProduct(productId));
            }
            return await Task.FromResult<Product>(null);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await Task.FromResult((IEnumerable<Product>)CreateFakerProducts());
        }

        public async Task<bool> IsExistProductAsync(int productId)
        {
            if (productId < 5)
                return await Task.FromResult(true);

            return await Task.FromResult(false);
        }

        #region FakeData

        private static Product CreateFakerProduct(int productId)
            => new Faker<Product>().CustomInstantiator(f
                => new Product(
                    productId,
                    f.Name.FirstName(),
                    f.Name.JobTitle(),
                    f.Name.JobDescriptor(),
                    f.Random.Enum<ProductCategory>(),
                    f.Name.FullName(),
                    f.Name.FullName(),
                    f.Name.FullName(),
                    f.Random.Enum<ProductColor>(),
                    f.Random.Bool(),
                    f.Random.Bool(),
                    f.Random.Number()));

        private static List<Product> CreateFakerProducts()
          => new Faker<Product>().CustomInstantiator(f
                => new Product(
                    f.Random.Number(1, 5),
                    f.Name.FirstName(),
                    f.Name.JobTitle(),
                    f.Name.JobDescriptor(),
                    f.Random.Enum<ProductCategory>(),
                    f.Name.FullName(),
                    f.Name.FullName(),
                    f.Name.FullName(),
                    f.Random.Enum<ProductColor>(),
                    f.Random.Bool(),
                    f.Random.Bool(),
                    f.Random.Number())).Generate(5);

        #endregion FakeData
    }
}
