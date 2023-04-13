using ECommerce.Domain.Products.Dtos.ProductDtos;

namespace ECommerce.Domain.Products
{
    public interface IProductReadRepository
    {
        Task<ProductDto> GetProductAsync(int productId);
        
        Task<IEnumerable<ProductDto>> GetProductsAsync();

        Task<bool> IsExistProductAsync(int productId);

    }
}
