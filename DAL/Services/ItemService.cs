using BLL.Interfaces;
using BLL.Models;
using MySql.Data.MySqlClient;

namespace DAL.Services;

public class ItemService : IItemService
{
    public Item GetItem(int id)
    {
        var parameters = new[]
        {
            new MySqlParameter("@id", id)
        };
        var data = Logic.ExecuteQuery("SELECT * FROM skinalerty_item WHERE id = @id", parameters);
        if (data.Count == 0) return null!;

        var item = new Item
        (
            (int)data[0]["id"],
            (int)data[0]["cs_id"],
            (string)data[0]["name"],
            (string)data[0]["exterior"],
            (string)data[0]["image_url"],
            (string)data[0]["steam_url"],
            (decimal)data[0]["price"]
        );
        return item;
    }

    public Item SaveItem(Item item)
    {
        var parameters = new[]
        {
            new MySqlParameter("@cs_id", item.CsId),
            new MySqlParameter("@name", item.Name),
            new MySqlParameter("@exterior", item.Exterior),
            new MySqlParameter("@image_url", item.ImageUrl),
            new MySqlParameter("@steam_url", item.SteamUrl),
            new MySqlParameter("@price", item.Price)
        };

        var data = Logic.ExecuteQuery("INSERT INTO skinalerty_item (cs_id, name, exterior, image_url, steam_url, price) VALUES (@cs_id, @name, @exterior, @image_url, @steam_url, @price); SELECT LAST_INSERT_ID();", parameters);
        if (data.Count == 0) return null!;

        item = new Item
        (
            (int)data[0]["LastInsertedId"],
            item.CsId,
            item.Name,
            item.Exterior,
            item.ImageUrl,
            item.SteamUrl,
            item.Price
        );
        return item;
    }

    public List<Item> SearchItems(string searchTerm)
    {
        var parameters = new[]
        {
            new MySqlParameter("@searchTerm", "%" + searchTerm + "%")
        };
        var data = Logic.ExecuteQuery("SELECT * FROM skinalerty_item WHERE name LIKE @searchTerm", parameters);
        var items = new List<Item>();
        foreach (var row in data)
        {
            var item = new Item
            (
                (int)row["id"],
                (int)row["cs_id"],
                (string)row["name"],
                (string)row["exterior"],
                (string)row["image_url"],
                (string)row["steam_url"],
                (decimal)row["price"]
            );
            items.Add(item);
        }
        return items;
    }
}