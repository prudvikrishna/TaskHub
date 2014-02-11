using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TaskHub.Web.Filters;
using TaskHub.Web.Models;
using WebMatrix.WebData;
using Newtonsoft.Json;

namespace TaskHub.Web.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        private ITaskHubContext db = new TaskHubEntities();

        public TasksController() { }
        public TasksController(ITaskHubContext context)
        {
            db = context;
        }
        //
        // GET: /Tasks/
        [InitializeSimpleMembership]
        public ActionResult Index(string searchString)
        {
            if(User.IsInRole("Admin"))
            {
                var tasks = FetchTasks(searchString);
                ViewBag.CurrentFilter = searchString;
                return View(tasks.ToList());
            }
            else
            {
                int userid = (int)Membership.GetUser().ProviderUserKey;
                var tasks = FetchTasks(searchString, userid);
                ViewBag.CurrentFilter = searchString;
                return View("Index", tasks.ToList());
            }

        }

        public IQueryable<Tasks> FetchTasks(string searchString, int userId = 0)
        {
            var tasks = db.Tasks.Where(t => t.Status != "Closed").Include(t => t.AssignedUser).Include(t => t.RequestedUser);
            if (userId > 0)
                tasks = tasks.Where(t => t.AssignedTo == userId);
            if (!String.IsNullOrEmpty(searchString))
            {
                tasks = tasks.Where(t => t.Title.Contains(searchString) || t.AssignedUser.UserName.Contains(searchString) || t.Priority.Contains(searchString) || t.Status.Contains(searchString));
            }
            return tasks;
        }

        public IEnumerable<Tasks> GetAllTasks()
        {
            var res = db.Tasks.ToList();
            return res;
        }

       //
        // GET: /Tasks/Details/5

        public ActionResult Details(Guid id)
        {
            Tasks tasks = db.Tasks.Find(id);
            if (tasks == null)
            {
                return HttpNotFound();
            }
            return View(tasks);
        }

        //
        // GET: /Tasks/Create
        public ActionResult Create()
        {
            ViewBag.AssignedTo = new SelectList(db.UserProfile, "UserId", "UserName");
            ViewBag.RequestedBy = new SelectList(db.UserProfile, "UserId", "UserName");
            return View();
        }

        [HttpGet]
        public ActionResult GetTasksbySearchString(string search)
        {
            var tasksByStatus = db.Tasks.Where(t => t.Title.Contains(search)).Include(t => t.AssignedUser).Include(t => t.RequestedUser).ToList();
            List<TaskHub.Web.Messages.Task> tasks = new List<TaskHub.Web.Messages.Task>();
            foreach (var tp in tasksByStatus)
            {
                var t = new TaskHub.Web.Messages.Task();
                t.Title = tp.Title;
                t.Status = tp.Status;
                t.Priority = tp.Priority;
                t.TaskId = tp.TaskId;
                t.DueDate = tp.DueDate;
                tasks.Add(t);

            }
            return Json(tasks, JsonRequestBehavior.AllowGet);
        }

         //
        // POST: /Tasks/Create

        [HttpPost]
        public ActionResult Create(Tasks tasks)
        {
            if (ModelState.IsValid)
            {
                tasks.TaskId = Guid.NewGuid();
                
                db.Tasks.Add(tasks);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AssignedTo = new SelectList(db.UserProfile, "UserId", "UserName", tasks.AssignedTo);
            ViewBag.RequestedBy = new SelectList(db.UserProfile, "UserId", "UserName", tasks.RequestedBy);
            ViewBag.LastModifiedDate = DateTime.Now;
            return View(tasks);
        }

        //
        // GET: /Tasks/Edit/5

        public ActionResult Edit(Guid id)
        {
            Tasks tasks = db.Tasks.Find(id);
            if (tasks == null)
            {
                return HttpNotFound();
            }
            var users = db.UserProfile;
            ViewBag.Users = db.UserProfile;
            ViewBag.AssignedTo = new SelectList(users, "UserId", "UserName", tasks.AssignedTo);
            ViewBag.RequestedBy = new SelectList(users, "UserId", "UserName", tasks.RequestedBy);
           
            return View(tasks);
        }

        //
        // POST: /Tasks/Edit/5

        [HttpPost]
        public ActionResult Edit(Tasks tasks)
        {
            if (tasks.AssignedUser == null)
                tasks.AssignedUser = db.UserProfile.First(u => u.UserId == tasks.AssignedTo);
            if (tasks.RequestedUser == null)
                tasks.RequestedUser = db.UserProfile.First(u => u.UserId == tasks.RequestedBy);
                  
            tasks.LastModifiedDate = DateTime.Now;
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                db.MarkAsModified(tasks);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AssignedTo = new SelectList(db.UserProfile, "UserId", "UserName", tasks.AssignedTo);
            ViewBag.RequestedBy = new SelectList(db.UserProfile, "UserId", "UserName", tasks.RequestedBy);
            return View(tasks);
        }

        //
        // GET: /Tasks/Delete/5

        public ActionResult Delete(Guid id)
        {
            Tasks tasks = db.Tasks.Find(id);
            if (tasks == null)
            {
                return HttpNotFound();
            }
            return View(tasks);
        }

        //
        // POST: /Tasks/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Tasks tasks = db.Tasks.Find(id);
            db.Tasks.Remove(tasks);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}