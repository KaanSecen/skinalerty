using Domain.Entities;

namespace SteamApi;

public class SteamHttpClientService : HttpClientService<Item>
{
    public SteamHttpClientService() :
        base(url: new Uri("https://api.steamapis.com/market/items/730?api_key={ YourSecretAPIKey }\n"))
}