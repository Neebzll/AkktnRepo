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
    public class ClientsController : Controller
    {
        private readonly AppDBContext _context;
        
        public  string AdminPassword = "1234";
        public  string ID = "";
        public string name = "";
        //private readonly string companyID_signedin = "Devp#5879";

        public ClientsController(AppDBContext context)
        {
            _context = context;

        }
      
        // GET: Clients
        public IActionResult Index(string? id,string? name)
        {
            //var email = HttpContext.Session.GetString("Signed").ToString();
            //var passw = from c in _context.admintbls where c.email == email select c.adminpass.ToString();
            //AdminPassword = passw.ToString();
            

            ViewData["isAdmin"] = false;
            ViewData["ID"] = id;
            this.ID = id;
            var clients=_context.clients.Where(c=>c.CompanyID.Equals(id));
            if (name == null)
            {
                var com = _context.companies.FirstOrDefault(c => c.CompanyID.Equals(id));
                ViewData["Name"] = com.CompanyName;
                this.name= com.CompanyName;
            }
            else { ViewData["Name"] = name;
                this.name = name;
            }
            
            if (clients == null) 
            {
                return View(clients);
            }
            return View(clients);
        }

        [HttpPost]
        public IActionResult Index(string ClientID, string CompanyID, string action)
        {
            var clientid = ClientID;

            if (action.Equals("Details", StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToAction(nameof(Details), new { id = clientid });
            }
            if (action.Equals("Edit", StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToAction(nameof(Edit), new { id = clientid });
            }
            if (action.Equals("Delete", StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToAction(nameof(Delete), new { id = clientid });
            }

            return RedirectToAction("Index", new { id = CompanyID });
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
        public IActionResult Create(string? id)
        {
            ViewData["ID"] = id;
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       
        public async Task<IActionResult> Create([Bind("ClientID,CompanyID,ClientName,RegistrationNumber,CIPCRegistrationDate,IncomeTaxNumber,VAT,VATPeriod,PayeeNumber,PayeeReferenceNumber,EMP501,UIF,UIFNumber,WCC,WCCNumber,Payroll,MonthlyCashbook,FinancialStatements,IncomeTaxReturn")] Clients clients)
        {

            _context.clients.Add(clients);
                await _context.SaveChangesAsync();
             
            return RedirectToAction("Index", "Clients", new { id = clients.CompanyID });

        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clients = await _context.clients.FindAsync(id);
            ViewData["id"] = clients.CompanyID;
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


            if (ModelState.IsValid)
            {
                
                    _context.clients.Update(clients);
                    await _context.SaveChangesAsync();
                
                
               
            }
            return RedirectToAction("Index", "Clients", new { id = clients.CompanyID });
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
            var  comid=clients.CompanyID;
            if (clients != null)
            {
                _context.clients.Remove(clients);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new {id=comid});
        }

        private bool ClientsExists(int id)
        {
            return _context.clients.Any(e => e.ClientID == id);
        }
    }
}
