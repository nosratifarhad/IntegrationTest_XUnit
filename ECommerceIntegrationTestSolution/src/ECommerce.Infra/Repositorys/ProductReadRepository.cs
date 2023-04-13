using Bogus;
using ECommerce.Domain.Products;
using ECommerce.Domain.Products.Dtos.ProductDtos;
using ECommerce.Domain.Products.Enums;

namespace ECommerce.Infra.Repositorys
{
    public class ProductReadRepository : IProductReadRepository
    {
        public async Task<ProductDto> GetProductAsync(int productId)
        {
            if (productId < 5)
                return await Task.FromResult(CreateFakerProductDto(productId));

            return await Task.FromResult(new ProductDto());
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            return await Task.FromResult((IEnumerable<ProductDto>)CreateFakerProductDtos());
        }

        public async Task<bool> IsExistProductAsync(int productId)
        {
            if (productId < 5)
                return await Task.FromResult(true);

            return await Task.FromResult(false);
        }

        #region FakeData

        private static ProductDto CreateFakerProductDto(int productId)
           => new Faker<ProductDto>()
              .RuleFor(bp => bp.ProductId, productId)
              .RuleFor(bp => bp.ProductName, f => f.Name.FirstName())
              .RuleFor(bp => bp.ProductTitle, f => f.Name.JobTitle())
              .RuleFor(bp => bp.ProductDescription, f => f.Name.JobDescriptor())
              .RuleFor(bp => bp.ProductCategory, f => f.Random.Enum<ProductCategory>())
              .RuleFor(bp => bp.MainImageName, f => f.Name.FullName())
              .RuleFor(bp => bp.MainImageTitle, f => f.Name.FullName())
              .RuleFor(bp => bp.MainImageUri, f => f.Name.FullName())
              .RuleFor(bp => bp.Color, f => f.Random.Enum<ProductColor>())
              .RuleFor(bp => bp.IsFreeDelivery, f => f.Random.Bool())
              .RuleFor(bp => bp.IsExisting, f => f.Random.Bool())
              .RuleFor(bp => bp.Weight, f => f.Random.Number());

        private static List<ProductDto> CreateFakerProductDtos()
           => new Faker<ProductDto>()
              .RuleFor(bp => bp.ProductId, f => f.Random.Number())
              .RuleFor(bp => bp.ProductName, f => f.Name.FirstName())
              .RuleFor(bp => bp.ProductTitle, f => f.Name.JobTitle())
              .RuleFor(bp => bp.ProductDescription, f => f.Name.JobDescriptor())
              .RuleFor(bp => bp.ProductCategory, f => f.Random.Enum<ProductCategory>())
              .RuleFor(bp => bp.MainImageName, f => f.Name.FullName())
              .RuleFor(bp => bp.MainImageTitle, f => f.Name.FullName())
              .RuleFor(bp => bp.MainImageUri, f => f.Name.FullName())
              .RuleFor(bp => bp.Color, f => f.Random.Enum<ProductColor>())
              .RuleFor(bp => bp.IsFreeDelivery, f => f.Random.Bool())
              .RuleFor(bp => bp.IsExisting, f => f.Random.Bool())
              .RuleFor(bp => bp.Weight, f => f.Random.Number()).Generate(5);

        #endregion FakeData
    }
}
