using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BLL.Models;
using System.Security.Claims;
using BLL.Controllers;
using Newtonsoft.Json;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class UserController(UserLogic userService) : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
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
                return RedirectToAction("ProtectedPage", "User");
            }
            ViewBag.HasError = !result.IsSuccess;
            ViewBag.ErrorMessage = result.Message!;
            return View(user);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var result = userService.Login(email, password);
            if (result.IsSuccess)
            {
                await Authenticate(email);

                UserViewModel userViewModel = UserViewModel.ConvertToView(result.Result!);

                // Convert the UserViewModel to a JSON string
                var userViewModelJson = JsonConvert.SerializeObject(userViewModel);

                // Store the JSON string in the session
                HttpContext.Session.SetString("User", userViewModelJson);

                return RedirectToAction("ProtectedPage", "User");
            }
            ViewBag.HasError = !result.IsSuccess;
            ViewBag.ErrorMessage = result.Message!;
            return View();
        }

        [Authorize]
        public IActionResult ProtectedPage()
        {
            // Retrieve the JSON string from the session
            var userViewModelJson = HttpContext.Session.GetString("User");

            // If the JSON string is null or empty, redirect to the Login view
            if (string.IsNullOrEmpty(userViewModelJson))
            {
                return RedirectToAction("Login", "User");
            }

            // Convert the JSON string back to a UserViewModel
            UserViewModel userViewModel = JsonConvert.DeserializeObject<UserViewModel>(userViewModelJson)!;

            return View(userViewModel);
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