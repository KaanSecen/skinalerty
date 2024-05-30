namespace SteamApi;

public abstract class HttpClientService<T>(Uri url, string apiKey, string apiHost)
{
    private readonly HttpClient _httpClient = new HttpClient();

    protected async Task<string> GetJsonData()
    {
        Task<string> task;

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = url,
            Headers =
            {
                { "X-RapidAPI-Key", apiKey },
                { "X-RapidAPI-Host", apiHost },
            },
        };

        using (var response = await _httpClient.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            task = response.Content.ReadAsStringAsync();
        }

        return await task;
    }

}