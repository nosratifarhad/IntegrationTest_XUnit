using Newtonsoft.Json;

namespace ECommerce.IntegrationTest.Extensions
{
    public static class JsonConvertExtension
    {
        public static async Task<T> ResponseToViewModel<T>(this HttpResponseMessage response)
            => JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
    }
}
