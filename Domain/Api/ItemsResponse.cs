using Newtonsoft.Json;

namespace Domain.Api;

public class ItemsResponse
{
    [JsonProperty("appID")]
    public int AppId { get; set; }

    [JsonProperty("data")]
    public List<ItemData> Data { get; set; }
}