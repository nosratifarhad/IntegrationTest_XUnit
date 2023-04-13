using Bogus;
using ECommerce.Domain.Products.Dtos.ProductDtos;
using ECommerce.Domain.Products.Entitys;
using ECommerce.Domain.Products.Enums;
using ECommerce.Service.InputModels.ProductInputModels;

namespace ECommerce.UnitTest.MockDatas
{
    public static class ProductMockData
    {
        public const int Zero = 0;

        public const int NegativeOne = -1;

        #region [ CreateProductInputModel ]
        public static CreateProductInputModel ProductNameIsNullInC1reateProductInputModel()
            => new Faker<CreateProductInputModel>()
              .RuleFor(bp => bp.ProductName, f => null)
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

        public static CreateProductInputModel ProductTitleIsNullInCreateProductInputModel()
            => new Faker<CreateProductInputModel>()
              .RuleFor(bp => bp.ProductName, f => f.Name.FirstName())
              .RuleFor(bp => bp.ProductTitle, f => null)
              .RuleFor(bp => bp.ProductDescription, f => f.Name.JobDescriptor())
              .RuleFor(bp => bp.ProductCategory, f => f.Random.Enum<ProductCategory>())
              .RuleFor(bp => bp.MainImageName, f => f.Name.FullName())
              .RuleFor(bp => bp.MainImageTitle, f => f.Name.FullName())
              .RuleFor(bp => bp.MainImageUri, f => f.Name.FullName())
              .RuleFor(bp => bp.Color, f => f.Random.Enum<ProductColor>())
              .RuleFor(bp => bp.IsFreeDelivery, f => f.Random.Bool())
              .RuleFor(bp => bp.IsExisting, f => f.Random.Bool())
              .RuleFor(bp => bp.Weight, f => f.Random.Number());

        public static CreateProductInputModel ValidCreateProductInputModel()
            => new Faker<CreateProductInputModel>()
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


        #endregion [ CreateProductInputModel ]

        #region [ UpdateProductInputModel ]

        public static UpdateProductInputModel ValidUpdateProductInputModel()
            => new Faker<UpdateProductInputModel>()
              .RuleFor(bp => bp.ProductId, f => f.Random.Number(1, 5))
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

        public static UpdateProductInputModel InValidProductIdInUpdateProductInputModel()
            => new Faker<UpdateProductInputModel>()
              .RuleFor(bp => bp.ProductId, f => f.Random.Number(5, 20))
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

        public static UpdateProductInputModel ZeroProductIdInUpdateProductInputModel()
            => new Faker<UpdateProductInputModel>()
              .RuleFor(bp => bp.ProductId, f => Zero)
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

        public static UpdateProductInputModel NegativeOneProductIdInUpdateProductInputModel()
            => new Faker<UpdateProductInputModel>()
              .RuleFor(bp => bp.ProductId, f => NegativeOne)
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

        public static UpdateProductInputModel ProductNameIsNullInUpdateProductInputModel()
            => new Faker<UpdateProductInputModel>()
              .RuleFor(bp => bp.ProductId, f => f.Random.Number(1, 50))
              .RuleFor(bp => bp.ProductName, f => null)
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

        public static UpdateProductInputModel ProductTitleIsNullInUpdateProductInputModel()
            => new Faker<UpdateProductInputModel>()
              .RuleFor(bp => bp.ProductId, f => f.Random.Number(1, 50))
              .RuleFor(bp => bp.ProductName, f => f.Name.FirstName())
              .RuleFor(bp => bp.ProductTitle, f => null)
              .RuleFor(bp => bp.ProductDescription, f => f.Name.JobDescriptor())
              .RuleFor(bp => bp.ProductCategory, f => f.Random.Enum<ProductCategory>())
              .RuleFor(bp => bp.MainImageName, f => f.Name.FullName())
              .RuleFor(bp => bp.MainImageTitle, f => f.Name.FullName())
              .RuleFor(bp => bp.MainImageUri, f => f.Name.FullName())
              .RuleFor(bp => bp.Color, f => f.Random.Enum<ProductColor>())
              .RuleFor(bp => bp.IsFreeDelivery, f => f.Random.Bool())
              .RuleFor(bp => bp.IsExisting, f => f.Random.Bool())
              .RuleFor(bp => bp.Weight, f => f.Random.Number());


        #endregion [ UpdateProductInputModel ]

        #region [ Product ]

        public static Product CreateProduct()
             => new Product(
                 new Faker().Name.FindName(), 
                 new Faker().Name.JobTitle(), 
                 string.Empty, 
                 ProductCategory.None, 
                 string.Empty, 
                 string.Empty, 
                 string.Empty, 
                 ProductColor.None, 
                 false, 
                 false, 
                 0);

        public static Product UpdateProduct()
             => new Product(
                 new Faker().Random.Number(1,5),
                 new Faker().Name.FindName(),
                 new Faker().Name.JobTitle(),
                 string.Empty,
                 ProductCategory.None,
                 string.Empty,
                 string.Empty,
                 string.Empty,
                 ProductColor.None,
                 false,
                 false,
                 0);


        #endregion [ Product ]

    }
}
