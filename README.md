# Xunit_IntegrationTest

### Test From Apis To Repos .
### Test Exceptions .
### Test All Logics .
### Test All Validation .

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



