using ECommerce.Domain.Products.Enums;

namespace ECommerce.Domain.Products.Entitys
{
    public class Product
    {
        public int ProductId { get; private set; }

        public string ProductName { get; private set; }

        public string ProductTitle { get; private set; }

        public string? ProductDescription { get; private set; }

        public ProductCategory? ProductCategory { get; private set; }

        public string? MainImageName { get; private set; }

        public string? MainImageTitle { get; private set; }

        public string? MainImageUri { get; private set; }

        public List<ProductImage>? ImagesUri { get; private set; }

        public ProductColor? Color { get; private set; }

        public bool IsFreeDelivery { get; private set; }

        public bool IsExisting { get; private set; }

        public float? Price { get; private set; }

        public float? OffPrice { get; private set; }

        public List<Material>? Materials { get; private set; }

        public int? Weight { get; private set; }

        #region Ctor

        public Product(
            string productName,
            string productTitle,
            string? productDescription,
            ProductCategory? productCategory,
            string? mainImageName,
            string? mainImageTitle,
            string? mainImageUri,
            ProductColor? color,
            bool isFreeDelivery,
            bool isExisting,
            int? weight)
        {
            ProductName = productName;
            ProductTitle = productTitle;
            ProductDescription = productDescription;
            ProductCategory = productCategory;
            MainImageName = mainImageName;
            MainImageTitle = mainImageTitle;
            MainImageUri = mainImageUri;
            Color = color;
            IsFreeDelivery = isFreeDelivery;
            IsExisting = isExisting;
            Weight = weight;
        }

        public Product(
            int productId,
            string productName,
            string productTitle,
            string? productDescription,
            ProductCategory? productCategory,
            string? mainImageName,
            string? mainImageTitle,
            string? mainImageUri,
            ProductColor? color,
            bool isFreeDelivery,
            bool isExisting,
            int? weight)
        {
            ProductId = productId;
            ProductName = productName;
            ProductTitle = productTitle;
            ProductDescription = productDescription;
            ProductCategory = productCategory;
            MainImageName = mainImageName;
            MainImageTitle = mainImageTitle;
            MainImageUri = mainImageUri;
            Color = color;
            IsFreeDelivery = isFreeDelivery;
            IsExisting = isExisting;
            Weight = weight;
        }

        #endregion Ctor

    }
}
