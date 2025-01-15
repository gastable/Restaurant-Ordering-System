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
    public class PayMethodsController : Controller
    {
        private RestaurantEntities db = new RestaurantEntities();

        // GET: PayMethods
        public ActionResult Index()
        {
            return View(db.PayMethods.ToList());
        }

        // GET: PayMethods/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PayMethods payMethods = db.PayMethods.Find(id);
            if (payMethods == null)
            {
                return HttpNotFound();
            }
            return View(payMethods);
        }

        // GET: PayMethods/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PayMethods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PaymentID,Method")] PayMethods payMethods)
        {
            if (ModelState.IsValid)
            {
                db.PayMethods.Add(payMethods);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(payMethods);
        }

        // GET: PayMethods/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PayMethods payMethods = db.PayMethods.Find(id);
            if (payMethods == null)
            {
                return HttpNotFound();
            }
            return View(payMethods);
        }

        // POST: PayMethods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PaymentID,Method")] PayMethods payMethods)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payMethods).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(payMethods);
        }

        // GET: PayMethods/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PayMethods payMethods = db.PayMethods.Find(id);
            if (payMethods == null)
            {
                return HttpNotFound();
            }
            return View(payMethods);
        }

        // POST: PayMethods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            PayMethods payMethods = db.PayMethods.Find(id);
            db.PayMethods.Remove(payMethods);
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
