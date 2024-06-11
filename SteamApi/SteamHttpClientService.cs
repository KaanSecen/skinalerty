using BLL.Layers;
using BLL.Models;
using Domain.Api;
using Newtonsoft.Json;

namespace SteamApi
{
    public class SteamHttpClientService(ItemLogic itemLogic)
    {
        private readonly HttpClient _httpClient = new();

        public async Task<List<ValidationResult<Item>>> FetchAndSaveItems()
        {
            var savedItems = new List<ValidationResult<Item>>();
            var response = await _httpClient.GetAsync("https://api.steamapis.com/market/items/730?api_key=XnbKtWKTr2CvE0P1XRb7WmZEND8");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var itemsResponse = JsonConvert.DeserializeObject<ItemsResponse>(content);

                foreach (var itemData in itemsResponse!.Data)
                {
                    var item = new Item
                    (
                        0,
                        Convert.ToInt32(itemData.NameID),
                        itemData.MarketName,
                        itemData.MarketHashName.Split('(').Last().TrimEnd(')'),
                        itemData.Image,
                        "https://steamcommunity.com/market/listings/730/" + itemData.MarketHashName,
                        itemData.Prices.Latest
                    );

                    var itemSaved = itemLogic.SaveItem(item);
                    savedItems.Add(itemSaved);
                }
            }
            return savedItems;
        }
    }
}