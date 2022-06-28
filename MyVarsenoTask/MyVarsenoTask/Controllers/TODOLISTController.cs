using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyVarsenoTask;

namespace MyVarsenoTask.Controllers
{
    [Authorize]
    public class TODOLISTController : Controller
    {
        private MyProjectEntities db = new MyProjectEntities();

        // GET: TODOLIST
        public ActionResult Index()
        {
            var tODOLISTTABLEs = db.TODOLISTTABLEs.Include(t => t.TODOLISTTYPE);
            return View(tODOLISTTABLEs.ToList());
        }

        // GET: TODOLIST/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TODOLISTTABLE tODOLISTTABLE = db.TODOLISTTABLEs.Find(id);
            if (tODOLISTTABLE == null)
            {
                return HttpNotFound();
            }
            return View(tODOLISTTABLE);
        }

        // GET: TODOLIST/Create
        public ActionResult Create()
        {
            ViewBag.PriorityId = new SelectList(db.TODOLISTTYPEs, "PriorityId", "Priority");
            return View();
        }

        // POST: TODOLIST/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ToDoListId,Date,Title,Task,PriorityId")] TODOLISTTABLE tODOLISTTABLE)
        {
            if (ModelState.IsValid)
            {
                db.TODOLISTTABLEs.Add(tODOLISTTABLE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PriorityId = new SelectList(db.TODOLISTTYPEs, "PriorityId", "Priority", tODOLISTTABLE.PriorityId);
            return View(tODOLISTTABLE);
        }

        // GET: TODOLIST/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TODOLISTTABLE tODOLISTTABLE = db.TODOLISTTABLEs.Find(id);
            if (tODOLISTTABLE == null)
            {
                return HttpNotFound();
            }
            ViewBag.PriorityId = new SelectList(db.TODOLISTTYPEs, "PriorityId", "Priority", tODOLISTTABLE.PriorityId);
            return View(tODOLISTTABLE);
        }

        // POST: TODOLIST/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ToDoListId,Date,Title,Task,PriorityId")] TODOLISTTABLE tODOLISTTABLE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tODOLISTTABLE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PriorityId = new SelectList(db.TODOLISTTYPEs, "PriorityId", "Priority", tODOLISTTABLE.PriorityId);
            return View(tODOLISTTABLE);
        }

        // GET: TODOLIST/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TODOLISTTABLE tODOLISTTABLE = db.TODOLISTTABLEs.Find(id);
            if (tODOLISTTABLE == null)
            {
                return HttpNotFound();
            }
            return View(tODOLISTTABLE);
        }

        // POST: TODOLIST/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TODOLISTTABLE tODOLISTTABLE = db.TODOLISTTABLEs.Find(id);
            db.TODOLISTTABLEs.Remove(tODOLISTTABLE);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
