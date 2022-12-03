using Microsoft.AspNetCore.Mvc;
using System;

namespace MVCPubs.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Message = "Bienvenidos al Sitio MVCPubs";
            ViewBag.Fecha = DateTime.Now.ToString();
            return View();
        }
    }
}
