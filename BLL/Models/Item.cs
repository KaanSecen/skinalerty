namespace BLL.Models;

public class Item(int id, int csId, string name, string exterior, string imageUrl, string steamUrl, decimal price) : Domain.Entities.Item(id, csId, name, exterior, imageUrl, steamUrl, price)
{
}