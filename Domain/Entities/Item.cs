namespace Domain.Entities;

public class Item
{
    public int Id { get; init; }

    public int CsId { get; init; }

    public required string Name { get; set; }

    public required string Exterior { get; set; }

    public required string ImageUrl { get; set; }

    public required string SteamUrl { get; set; }

    public required string Price { get; set; }

}
