using Newtonsoft.Json;

namespace Domain.Api;

public class ItemData
{
    [JsonProperty("nameID")]
    public string NameID { get; set; }

    [JsonProperty("market_name")]
    public string MarketName { get; set; }

    [JsonProperty("market_hash_name")]
    public string MarketHashName { get; set; }

    [JsonProperty("image")]
    public string Image { get; set; }

    [JsonProperty("prices")]
    public Prices Prices { get; set; }
}