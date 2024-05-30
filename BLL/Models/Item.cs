namespace BLL.Models;

public class Item : Domain.Entities.Item
{
    public bool IsPriceValid()
    {
        return decimal.TryParse(Price, out _);
    }
}