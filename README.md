# IntegrationTest In Xunit

 * Test From ApiControllers Services To Repositories .
 * Test Exceptions .
 * Test All Logics in Services .
 * Test All Validation .
----
### * Install pakeges :
* dotnet add package FluentAssertions
* dotnet add package Bogus
----
### Test For Created Successed Product

```csharp
[Fact]
public async Task When_ValidCreateProductInputModelInCreateProduct_Then_CreatedProductInDataBase()
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
```

### Test For Update Successed Product
```csharp
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
```

### Test For Get All Products

```csharp
[Fact]
public async Task When_CallGetProducts_Then_ReturnListPoductViewModel()
{
    var response = await _client.GetAsync("/api/products").ConfigureAwait(false);

    var responseModel = await response.ToResponseModel<IEnumerable<ProductViewModel>>();

    response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    response.IsSuccessStatusCode.Should().BeTrue();

    responseModel.Should().NotBeNull();
    responseModel.Should().HaveCount(5);
}
```

### Test For Get By ProductId

```csharp
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
```
### User Bogus For Generate Fake Data In MockDatas Like This

```csharp
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
```
### Passed All Tests

![My Remote Image](https://github.com/nosratifarhad/Xunit_IntegrationTest/blob/main/docs/res.png)

