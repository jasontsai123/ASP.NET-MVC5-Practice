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
      public ActionResult Index(string Genre, string searchString)
      {
         //https://dotblogs.com.tw/berrynote/2016/08/26/000310
         //Index方法新增了一個叫做Genre的參數。在前幾行的程式碼中，先建立了一個List與查詢所有部門(Genre)的LINQ。 
         var GenreLst = new List<string>();
         
         var GenreQry = from d in db.userSet
                        orderby d.dept.dept_name
                        select d.dept.dept_name;
         //AddRange方法將剛剛GenreQry查詢出來的資料( Distinct()方法避免重複的部門 )加入GenreLst，再將GenreLst儲存到ViewBag.Genre。
         //而下面的程式碼中，會先檢查Genre是否為空或是Null。若有值則加入Where條件，依照Genre篩選員工。
         GenreLst.AddRange(GenreQry.Distinct());
         ViewBag.Genre = new SelectList(GenreLst);

         var users = from m in db.userSet
                      select m;

         if (!String.IsNullOrEmpty(searchString)) {
            users = users.Where(s => s.user_name.Contains(searchString));
         }

         if (!string.IsNullOrEmpty(Genre)) {
            users = users.Where(x => x.dept.dept_name == Genre);
         }
         //var userSet = db.userSet.Include(u => u.dept);
         return View(users);
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
