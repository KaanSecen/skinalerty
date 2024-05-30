using BLL.Models;

namespace BLL.Interfaces;

public interface INotificationService
{
    Notification GetNotification(int id);
    Notification SaveNotification(Notification notification);
    List<Notification> GetAllNotificationFromUser(int userId);
    Notification? GetNotificationByUserAndItemId(int userId, int itemId);
}