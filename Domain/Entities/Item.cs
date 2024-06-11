namespace Domain.Entities;

public class Item(int id, int csId, string name, string exterior, string imageUrl, string steamUrl, decimal price)
{
    public int Id { get; init; } = id;

    public int CsId { get; init; } = csId;

    public string Name { get; set; } = name;

    public string Exterior { get; set; } = exterior;

    public string ImageUrl { get; set; } = imageUrl;

    public string SteamUrl { get; set; } = steamUrl;
    public decimal Price { get; set; } = price;

}
