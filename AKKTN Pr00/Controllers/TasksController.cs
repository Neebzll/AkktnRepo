using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AKKTN_Pr00.Data;
using AKKTN_Pr00.Models;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.IdentityModel.Tokens;

namespace AKKTN_Pr00.Controllers
{
    public class TasksController : Controller
    {
        private readonly AppDBContext _context;

        public TasksController(AppDBContext context)
        {
            _context = context;
        }

        // GET: Tasks
        /*public async Task<IActionResult> Index(int? ClientID,string? id,string? name)
        {
            var tasksForClient = from at in _context.assignedTasks
                                 join t in _context.tasks on at.TaskID equals t.TaskID
                                 where at.ClientID == ClientID
                                select  t;
           var tasks= tasksForClient.Distinct();
            ViewData["ID"] = id;
            ViewData["Name"]=_context.companies.FirstOrDefault(c=>c.CompanyID==id).CompanyName;
            if (name != null)
            {
                ViewData["client"]=name;
            }
            else
            {
                ViewData["client"] = _context.clients.FirstOrDefault(c => c.ClientID == ClientID).ClientName;

            }
            return View(await tasks.ToListAsync());
        }  */      
        public async Task<IActionResult> Index(int? ClientID, string? id)
        {
            HttpContext.Session.SetString("ClientID", ClientID.ToString());
            //HttpContext.Session.SetString("CompanyID", id.ToString());
            id =HttpContext.Session.GetString("companyID");
            // Fetch tasks based on the provided CompanyID
            var tasks = await _context.tasks
                .Where(t => t.CompanyID == id)
                .ToListAsync();

            // Fetch members for each task filtered by CompanyID
            var taskMembers = tasks.ToDictionary(
                task => task.TaskID,
                task => _context.assignedTasks
                    .Where(at => at.TaskID == task.TaskID)
                    .Join(_context.companiesTeam,
                          at => at.memberID,
                          member => member.memberID,
                          (at, member) => member)
                    .Where(member => member.CompanyID == id) // Ensure the member is linked to the CompanyID
                    .ToList()
            );

            // Populate ViewModel
            var viewModel = new ViewTasks
            {
                Tasks = tasks,
                TaskMembers = taskMembers,
               };
            //ViewData["ID"] = id;
           /* ViewData["Name"]*/ var cname = _context.companies.FirstOrDefault(c => c.CompanyID == id).CompanyName;
            HttpContext.Session.SetString("CompanyName", cname.ToString());
           
            
               HttpContext.Session.SetString("ClientName", _context.clients.FirstOrDefault(c => c.ClientID == int.Parse(HttpContext.Session.GetString("ClientID"))).ClientName) ;

            

            return View(viewModel);
        }
        /*public async Task<IActionResult> Index(int? ClientID,string? id,string? name,ViewTasks? view)
        {
            var tasks = _context.tasks.ToList();

            // Create a dictionary to map TaskID to members
            var taskMembers = tasks.ToDictionary(
                task => task.TaskID,
                task => _context.assignedTasks
                                .Where(at => at.TaskID == task.TaskID)
                                .Join(_context.companiesTeam,
                                      at => at.memberID,
                                      member => member.memberID,
                                      (at, member) => member) // Join assignedTasks with CompanyTeam
                                .ToList()
            );

            // Populate the ViewModel
            var viewModel = new ViewTasks
            {
                Tasks = tasks,
                TaskMembers = taskMembers
            };

            // Pass the ViewModel to the view

            ViewData["ID"] = id;
            ViewData["Name"]=_context.companies.FirstOrDefault(c=>c.CompanyID==id).CompanyName;
            if (name != null)
            {
                ViewData["client"]=name;
            }
            else
            {
                ViewData["client"] = _context.clients.FirstOrDefault(c => c.ClientID == ClientID).ClientName;

            }
            return View(viewModel);
        }*/

        // GET: Tasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tasks = await _context.tasks
                .FirstOrDefaultAsync(m => m.TaskID == id);
            if (tasks == null)
            {
                return NotFound();
            }

            return View(tasks);
        }

        // GET: Tasks/Create
        public IActionResult Create()
        {
            string id = HttpContext.Session.GetString("companyID");
            CreateTask model = new CreateTask()
            {
                Tasks = new Tasks(),
                TeamMembers = _context.companiesTeam.Where(ct => ct.CompanyID == id).ToList()
            };
            
            return View(model);
        }


        // POST: Tasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
      [Bind("CompanyID,TaskDescription,AssignTaskDate,DueDate,Reminders,TaskStatus")] Tasks tasks,List<int> MembersID)
        {
            if (MembersID.IsNullOrEmpty()) {

                ViewBag.ErrorMessage = "Select at least one team member";

            }
            else
            {
                if (ModelState.IsValid)
                {
                   
                    try
                    {
                        _context.tasks.Add(tasks);
                        _context.SaveChanges();
                        int taskid = tasks.TaskID;
                        foreach(int m in MembersID)
                        {
                            assignedTasks assigned = new assignedTasks()
                            {
                                ClientID = int.Parse(HttpContext.Session.GetString("ClientID")),
                                TaskID = taskid,
                                memberID = m
                            };
                            _context.assignedTasks.Add(assigned);
                        }
                        _context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        return View("Error");
                    }

                  
                    return RedirectToAction("Index", new { ClientID = HttpContext.Session.GetString("ClientID") });
                }
            }

           

            // Reload members in case of error

            string id = HttpContext.Session.GetString("companyID");
            CreateTask model = new CreateTask()
            {
                Tasks = tasks,
                TeamMembers = _context.companiesTeam.Where(ct => ct.CompanyID == id).ToList()
            };
            return View(model);
        }
    
        
      //  [HttpPost]
      //  [ValidateAntiForgeryToken]
      //  public async Task<IActionResult> Create(
      //[Bind("CompanyID,TaskDescription,AssignTaskDate,DueDate,Reminders,TaskStatus")] Tasks tasks)
      //  {
      //      if (ModelState.IsValid)
      //      {
      //          _context.tasks.Add(tasks);
      //          await _context.SaveChangesAsync(); 
      //          return RedirectToAction("Index",new {ClientID=HttpContext.Session.GetString("ClientID")});
      //      }

      //      // Reload members in case of error
         

      //      return View(tasks);
      //  }



        // POST: Tasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TaskID,CompanyID,TaskDescription,AssignTaskDate,DueDate,Reminders,TaskStatus")] Tasks tasks)
        {
            if (id != tasks.TaskID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tasks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TasksExists(tasks.TaskID))
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
            return View(tasks);
        }

        // GET: Tasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tasks = await _context.tasks
                .FirstOrDefaultAsync(m => m.TaskID == id);
            if (tasks == null)
            {
                return NotFound();
            }

            return View(tasks);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tasks = await _context.tasks.FindAsync(id);
            var company=tasks.CompanyID;
            
            var clientid = int.Parse(HttpContext.Session.GetString("ClientID"));
            var clientname = "";
            if (tasks != null)
            {
                var assigned = _context.assignedTasks.Where(assignedTasks => assignedTasks.TaskID == id);
             
                
               var client = _context.clients.FirstOrDefault(c=>c.ClientID==clientid).ClientName;
                clientname = client;
                if (assigned.Any())
                {
                    clientid = assigned.FirstOrDefault(c => c.TaskID == id).ClientID;

                    _context.assignedTasks.RemoveRange(assigned);
                   
                }
                _context.tasks.Remove(tasks);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { ClientID = clientid, id = company,name=clientname });
        }

        private bool TasksExists(int id)
        {
            return _context.tasks.Any(e => e.TaskID == id);
        }
    }
}
