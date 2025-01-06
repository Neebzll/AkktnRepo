using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AKKTN_Pr00.Data;
using AKKTN_Pr00.Models;

namespace AKKTN_Pr00.Controllers
{
    public class CompanyTeamsController : Controller
    {
        private readonly AppDBContext _context;
        public string name = "";


        public CompanyTeamsController(AppDBContext context)
        {
            _context = context;
        }

        // GET: CompanyTeams
        public async Task<IActionResult> Index(string? id)
        {
           
            ViewData["ID"] = id;
            var name = _context.companies.FirstOrDefault(c => c.CompanyID == id);
            ViewData["Name"] = name.CompanyName;

            
           
            return View(await _context.companiesTeam.Where(ct=>ct.CompanyID==id).ToListAsync());
        }

        // GET: CompanyTeams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyTeam = await _context.companiesTeam
                .FirstOrDefaultAsync(m => m.memberID == id);
            if (companyTeam == null)
            {
                return NotFound();
            }

            return View(companyTeam);
        }

        // GET: CompanyTeams/Create
        public IActionResult Create(string? id)
        {
            ViewData["ID"] = id;
            return View();
        }

        // POST: CompanyTeams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("memberID,CompanyID,ContactName,JobTitle,Email,Cell,Role")] CompanyTeam companyTeam)
        {
            if (ModelState.IsValid)
            {
                _context.Add(companyTeam);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "CompanyTeams", new {id=companyTeam.CompanyID});
            }
            return RedirectToAction("Index", "CompanyTeams", new { id = companyTeam.CompanyID });
        }

        // GET: CompanyTeams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyTeam = await _context.companiesTeam.FindAsync(id);
            if (companyTeam == null)
            {
                return NotFound();
            }
            return View(companyTeam);
        }

        // POST: CompanyTeams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("memberID,CompanyID,ContactName,JobTitle,Email,Cell,Role")] CompanyTeam companyTeam)
        {
            

            if (ModelState.IsValid)
            {
               
                    _context.Update(companyTeam);
                    await _context.SaveChangesAsync();
               
                return RedirectToAction("Index", "CompanyTeams", new { id = companyTeam.CompanyID,name=name });
            }
            return RedirectToAction("Index", "CompanyTeams", new { id = companyTeam.CompanyID,name=name});
        }

        // GET: CompanyTeams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {

            var companyTeam = await _context.companiesTeam
                .FirstOrDefaultAsync(m => m.memberID == id);

            return View(companyTeam);
        }

        // POST: CompanyTeams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var companyTeam = await _context.companiesTeam.FindAsync(id);
            string comid=companyTeam.CompanyID;
            if (companyTeam != null)
            {
                _context.companiesTeam.Remove(companyTeam);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "CompanyTeams", new {id=comid});
        }

        private bool CompanyTeamExists(int id)
        {
            return _context.companiesTeam.Any(e => e.memberID == id);
        }
    }
}
