using MyVarsenoTask.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MyVarsenoTask.Controllers
{
    public class AccountController : Controller
    {
        private MyProjectEntities db = new MyProjectEntities();

        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserTable model)
        {
            using (var context = new MyProjectEntities())
            {
                bool isValid = context.UserTable
                    .Any(x => x.Email == model.Email && x.Password == model.Password);
                if (isValid)
                {
                    FormsAuthentication.SetAuthCookie(model.Email, false);
                    /* var us = new Login{ UserId = model.UserId };
                     Session["User"] = us;*/
                    Session["UserId"] = Convert.ToInt32(model.UserId);
                    var userid = db.UserTable.Where(m => m.Email == model.Email).Select(m => m.UserId).SingleOrDefault();
                    Session["UserId"] = userid;
                    // HttpContext.Session.Add("Template", (model.UserId));
                    return RedirectToAction("Index", "TODOLIST");
                }
                ModelState.AddModelError("", "Invalid Email or Password");
                return View();

            }
        }
        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(UserTable model)
        {
            using (var context = new MyProjectEntities())
            {
                bool isExist = context.UserTable
                    .Any(x => x.Email == model.Email);
                if (isExist)
                {
                    ModelState.AddModelError("", "This account is already existed");
                    return View();
                }
                else
                {
                    context.UserTable.Add(model);
                    context.SaveChanges();
                }

            }
            return RedirectToAction("Login");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
        public ActionResult Edit(int id)
        {
            MyProjectEntities db = new MyProjectEntities();
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserTable user = db.UserTable.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(UserTable userTable)
        {
            if (ModelState.IsValid)
            {
                MyProjectEntities db = new MyProjectEntities();
                db.Entry(userTable).State = EntityState.Modified;
                db.SaveChanges();
                //db.Entry(userTable).State = EntityState.Modified;
                //db.SaveChanges();
                //  return RedirectToAction("Index");
                return RedirectToAction("Index", "TODOLIST");
            }
            return View(userTable);
            /*public ActionResult Edit(int id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                UserTable userTable = db.UserTable.Find(id);
                if (userTable == null)
                {
                    return HttpNotFound();
                }
    *//*            ViewBag.PriorityId = new SelectList(db.UserTable, "PriorityId", "Priority", tODOLISTTABLE.PriorityId);
    *//*            return View(userTable);
            }*/

            // POST: TODOLIST/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to, for 
            // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
            /* [HttpPost]
             [ValidateAntiForgeryToken]
             public ActionResult Edit([Bind(Include = "UserId,Email,FirstName,LastName,DOB,Gender,Education,Address,Password,ConfirmPassword")] UserTable userTable)
             {
                 if (ModelState.IsValid)
                 {
                     db.Entry(userTable).State = EntityState.Modified;
                     db.SaveChanges();
                     return RedirectToAction("Signup");
                 }
     *//*            ViewBag.PriorityId = new SelectList(db.TODOLISTTYPEs, "PriorityId", "Priority", tODOLISTTABLE.PriorityId);
     *//*            return View(userTable);
             }*/
            //[HttpGet]
            //public ActionResult Edit(int id)
            //{
            //    MyProjectEntities db = new MyProjectEntities();
            //    UserTable userTable = db.UserTable.Single(x => x.UserId == id);
            //    return View(userTable);
            //}
            //[HttpPost]
            //public ActionResult Edit(UserTable userTable)
            //{
            //    if (ModelState.IsValid)
            //    {
            //        MyProjectEntities db = new MyProjectEntities();
            //        db.Entry(userTable).State = EntityState.Modified;
            //        db.SaveChanges();
            //        return RedirectToAction("Signup");
            //    }
            //    return View(userTable);
            //}
        }
    }
}