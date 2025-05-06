using System.Text.Json;

namespace RipperdocShop.Web.Services;

public abstract class BaseApiService(IHttpClientFactory httpClientFactory)
{
    private readonly HttpClient _http = httpClientFactory.CreateClient("CustomerApi");

    protected async Task<T?> GetAsync<T>(string endpoint)
    {
        var res = await _http.GetAsync(endpoint);
        if (!res.IsSuccessStatusCode) return default;
        var json = await res.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }
}
