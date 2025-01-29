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
using DocumentFormat.OpenXml.Office2021.DocumentTasks;

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
            var tasks = await _context.assignedTasks
     .Where(at => at.ClientID == ClientID)
     .Join(_context.tasks,
           at => at.TaskID,
           task => task.TaskID,
           (at, task) => task)
     .Where(task => task.CompanyID == id)
     .Distinct() // Ensures unique tasks
     .ToListAsync();


            // Fetch members for each task filtered by ClientID and CompanyID
            var taskMembers = new Dictionary<int, List<CompanyTeam>>();
            foreach (var task in tasks)
            {
                if (!taskMembers.ContainsKey(task.TaskID))
                {
                    taskMembers[task.TaskID] = _context.assignedTasks
                        .Where(at => at.TaskID == task.TaskID)
                        .Join(_context.companiesTeam,
                              at => at.memberID,
                              member => member.memberID,
                              (at, member) => member)
                        .Where(member => member.CompanyID == id)
                        .ToList();
                }
            }



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
                Tasks = new Models.Tasks(),
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
      [Bind("CompanyID,TaskDescription,AssignTaskDate,DueDate,Reminders,TaskStatus")] Models.Tasks tasks,List<int> MembersID)
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



        // POST: Tasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        public async Task<IActionResult> Edit(int id)
        {
            var task = await _context.tasks.FirstOrDefaultAsync(t => t.TaskID == id);

            if (task == null)
            {
                return NotFound();
            }

            // Get the company ID from the session
            string companyId = HttpContext.Session.GetString("companyID");

            // Get all team members for the company
            var teamMembers = _context.companiesTeam.Where(ct => ct.CompanyID == companyId).ToList();

            // Get the IDs of members already assigned to this task
            var assignedMemberIDs = _context.assignedTasks
                .Where(tm => tm.TaskID == id)
                .Select(tm => tm.memberID)
                .ToList();

            // Populate the model
            CreateTask model = new CreateTask()
            {
                Tasks = task,
                TeamMembers = teamMembers
            };

            // Pass the assigned Member IDs via ViewBag
            ViewBag.AssignedMembers = assignedMemberIDs;

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
       [Bind("TaskID,CompanyID,TaskDescription,AssignTaskDate,DueDate,Reminders,TaskStatus")] Models.Tasks tasks,
       List<int> MembersID)
        {
            string companyId = HttpContext.Session.GetString("companyID");

            if (MembersID == null || !MembersID.Any())
            {
                ViewBag.ErrorMessage = "Select at least one team member.";
            }

            if (ModelState.IsValid && MembersID.Any())
            {
                try
                {
                    // Update the task details
                    _context.Update(tasks);
                    await _context.SaveChangesAsync();

                    // Fetch existing assignments for the task
                    var existingAssignments = _context.assignedTasks.Where(at => at.TaskID == tasks.TaskID).ToList();

                    // Find members to remove (not in the new MembersID list)
                    var membersToRemove = existingAssignments
                        .Where(at => !MembersID.Contains(at.memberID))
                        .ToList();

                    // Remove unassigned members
                    _context.assignedTasks.RemoveRange(membersToRemove);

                    // Find members to add (in MembersID but not in existingAssignments)
                    var membersToAdd = MembersID
                        .Where(memberID => !existingAssignments.Any(at => at.memberID == memberID))
                        .Select(memberID => new assignedTasks
                        {
                            ClientID = int.Parse(HttpContext.Session.GetString("ClientID").ToString()),
                            TaskID = tasks.TaskID,
                            memberID = memberID
                        });
                    Console.WriteLine($"ClientID: {HttpContext.Session.GetString("ClientID")}");
                    // Add new assignments
                    _context.assignedTasks.AddRange(membersToAdd);

                    // Save changes
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index", new { ClientID = HttpContext.Session.GetString("ClientID") });

                }
                catch (Exception)
                {
                    return View("Error");
                }
            }

            var teamMembers = _context.companiesTeam.Where(ct => ct.CompanyID == companyId).ToList();

            // Get the IDs of members already assigned to this task
            var assignedMemberIDs = _context.assignedTasks
                .Where(tm => tm.TaskID == tasks.TaskID)
                .Select(tm => tm.memberID)
                .ToList();

            // Populate the model
            CreateTask model = new CreateTask()
            {
                Tasks = tasks,
                TeamMembers = teamMembers
            };

            // Pass the assigned Member IDs via ViewBag
            ViewBag.AssignedMembers = assignedMemberIDs;

            return View(model);
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
