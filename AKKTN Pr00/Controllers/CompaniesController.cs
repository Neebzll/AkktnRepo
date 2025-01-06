﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AKKTN_Pr00.Data;
using AKKTN_Pr00.Models;
using System.Collections;
using System.ComponentModel.Design;

namespace AKKTN_Pr00.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly AppDBContext _context;

        public CompaniesController(AppDBContext context)
        {
            _context = context;
        }

        // GET: Companies
        public IActionResult Index(string? name)
        {
            if (name == null) 
            {
                return View(_context.companies.ToList());
            }
            else
            {
              var  company = _context.companies.FirstOrDefault(com => com.CompanyName.Equals(name));
                Console.WriteLine("Company " + company.CompanyName);

                return View(new List<Company> { company});
            }

            
        }
        //public IActionResult Index(Company company)
        //{
        //    return View(company);
        //}

        // GET: Companies/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.companies
                .FirstOrDefaultAsync(m => m.CompanyID == id);
            if (company == null)
            {
                return NotFound();
            }

           return View(company);
        }

        // GET: Companies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompanyID,CompanyName,companypass,RegistrationNumber,Status,ContactName1,Email1,Cell1,ContactName2,Email2,Cell2")] Company company)
        {
            if (ModelState.IsValid)
            {
                _context.Add(company);
                await _context.SaveChangesAsync();

                return RedirectToAction("AdminDash", "Admin");
            }
            return RedirectToAction("AdminDash", "Admin");
        }

        // GET: Companies/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.companies.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CompanyID,CompanyName,companypass,RegistrationNumber,Status,ContactName1,Email1,Cell1,ContactName2,Email2,Cell2")] Company company)
        {
            if (id != company.CompanyID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                    _context.Update(company);
                    await _context.SaveChangesAsync();
                
               
                
                return RedirectToAction("AdminDash","Admin");
            }
            return RedirectToAction("AdminDash", "Admin");
        }

        // GET: Companies/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.companies
                .FirstOrDefaultAsync(m => m.CompanyID == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var tasks = await _context.tasks
     .Where(t => t.CompanyID == id)
     .ToListAsync();

            // Get all TaskIDs from the tasks
            var taskIds = tasks.Select(t => t.TaskID).ToList();

            // Find and remove all assignedTasks linked to these TaskIDs
            var assignedTasks = await _context.assignedTasks
                .Where(at => taskIds.Contains(at.TaskID))
                .ToListAsync();

            if (assignedTasks.Any())
            {
                _context.assignedTasks.RemoveRange(assignedTasks);
            }

            // Remove the Tasks
            if (tasks.Any())
            {
                _context.tasks.RemoveRange(tasks);
            }
            var companiesTeam=_context.companiesTeam.Where(ct=>ct.CompanyID == id);
            if (companiesTeam.Any()) { 
            
            _context.companiesTeam.RemoveRange(companiesTeam);
            }
            var clients=_context.clients.Where(c=>c.CompanyID==id);
            if (clients.Any()) {
                _context.clients.RemoveRange(clients);
            }
            // Finally, remove the Company
            var company = await _context.companies
                .FirstOrDefaultAsync(c => c.CompanyID == id);

            if (company != null)
            {
                _context.companies.Remove(company);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("AdminDash","admin");
        }

        private bool CompanyExists(string id)
        {
            return _context.companies.Any(e => e.CompanyID == id);
        }
    }
}
