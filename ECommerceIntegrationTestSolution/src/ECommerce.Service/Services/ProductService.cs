using ECommerce.Domain.Products;
using ECommerce.Domain.Products.Dtos.ProductDtos;
using ECommerce.Domain.Products.Entitys;
using ECommerce.Service.Contract;
using ECommerce.Service.InputModels.ProductInputModels;
using ECommerce.Service.ViewModels.ProductViewModels;

namespace ECommerce.Service.Services
{
    public class ProductService : IProductService
    {
        #region Fields
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;
        #endregion Fields

        #region Ctor

        public ProductService(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }

        #endregion Ctor

        #region Implement

        public async Task<ProductViewModel> GetProductAsync(int productId)
        {
            if (productId <= 0)
                throw new ArgumentException("Product Id Is Invalid");

            var productDto = await _productReadRepository.GetProductAsync(productId).ConfigureAwait(false);
            if (productDto == null)
                return new ProductViewModel();

            var productViewModel = CreateProductViewModelFromProductDto(productDto);

            return productViewModel;
        }

        public async Task<IEnumerable<ProductViewModel>> GetProductsAsync()
        {
            var productDtos = await _productReadRepository.GetProductsAsync().ConfigureAwait(false);

            if (productDtos == null || productDtos.Count() == 0)
                return Enumerable.Empty<ProductViewModel>();

            var productViewModels = CreateProductViewModelsFromProductDtos(productDtos);

            return productViewModels;
        }

        public async Task<int> CreateProductAsync(CreateProductInputModel inputModel)
        {
            ValidateProductName(inputModel.ProductName);

            ValidateProductTitle(inputModel.ProductTitle);

            var productEntoty = CreateProductEntityFromInputModel(inputModel);

            return await _productWriteRepository.CreateProductAsync(productEntoty).ConfigureAwait(false);
        }

        public async Task UpdateProductAsync(UpdateProductInputModel inputModel)
        {
            if (inputModel.ProductId <= 0)
                throw new ArgumentException("ProductId Is Invalid.");

            ValidateProductName(inputModel.ProductName);

            ValidateProductTitle(inputModel.ProductTitle);

            await IsExistProduct(inputModel.ProductId).ConfigureAwait(false);

            var productEntoty = CreateProductEntityFromInputModel(inputModel);

            await _productWriteRepository.UpdateProductAsync(productEntoty).ConfigureAwait(false);
        }

        public async Task DeleteProductAsync(int productId)
        {
            if (productId <= 0)
                throw new ArgumentException("ProductId Is Invalid.");

            await IsExistProduct(productId).ConfigureAwait(false);

            await _productWriteRepository.DeleteProductAsync(productId).ConfigureAwait(false);
        }

        #endregion Implement

        #region Private

        private async Task IsExistProduct(int productId)
        {
            var isExistProduct = await _productReadRepository.IsExistProductAsync(productId).ConfigureAwait(false);
            if (isExistProduct == false)
                throw new ArgumentException("productId Is Not Found.");
        }

        private Product CreateProductEntityFromInputModel(CreateProductInputModel inputModel)
            => new Product(inputModel.ProductName, inputModel.ProductTitle, inputModel.ProductDescription, inputModel.ProductCategory, inputModel.MainImageName, inputModel.MainImageTitle, inputModel.MainImageUri, inputModel.Color, inputModel.IsExisting, inputModel.IsFreeDelivery, inputModel.Weight);

        private Product CreateProductEntityFromInputModel(UpdateProductInputModel inputModel)
            => new Product(inputModel.ProductId, inputModel.ProductName, inputModel.ProductTitle, inputModel.ProductDescription, inputModel.ProductCategory, inputModel.MainImageName, inputModel.MainImageTitle, inputModel.MainImageUri, inputModel.Color, inputModel.IsExisting, inputModel.IsFreeDelivery, inputModel.Weight);

        private ProductViewModel CreateProductViewModelFromProductDto(ProductDto dto)
            => new ProductViewModel()
            {
                ProductId = dto.ProductId,
                ProductName = dto.ProductName,
                ProductTitle = dto.ProductTitle,
                ProductDescription = dto.ProductDescription,
                ProductCategory = dto.ProductCategory,
                MainImageName = dto.MainImageName,
                MainImageTitle = dto.MainImageTitle,
                MainImageUri = dto.MainImageUri,
                Color = dto.Color,
                IsExisting = dto.IsExisting,
                IsFreeDelivery = dto.IsFreeDelivery,
                Weight = dto.Weight
            };

        private IEnumerable<ProductViewModel> CreateProductViewModelsFromProductDtos(IEnumerable<ProductDto> dtos)
        {
            ICollection<ProductViewModel> productViewModels = new List<ProductViewModel>();

            foreach (var ProductDto in dtos)
                productViewModels.Add(
                     new ProductViewModel()
                     {

                         ProductId = ProductDto.ProductId,
                         ProductName = ProductDto.ProductName,
                         ProductTitle = ProductDto.ProductTitle,
                         ProductDescription = ProductDto.ProductDescription,
                         ProductCategory = ProductDto.ProductCategory,
                         MainImageName = ProductDto.MainImageName,
                         MainImageTitle = ProductDto.MainImageTitle,
                         MainImageUri = ProductDto.MainImageUri,
                         Color = ProductDto.Color,
                         IsExisting = ProductDto.IsExisting,
                         IsFreeDelivery = ProductDto.IsFreeDelivery,
                         Weight = ProductDto.Weight
                     });


            return (IEnumerable<ProductViewModel>)productViewModels;
        }

        private void ValidateProductName(string productName)
        {
            if (string.IsNullOrEmpty(productName) || string.IsNullOrWhiteSpace(productName))
                throw new ArgumentNullException(nameof(productName), "Product Name must not be empty");
        }

        private void ValidateProductTitle(string productTitle)
        {
            if (string.IsNullOrEmpty(productTitle) || string.IsNullOrWhiteSpace(productTitle))
                throw new ArgumentNullException(nameof(productTitle), "Product Title must not be empty");
        }

        #endregion Private
    }
}
