using Login_and_Registration.Models;
using Login_and_Registration.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Login_and_Registration.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
<<<<<<< HEAD

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
