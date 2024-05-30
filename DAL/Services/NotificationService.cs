using BLL.Interfaces;
using BLL.Models;
using MySql.Data.MySqlClient;

namespace DAL.Services;

public class NotificationService : INotificationService
{
    public Notification GetNotification(int id)
    {
        var parameters = new[]
        {
            new MySqlParameter("@id", id)
        };
        var data = Logic.ExecuteQuery("SELECT * FROM skinalerty_notification WHERE id = @id", parameters);
        if (data.Count == 0) return null!;

        var notification = new Notification
        (
            (int)data[0]["id"],
            (int)data[0]["user_id"],
            (int)data[0]["item_id"],
            (decimal)data[0]["desired_price"],
            (int)data[0]["interval_seconds"],
            (bool)data[0]["status"]
        );
        return notification;
    }

    public List<Notification> GetAllNotificationFromUser(int userId)
    {
        var parameters = new[]
        {
            new MySqlParameter("@user_id", userId)
        };
        var data = Logic.ExecuteQuery("SELECT * FROM skinalerty_notification WHERE user_id = @user_id", parameters);
        if (data.Count == 0) return new List<Notification>();

        var notifications = new List<Notification>();
        foreach (var row in data)
        {
            var notification = new Notification
            (
                (int)row["id"],
                (int)row["user_id"],
                (int)row["item_id"],
                (decimal)row["desired_price"],
                (int)row["interval_seconds"],
                (bool)row["status"]
            );
            notifications.Add(notification);
        }
        return notifications;
    }

    public Notification SaveNotification(Notification notification)
    {
        var parameters = new[]
        {
            new MySqlParameter("@user_id", notification.UserId),
            new MySqlParameter("@item_id", notification.ItemId),
            new MySqlParameter("@desired_price", notification.DesiredPrice),
            new MySqlParameter("@interval_seconds", notification.IntervalSeconds),
            new MySqlParameter("@status", notification.Status)
        };

        var data = Logic.ExecuteQuery("INSERT INTO skinalerty_notification (user_id, item_id, desired_price, interval_seconds, status) VALUES (@user_id, @item_id, @desired_price, @interval_seconds, @status)", parameters);
        if (data.Count == 0) return null!;

        notification = new Notification
        (
            (int)data[0]["LastInsertedId"],
            notification.UserId,
            notification.ItemId,
            notification.DesiredPrice,
            notification.IntervalSeconds,
            notification.Status
        );
        return notification;
    }

    public Notification? GetNotificationByUserAndItemId(int userId, int itemId)
    {
        var parameters = new[]
        {
            new MySqlParameter("@user_id", userId),
            new MySqlParameter("@item_id", itemId)
        };
        var data = Logic.ExecuteQuery("SELECT * FROM skinalerty_notification WHERE user_id = @user_id AND item_id = @item_id", parameters);
        if (data.Count == 0) return null;

        var notification = new Notification
        (
            (int)data[0]["id"],
            (int)data[0]["user_id"],
            (int)data[0]["item_id"],
            (decimal)data[0]["desired_price"],
            (int)data[0]["interval_seconds"],
            (bool)data[0]["status"]
        );
        return notification;
    }
}