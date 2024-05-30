namespace Domain.Entities;

public class Notification(int id, int userId, int itemId, decimal desiredPrice, int intervalSeconds, bool status)
{
    public int Id { get; init; } = id;

    public int UserId { get; protected set; } = userId;

    public int ItemId { get; protected set; } = itemId;

    public decimal DesiredPrice { get;  protected set; } = desiredPrice;

    public int IntervalSeconds { get; protected set; } = intervalSeconds;

    public bool Status { get; protected set; } = status;
}