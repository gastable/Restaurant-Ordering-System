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
    public class InvoiceDetailsController : Controller
    {
        private RestaurantEntities db = new RestaurantEntities();

        // GET: InvoiceDetails
        public ActionResult Index()
        {
            var invoiceDetails = db.InvoiceDetails.Include(i => i.Invoices).Include(i => i.Orders);
            return View(invoiceDetails.ToList());
        }

        // GET: InvoiceDetails/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceDetails invoiceDetails = db.InvoiceDetails.Find(id);
            if (invoiceDetails == null)
            {
                return HttpNotFound();
            }
            return View(invoiceDetails);
        }

        // GET: InvoiceDetails/Create
        public ActionResult Create()
        {
            ViewBag.InvID = new SelectList(db.Invoices, "InvID", "InvID");
            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "OrderID");
            return View();
        }

        // POST: InvoiceDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SSN,InvID,OrderID,SubTotal")] InvoiceDetails invoiceDetails)
        {
            if (ModelState.IsValid)
            {
                db.InvoiceDetails.Add(invoiceDetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InvID = new SelectList(db.Invoices, "InvID", "InvID", invoiceDetails.InvID);
            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "OrderID", invoiceDetails.OrderID);
            return View(invoiceDetails);
        }

        // GET: InvoiceDetails/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceDetails invoiceDetails = db.InvoiceDetails.Find(id);
            if (invoiceDetails == null)
            {
                return HttpNotFound();
            }
            ViewBag.InvID = new SelectList(db.Invoices, "InvID", "InvID", invoiceDetails.InvID);
            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "OrderID", invoiceDetails.OrderID);
            return View(invoiceDetails);
        }

        // POST: InvoiceDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SSN,InvID,OrderID,SubTotal")] InvoiceDetails invoiceDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoiceDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InvID = new SelectList(db.Invoices, "InvID", "InvID", invoiceDetails.InvID);
            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "OrderID", invoiceDetails.OrderID);
            return View(invoiceDetails);
        }

        // GET: InvoiceDetails/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceDetails invoiceDetails = db.InvoiceDetails.Find(id);
            if (invoiceDetails == null)
            {
                return HttpNotFound();
            }
            return View(invoiceDetails);
        }

        // POST: InvoiceDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            InvoiceDetails invoiceDetails = db.InvoiceDetails.Find(id);
            db.InvoiceDetails.Remove(invoiceDetails);
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
