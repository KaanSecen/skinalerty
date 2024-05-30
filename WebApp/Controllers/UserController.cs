using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BLL.Models;
using System.Security.Claims;
using BLL.Layers;
using Newtonsoft.Json;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class UserController(UserLogic userService, NotificationLogic notificationService) : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Dashboard", "User");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserViewModel user)
        {
            User userConverted = UserViewModel.ConvertToUser(user);

            var result = userService.SaveUser(userConverted);
            if (result.IsSuccess)
            {
                await Authenticate(user.Email);

                // Convert the returned user to a UserViewModel
                UserViewModel userViewModel = UserViewModel.ConvertToView(result.Result!.First());

                // Convert the UserViewModel to a JSON string
                var userViewModelJson = JsonConvert.SerializeObject(userViewModel);

                // Store the JSON string in the session
                HttpContext.Session.SetString("User", userViewModelJson);

                return RedirectToAction("Dashboard", "User");
            }
            ViewBag.HasError = !result.IsSuccess;
            ViewBag.ErrorMessage = result.Message!;
            return View(user);
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Dashboard", "User");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var result = userService.Login(email, password);
            if (result.IsSuccess)
            {
                await Authenticate(email);

                UserViewModel userViewModel = UserViewModel.ConvertToView(result.Result!.First());

                var userViewModelJson = JsonConvert.SerializeObject(userViewModel);

                HttpContext.Session.SetString("User", userViewModelJson);

                return RedirectToAction("Dashboard", "User");
            }
            ViewBag.HasError = !result.IsSuccess;
            ViewBag.ErrorMessage = result.Message!;
            return View();
        }

        [Authorize]
        public IActionResult Dashboard()
        {
            var userViewModelJson = HttpContext.Session.GetString("User");

            if (string.IsNullOrEmpty(userViewModelJson))
            {
                return RedirectToAction("Login", "User");
            }

            UserViewModel userViewModel = JsonConvert.DeserializeObject<UserViewModel>(userViewModelJson)!;

            var notifications = notificationService.GetAllNotificationsFromUser(userViewModel.Id);

            var notificationViewModels = notifications.Result.Select(NotificationViewModel.ConvertToView).ToList();

            var dashboardViewModel = new DashboardViewModel
            {
                User = userViewModel,
                Notifications = notificationViewModels
            };

            return View(dashboardViewModel);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Settings()
        {
            var userViewModelJson = HttpContext.Session.GetString("User");

            if (string.IsNullOrEmpty(userViewModelJson))
            {
                return RedirectToAction("Login", "User");
            }

            UserViewModel userViewModel = JsonConvert.DeserializeObject<UserViewModel>(userViewModelJson)!;

            return View(userViewModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Settings(UserViewModel userViewModel)
        {
            User user = UserViewModel.ConvertToUser(userViewModel);
            var result = userService.UpdateUser(user);
            if (result.IsSuccess)
            {
                // Convert the UserViewModel to a JSON string
                var userViewModelJson = JsonConvert.SerializeObject(userViewModel);

                // Store the JSON string in the session
                HttpContext.Session.SetString("User", userViewModelJson);

                return RedirectToAction("Dashboard", "User");
            }
            ViewBag.HasError = !result.IsSuccess;
            ViewBag.ErrorMessage = result.Message!;
            return View(userViewModel);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "User");
        }

        private async Task Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}