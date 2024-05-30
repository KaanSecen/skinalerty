namespace BLL.Models;

public class Notification(int id, int userId, int itemId, decimal desiredPrice, int intervalSeconds, bool status) : Domain.Entities.Notification(id, userId, itemId, desiredPrice, intervalSeconds, status)
{
    public bool IsDesiredPriceValid()
    {
        return DesiredPrice > 0;
    }

    public bool IsIntervalSecondsValid()
    {
        return IntervalSeconds > 0;
    }

    public void SetStatus(bool status)
    {
        Status = status;
    }

    public void SetDesiredPrice(decimal desiredPrice)
    {
        DesiredPrice = desiredPrice;
    }
}