using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Northwind_John.Models;
using Northwind_John.ViewModel;

namespace Northwind_John.Controllers
{
   public class deptsController : Controller
   {
      private ModelContainer db = new ModelContainer();

      // GET: depts
      public ActionResult Index()
      {
         depViewModel dep = new depViewModel();
         dep.depts = db.deptSet.ToList();
         return View(dep);
      }

      // GET: depts/Details/5
      public ActionResult Details(int? id)
      {
         if (id == null) {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }
         dept dept = db.deptSet.Find(id);
         if (dept == null) {
            return HttpNotFound();
         }
         return View(dept);
      }

      // GET: depts/Create
      public ActionResult Create()
      {
         return PartialView();
      }

      // POST: depts/Create
      // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
      // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Create([Bind(Include = "dept_id,dept_name")] depViewModel dept)
      {
         if (ModelState.IsValid) {
            db.deptSet.Add(dept.deptfile);
            db.SaveChanges();
            return RedirectToAction("Index");
         }
         
         return PartialView(dept.deptfile);
      }

      // GET: depts/Edit/5
      public ActionResult Edit(int? id)
      {
         if (id == null) {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }
         dept dept = db.deptSet.Find(id);
         if (dept == null) {
            return HttpNotFound();
         }
         return View(dept);
      }

      // POST: depts/Edit/5
      // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
      // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Edit([Bind(Include = "dept_id,dept_name")] dept dept)
      {
         if (ModelState.IsValid) {
            db.Entry(dept).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
         }
         return View(dept);
      }

      // GET: depts/Delete/5
      public ActionResult Delete(int? id)
      {
         if (id == null) {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }
         dept dept = db.deptSet.Find(id);

         if (dept == null) {
            return HttpNotFound();
         }
         db.deptSet.Remove(dept);
         db.SaveChanges();
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
