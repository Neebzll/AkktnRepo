﻿using System;
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
    public class ClientsController : Controller
    {
        private readonly AppDBContext _context;
        private readonly string AdminPassword = "1234";
        private readonly string companyID_signedin = "Devp#5879";

        public ClientsController(AppDBContext context)
        {
            _context = context;
        }

        // GET: Clients
        public async Task<IActionResult> Index()
        {
            ViewData["isAdmin"] = false; // Default to non-admin
            var name = from c in _context.companies where c.CompanyID.Equals("Devp#5879") select c.CompanyName;

            HttpContext.Session.SetString("Signed", name.First());
            var clients = from c in _context.clients where companyID_signedin.Equals(c.CompanyID) select c;
            return View(await clients.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Index(string pass, string ClientID, string CompanyID, string action)
        {
            // Validate admin password
            if (pass == AdminPassword)
            {
                
           
            var clientid = ClientID;

            // Set ViewData to enable sensitive info display
            //ViewData["isAdmin"] = true;

            // Optionally: Fetch specific client data based on ClientID or CompanyID
            //var client = await _context.clients
            //    .FirstOrDefaultAsync(c => c.ClientID.Equals( clientid) && c.CompanyID == CompanyID);

            //if (client == null)
            //{
            //    TempData["ErrorMessage"] = "Client not found.";
            //    return RedirectToAction(nameof(Index));
            //}

            //TempData["SuccessMessage"] = "Admin privileges granted. Sensitive data is now visible.";
            if (action.Equals("Details"))
            {
                return RedirectToAction(nameof(Details), new { id = clientid });
            }           
            if (action.Equals("Edit"))
            {
                return RedirectToAction(nameof(Edit), new { id = clientid });
            }           
            if (action.Equals("Delete"))
            {
                return RedirectToAction(nameof(Delete), new { id = clientid });
            }
            }

            return View(await _context.clients.ToListAsync());
            
            
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clients = await _context.clients
                .FirstOrDefaultAsync(m => m.ClientID == id);
            if (clients == null)
            {
                return NotFound();
            }

            return View(clients);
        }

        // GET: Clients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClientID,CompanyID,ClientName,RegistrationNumber,CIPCRegistrationDate,IncomeTaxNumber,VAT,VATPeriod,PayeeNumber,PayeeReferenceNumber,EMP501,UIF,UIFNumber,WCC,WCCNumber,Payroll,MonthlyCashbook,FinancialStatements,IncomeTaxReturn")] Clients clients)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clients);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clients);
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clients = await _context.clients.FindAsync(id);
            if (clients == null)
            {
                return NotFound();
            }
            return View(clients);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClientID,CompanyID,ClientName,RegistrationNumber,CIPCRegistrationDate,IncomeTaxNumber,VAT,VATPeriod,PayeeNumber,PayeeReferenceNumber,EMP501,UIF,UIFNumber,WCC,WCCNumber,Payroll,MonthlyCashbook,FinancialStatements,IncomeTaxReturn")] Clients clients)
        {
            ViewBag.isAdmin = false;
            if (id != clients.ClientID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clients);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientsExists(clients.ClientID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(clients);
        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clients = await _context.clients
                .FirstOrDefaultAsync(m => m.ClientID == id);
            if (clients == null)
            {
                return NotFound();
            }

            return View(clients);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clients = await _context.clients.FindAsync(id);
            if (clients != null)
            {
                _context.clients.Remove(clients);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientsExists(int id)
        {
            return _context.clients.Any(e => e.ClientID == id);
        }
    }
}