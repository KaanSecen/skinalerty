using BLL.Models;

namespace BLL.Interfaces;

public interface IItemService
{
    Item SaveItem(Item item);
    Item GetItem(int id);
    List<Item> SearchItems(string searchTerm);

}