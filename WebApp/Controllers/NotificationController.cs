using Microsoft.AspNetCore.Mvc;
using BLL.Models;
using BLL.Layers;
using Newtonsoft.Json;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class NotificationController(NotificationLogic notificationLogic) : Controller
    {
        [HttpGet]
        public IActionResult Create()
        {
            var notificationViewModel = new NotificationViewModel
            {
                UserId = 0,
                ItemId = 0,
                DesiredPrice = 0,
                IntervalSeconds = 0,
                Status = false
            };

            var userViewModelJson = HttpContext.Session.GetString("User");
            if (!string.IsNullOrEmpty(userViewModelJson))
            {
                var userViewModel = JsonConvert.DeserializeObject<UserViewModel>(userViewModelJson);
                if (userViewModel != null)
                {
                    notificationViewModel.UserId = userViewModel.Id;
                }
            } else
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Login", "User");
            }

            return View(notificationViewModel);
        }

        [HttpPost]
        public IActionResult Create(NotificationViewModel notificationViewModel)
        {
            if (ModelState.IsValid)
            {
                var notification = NotificationViewModel.ConvertToNotification(notificationViewModel);

                var result = notificationLogic.SaveNotification(notification);

                if (result.IsSuccess)
                {
                    return RedirectToAction("Dashboard", "User");
                }
                ViewBag.HasError = !result.IsSuccess;
                ViewBag.ErrorMessage = result.Message!;
            }
            return View(notificationViewModel);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var notification = notificationLogic.GetNotification(id);
            if (notification.IsSuccess)
            {
                NotificationViewModel notificationViewModel = NotificationViewModel.ConvertToView(notification.Result!.First());
                return View(notificationViewModel);
            }

            ViewBag.HasError = true;
            ViewBag.ErrorMessage = notification.Message!;
            return View();
        }
    }
}