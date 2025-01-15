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
    public class InvoicesController : Controller
    {
        private RestaurantEntities db = new RestaurantEntities();

        // GET: Invoices
        public ActionResult Index()
        {
            var invoices = db.Invoices.Include(i => i.PayMethods);
            return View(invoices.ToList());
        }

        // GET: Invoices/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoices invoices = db.Invoices.Find(id);
            if (invoices == null)
            {
                return HttpNotFound();
            }
            return View(invoices);
        }

        // GET: Invoices/Create
        public ActionResult Create()
        {
            ViewBag.PaymentID = new SelectList(db.PayMethods, "PaymentID", "Method");
            return View();
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InvID,InvoiceTime,Total,Tips,PaymentID")] Invoices invoices)
        {
            if (ModelState.IsValid)
            {
                db.Invoices.Add(invoices);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PaymentID = new SelectList(db.PayMethods, "PaymentID", "Method", invoices.PaymentID);
            return View(invoices);
        }

        // GET: Invoices/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoices invoices = db.Invoices.Find(id);
            if (invoices == null)
            {
                return HttpNotFound();
            }
            ViewBag.PaymentID = new SelectList(db.PayMethods, "PaymentID", "Method", invoices.PaymentID);
            return View(invoices);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InvID,InvoiceTime,Total,Tips,PaymentID")] Invoices invoices)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoices).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PaymentID = new SelectList(db.PayMethods, "PaymentID", "Method", invoices.PaymentID);
            return View(invoices);
        }

        // GET: Invoices/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoices invoices = db.Invoices.Find(id);
            if (invoices == null)
            {
                return HttpNotFound();
            }
            return View(invoices);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Invoices invoices = db.Invoices.Find(id);
            db.Invoices.Remove(invoices);
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
