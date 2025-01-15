using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Restaurant.Models;

namespace Restaurant.Controllers
{
    public class WorkSectionsController : Controller
    {
        private RestaurantEntities db = new RestaurantEntities();

        // GET: WorkSections
        public ActionResult Index()
        {
            return View(db.WorkSections.ToList());
        }

        // GET: WorkSections/Details/5
        public ActionResult Details(short? id)
        {
            GetData gd = new GetData();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkSections workSections = db.WorkSections.Find(id);
            if (workSections == null)
            {
                return HttpNotFound();
            }

            string sql = "spGetItemForSection";
            List<SqlParameter> list = new List<SqlParameter> {
                new SqlParameter("id",id)
            };
            var ms = gd.TableQueryBySP(sql, list);

           ViewBag.WorkSections = workSections.Name;
            return View(ms);
        }

        // GET: WorkSections/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WorkSections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SecID,Name")] WorkSections workSections)
        {
            if (ModelState.IsValid)
            {
                db.WorkSections.Add(workSections);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(workSections);
        }

        // GET: WorkSections/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkSections workSections = db.WorkSections.Find(id);
            if (workSections == null)
            {
                return HttpNotFound();
            }
            return View(workSections);
        }

        // POST: WorkSections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SecID,Name")] WorkSections workSections)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workSections).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(workSections);
        }

        // GET: WorkSections/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkSections workSections = db.WorkSections.Find(id);
            if (workSections == null)
            {
                return HttpNotFound();
            }
            return View(workSections);
        }

        // POST: WorkSections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            WorkSections workSections = db.WorkSections.Find(id);
            db.WorkSections.Remove(workSections);
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
