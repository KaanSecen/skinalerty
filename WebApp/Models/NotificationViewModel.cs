using BLL.Models;

namespace WebApp.Models;

public class NotificationViewModel
{
    public int Id { get; set; }

    public required int UserId { get; set; }

    public required int ItemId { get; set; }

    public required decimal DesiredPrice { get; set; }

    public required int IntervalSeconds { get; set; }

    public required bool Status { get; set; }

    public static Notification ConvertToNotification(NotificationViewModel notificationViewModel)
    {
        return new Notification
        (
            notificationViewModel.Id,
            notificationViewModel.UserId,
            notificationViewModel.ItemId,
            notificationViewModel.DesiredPrice,
            notificationViewModel.IntervalSeconds,
            notificationViewModel.Status
        );
    }

    public static NotificationViewModel ConvertToView(Notification notification)
    {
        return new NotificationViewModel
        {
            Id = notification.Id,
            UserId = notification.UserId,
            ItemId = notification.ItemId,
            DesiredPrice = notification.DesiredPrice,
            IntervalSeconds = notification.IntervalSeconds,
            Status = notification.Status
        };
    }
}