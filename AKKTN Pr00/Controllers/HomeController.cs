using AKKTN_Pr00.Data;
using AKKTN_Pr00.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Data;

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
            
            if (!HttpContext.Session.Keys.Contains("Signed"))
            {
                HttpContext.Session.SetString("Signed", "not signed-in");//doesnt show anyone logged as default
            }

            return View();
        }
    

        //        public IActionResult Index(string user)
        //{
        //    string query = "";
        //    if (user == "admin") {
        //        query = "Select * from unmaskedclients";//reveals the masked columns
        //    }
        //    else
        //    {
        //        query = "Select * from maskedClientsTbl";
        //    }
            
        //    var companies = _db.clients.FromSqlRaw(query).ToList();
        //    return View(companies); }
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
