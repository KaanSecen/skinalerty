using Microsoft.AspNetCore.Mvc;
using BLL.Models;
using BLL.Layers;
using Newtonsoft.Json;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ItemController(ItemLogic itemLogic) : Controller
    {
        [HttpGet]
        public IActionResult Search(string searchTerm)
        {
            var result = itemLogic.SearchItems(searchTerm);

            if (result is { IsSuccess: true, Result: not null })
            {
                return Json(result.Result);
            }

            return Json(new { error = result.Message });
        }
    }
}