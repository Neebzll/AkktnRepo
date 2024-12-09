using AKKTN_Pr00.Data;
using AKKTN_Pr00.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AKKTN_Pr00.Controllers
{
    public class HomeController : Controller
    {
        private AppDBContext _db;

        public HomeController(AppDBContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            
            return View(_db.admintbls.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
