using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Restaurant.Models;

namespace Restaurant.Controllers
{
    public class TablesController : Controller
    {
        private RestaurantEntities db = new RestaurantEntities();

        // GET: Tables
        public ActionResult Index()
        {
            return View(db.Tables.ToList());
        }

        // GET: Tables/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tables tables = db.Tables.Find(id);
            if (tables == null)
            {
                return HttpNotFound();
            }
            return View(tables);
        }

        // GET: Tables/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TbID,TbCode,Password")] Tables tables)
        {
            if (ModelState.IsValid)
            {
                db.Tables.Add(tables);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tables);
        }

        // GET: Tables/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tables tables = db.Tables.Find(id);
            if (tables == null)
            {
                return HttpNotFound();
            }
            return View(tables);
        }

        // POST: Tables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TbID,TbCode,Password")] Tables tables)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tables).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tables);
        }

        // GET: Tables/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tables tables = db.Tables.Find(id);
            if (tables == null)
            {
                return HttpNotFound();
            }
            return View(tables);
        }

        // POST: Tables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            Tables tables = db.Tables.Find(id);
            db.Tables.Remove(tables);
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
