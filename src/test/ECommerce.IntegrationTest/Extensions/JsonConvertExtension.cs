using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ECommerce.IntegrationTest.Extensions
{
    public static class JsonConvertExtension
    {
        public static async Task<T> ToResponseModel<T>(this HttpResponseMessage response)
            => JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());

        public static StringContent ToRequestModel<T>(this T requestModel)
            => new StringContent(JsonConvert.SerializeObject(requestModel), Encoding.UTF8, "application/json");

        public static async Task<int> ToRequestItem(this HttpResponseMessage response, string itemName)
        {
            var camelCaseItemName = itemName.ToCamelCase();

            var json = await response.Content.ReadAsStringAsync();

            int businessItem = (int)json.ToJObject().SelectToken(camelCaseItemName);

            return businessItem;
        }

        public static JObject ToJObject(this string jsonSerializer)
             => JObject.Parse(jsonSerializer);

        #region Private

        private static string ToCamelCase(this string str)
        {
            string[] words = str.Split(new[] { "_", " " }, StringSplitOptions.RemoveEmptyEntries);

            string leadWord = Regex.Replace(words[0], @"([A-Z])([A-Z]+|[a-z0-9]+)($|[A-Z]\w*)",
                m =>
                {
                    return m.Groups[1].Value.ToLower() + m.Groups[2].Value.ToLower() + m.Groups[3].Value;
                });

            string[] tailWords = words.Skip(1)
                .Select(word => char.ToUpper(word[0]) + word.Substring(1))
                .ToArray();

            var camelCaseStr = $"{leadWord}{string.Join(string.Empty, tailWords)}";

            return camelCaseStr;
        }

        #endregion Private
    }
}
