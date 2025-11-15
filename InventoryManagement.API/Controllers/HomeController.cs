using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.API.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Welcome()
        {
            return View();
        }
        public IActionResult ProductManagement()
        {
            return View();
        }
        public IActionResult CreateProduct()
        {
            return View();
        } 
        public IActionResult EditProduct()
        {
            return View();
        } 

    }
}
