using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Northwind_John.Models;

namespace Northwind_John.Controllers
{
   public class usersController : Controller
   {
      private ModelContainer db = new ModelContainer();

      // GET: users
      public ActionResult Index()
      {
         var userSet = db.userSet.Include(u => u.dept);
         return View(userSet.ToList());
      }

      // GET: users/Details/5
      public ActionResult Details(int? id)
      {
         if (id == null) {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }
         user user = db.userSet.Find(id);
         if (user == null) {
            return HttpNotFound();
         }
         return PartialView(user);
      }

      // GET: users/Create
      public ActionResult Create()
      {
         ViewBag.dept_id = new SelectList(db.deptSet, "dept_id", "dept_name");
         return View();
      }

      // POST: users/Create
      // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
      // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Create([Bind(Include = "user_id,user_name,dept_id,update_date")] user user)
      {
         if (ModelState.IsValid) {
            user.update_date = DateTime.Now.ToString();
            db.userSet.Add(user);
            db.SaveChanges();
            return RedirectToAction("Index");
         }

         ViewBag.dept_id = new SelectList(db.deptSet, "dept_id", "dept_name", user.dept_id);
         return PartialView(user);
      }

      // GET: users/Edit/5
      public ActionResult Edit(int? id)
      {
         if (id == null) {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }
         user user = db.userSet.Find(id);
         if (user == null) {
            return HttpNotFound();
         }
         ViewBag.dept_id = new SelectList(db.deptSet, "dept_id", "dept_name", user.dept_id);
         return View(user);
      }

      // POST: users/Edit/5
      // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
      // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Edit([Bind(Include = "user_id,user_name,dept_id,update_date")] user user)
      {
         if (ModelState.IsValid) {
            user.update_date = DateTime.Now.ToString();
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
         }
         ViewBag.dept_id = new SelectList(db.deptSet, "dept_id", "dept_name", user.dept_id);
         return View(user);
      }

      // GET: users/Delete/5
      public ActionResult Delete(int? id)
      {
         if (id == null) {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }
         user user = db.userSet.Find(id);
         db.userSet.Remove(user);
         db.SaveChanges();
         if (user == null) {
            return HttpNotFound();
         }
         TempData["message"] = "你已刪除" + user.user_name;
         return RedirectToAction("Index");
      }
      
      protected override void Dispose(bool disposing)
      {
         if (disposing) {
            db.Dispose();
         }
         base.Dispose(disposing);
      }
   }
}
