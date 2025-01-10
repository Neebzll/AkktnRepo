using AKKTN_Pr00.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AKKTN_Pr00.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Tasks()
        {

            return View();
        }
        public IActionResult Clients()
        {

            return View();
        }
        public IActionResult Teams()
        {

            return View();
        }

        // Submit the "Add Task" form
        [HttpPost]
        public IActionResult GetTask()
        {
            return PartialView("AddTask", new Tasks());
        }

    }
}
