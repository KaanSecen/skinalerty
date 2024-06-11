using BLL.Interfaces;
using BLL.Models;

namespace BLL.Layers;

public class NotificationLogic(INotificationService notificationService)
{

    public ValidationResult<Notification> GetAllNotificationsFromUser(int userId)
    {
        ValidationResult<Notification> notificationValidationResult = new ValidationResult<Notification>
        {
            Result = null,
        };

        var notification = notificationService.GetAllNotificationFromUser(userId);
        notificationValidationResult.Result = notification;

        if (notificationValidationResult.Result.Any())
        {
            notificationValidationResult.IsSuccess = true;
            notificationValidationResult.Message = "Notifications retrieved successfully!";
            return notificationValidationResult;
        }

        notificationValidationResult.IsSuccess = false;
        notificationValidationResult.Message = "No notifications found!";
        return notificationValidationResult;
    }

    public ValidationResult<Notification> SaveNotification(Notification notification)
    {
        ValidationResult<Notification> notificationValidationResult = new ValidationResult<Notification>
        {
            Result = null,
        };

        if (notification.IsDesiredPriceValid() && notification.IsIntervalSecondsValid())
        {
            var existingNotification = notificationService.GetNotificationByUserAndItemId(notification.UserId, notification.ItemId);
            if (existingNotification != null)
            {
                notificationValidationResult.IsSuccess = false;
                notificationValidationResult.Message = "A notification for this item already exists!";
                return notificationValidationResult;
            }

            notificationValidationResult.Result = [notificationService.SaveNotification(notification)];
            notificationValidationResult.IsSuccess = true;
            notificationValidationResult.Message = "Notification created successfully!";
            return notificationValidationResult;
        }

        notificationValidationResult.IsSuccess = false;
        notificationValidationResult.Message = "Desired price and interval seconds must be greater than 0!";
        return notificationValidationResult;
    }

    public ValidationResult<Notification> UpdateNotification(Notification notification)
    {
        ValidationResult<Notification> notificationValidationResult = new ValidationResult<Notification>
        {
            Result = null,
        };

        if (notification.IsDesiredPriceValid() && notification.IsIntervalSecondsValid())
        {
            var existingNotification = notificationService.GetNotificationByUserAndItemId(notification.UserId, notification.ItemId);
            if (existingNotification == null)
            {
                notificationValidationResult.IsSuccess = false;
                notificationValidationResult.Message = "No notification found!";
                return notificationValidationResult;
            }

            notificationValidationResult.Result = [notificationService.UpdateNotification(notification)];
            notificationValidationResult.IsSuccess = true;
            notificationValidationResult.Message = "Notification updated successfully!";
            return notificationValidationResult;
        }

        notificationValidationResult.IsSuccess = false;
        notificationValidationResult.Message = "Desired price and interval seconds must be greater than 0!";
        return notificationValidationResult;
    }

    public ValidationResult<Notification> GetNotification(int id)
    {
        ValidationResult<Notification> notificationValidationResult = new ValidationResult<Notification>
        {
            Result = null,
        };

        var notification = notificationService.GetNotification(id);
        if (notification != null)
        {
            notificationValidationResult.Result = [notification];
            notificationValidationResult.IsSuccess = true;
            notificationValidationResult.Message = "Notification retrieved successfully!";
        }
        else
        {
            notificationValidationResult.IsSuccess = false;
            notificationValidationResult.Message = "Notification not found!";
        }

        return notificationValidationResult;
    }

}