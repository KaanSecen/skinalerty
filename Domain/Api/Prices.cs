using Newtonsoft.Json;

namespace Domain.Api;

public class Prices
{
    [JsonProperty("latest")]
    public decimal Latest { get; set; }
}