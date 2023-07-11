namespace SecurityConnect.WebApp.Services.Base;
public abstract class HttpBaseService
{
    protected HttpClient _httpClient;

    public HttpBaseService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    protected async Task<T> GetAsync<T>(string endpoint)
    {
        try
        {
            var response = await _httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }
        catch
        {
            // In case of exception, return a default value of T.
            return default;
        }
    }

    protected async Task<List<T>> GetListAsync<T>(string endpoint)
    {
        try
        {
            var response = await _httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadFromJsonAsync<List<T>>();
            return data ?? new List<T>();
        }
        catch
        {
            // In case of exception, return an empty list.
            return new List<T>();
        }
    }


    protected async Task<TResponse?> PostAsync<TResponse, TContent>(string endpoint, TContent content)
    {
        var response = await _httpClient.PostAsJsonAsync(endpoint, content);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<TResponse>();
    }

    protected async Task PutAsync<T>(string endpoint, T content)
    {
        var response = await _httpClient.PutAsJsonAsync(endpoint, content);
        response.EnsureSuccessStatusCode();
    }

    protected async Task DeleteAsync(string endpoint)
    {
        var response = await _httpClient.DeleteAsync(endpoint);
        response.EnsureSuccessStatusCode();
    }
}
