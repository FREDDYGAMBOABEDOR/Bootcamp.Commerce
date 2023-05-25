
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Order.Service.Proxies.Catalog.Commands;
using System.Text;
using System.Text.Json;

namespace Order.Service.Proxies.Catalog
{
    public interface ICatalogProxy
    {
        Task UpdateStockAsync(ProductInStockUpdateStockCommand command);
    }

    public class CatalogProxy : ICatalogProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public CatalogProxy(IOptions<ApiUrls> apiUrls, HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);
            _apiUrls = apiUrls.Value;
            _httpClient = httpClient;
        }

        public async Task UpdateStockAsync(ProductInStockUpdateStockCommand command)
        {
            var context = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json");

            var request = await _httpClient.PutAsync($"{_apiUrls.CatalogUrl}v1/stocks", context);

            request.EnsureSuccessStatusCode();
        }
    }
}
