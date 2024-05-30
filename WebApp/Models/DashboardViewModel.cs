using BLL.Models;

namespace WebApp.Models;

public class DashboardViewModel
{
    public UserViewModel User { get; set; }
    public List<NotificationViewModel> Notifications { get; set; }
}