using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AKKTN_Pr00.Data;
using AKKTN_Pr00.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace AKKTN_Pr00.Controllers
{
    public class adminController : Controller
    {
        private readonly AppDBContext _context;

        public adminController(AppDBContext context)
        {
            _context = context;
        }
        public ActionResult AdminDash()
        {
            ViewData["Name"] = "";
            ViewData["ID"] = "";
            ViewData["client"] = "";
            HttpContext.Session.SetString("Signed", "admin@gmail.com");
            return View(_context.companies.ToList());
        }
        // GET: admin
        public ActionResult Index()
        {
            HttpContext.Session.SetString("Signed", "not signed-in");
            return View();
        }
        [HttpPost]
        public ActionResult Index(string adminemail, string adminpass)
        {
            if (ModelState.IsValid)
            {
                // Perform query to find admin credentials
                var findemail = _context.admintbls
                    .FirstOrDefault(ad => ad.email.Equals(adminemail) && ad.adminpass.Equals(adminpass));

                if (findemail != null) // Check if a record was found
                {
                    // Save to session if valid
                    HttpContext.Session.SetString("Signed", findemail.email);
                    HttpContext.Session.SetString("isAdmin", "true");

                    // Redirect to Home controller if login is successful
                    return RedirectToAction("AdminDash");
                }
                else
                {
                    // Add model error if login fails
                    ModelState.AddModelError("Error", "Incorrect email or password");

                    // Return to the Index view in the Admin controller
                    return View();
                }
            }

            // Return to the same view if ModelState is invalid
            HttpContext.Session.SetString("Signed", "not signed-in");
            return View();
        }



        // GET: admin/Details/5

        public async Task<IActionResult> Details(string? name)
        {
            if (name == null)
            {
                return NotFound();
            }

            var comtbl = await _context.companies
                .FirstOrDefaultAsync(m => m.CompanyName == name);
            if (comtbl == null)
            {
                return NotFound();
            }
            
            return RedirectToAction("Index", "Clients", new { id = comtbl.CompanyID, name=comtbl.CompanyName });
            //return RedirectToAction("Index", "Companies", new { name = comtbl.CompanyName });
        }

        // GET: admin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: admin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("adminID,email,cell,adminpass")] admintbl admintbl)
        {
            if (ModelState.IsValid)
            {
                _context.Add(admintbl);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(admintbl);
        }

        // GET: admin/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var com = await _context.companies.FindAsync(id);
            if (com == null)
            {
                return NotFound();
            }
            return RedirectToAction("Edit", "Companies", new { id = id });
        }

        // POST: admin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("adminID,email,cell,adminpass")] admintbl admintbl)
        //{
        //    if (id != admintbl.adminID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(admintbl);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!admintblExists(admintbl.adminID))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(admintbl);
        //}

        // GET: admin/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var com = await _context.companies
                .FirstOrDefaultAsync(m => m.CompanyID == id);
            if (com == null)
            {
                return NotFound();
            }

            return RedirectToAction("Delete", "Companies", new { id = id });
        }

        // POST: admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var admintbl = await _context.admintbls.FindAsync(id);
            if (admintbl != null)
            {
                _context.admintbls.Remove(admintbl);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool admintblExists(int id)
        {
            return _context.admintbls.Any(e => e.adminID == id);
        }
    }
}
