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
    public class OrderDetailsController : Controller
    {
        private RestaurantEntities db = new RestaurantEntities();

        // GET: OrderDetails
        public ActionResult Index()
        {
            var orderDetails = db.OrderDetails.Include(o => o.Items).Include(o => o.Orders);
            return View(orderDetails.ToList());
        }

        // GET: OrderDetails/Details/5
        public ActionResult Details(short secid)
        {
            var SectionItems =
                from od in db.OrderDetails
                join i in db.Items on od.ItemID equals i.ItemID
                join o in db.Orders on od.OrderID equals o.OrderID
                where i.SecID == secid && od.Served == false
                select new
                {
                    o.TbID,
                    i.DishCode,
                    od.Preparing,
                    od.Served
                };
            var secName = db.WorkSections.Where(w => w.SecID == secid).Select(w => w.Name).FirstOrDefault();
            ViewBag.WorkSections = secName;
            ViewBag.Source = SectionItems;
            return View(SectionItems.ToList());
        }

        // GET: OrderDetails/Create
        public ActionResult Create()
        {
            ViewBag.ItemID = new SelectList(db.Items, "ItemID", "Name");
            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "OrderID");
            return View();
        }

        // POST: OrderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SSN,OrderID,ItemID,OrderTime,Preparing,PreTime,Served,ServedTime,Paid,PaidTime,InvoiceGrp")] OrderDetails orderDetails)
        {
            if (ModelState.IsValid)
            {
                db.OrderDetails.Add(orderDetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ItemID = new SelectList(db.Items, "ItemID", "Name", orderDetails.ItemID);
            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "OrderID", orderDetails.OrderID);
            return View(orderDetails);
        }

        // GET: OrderDetails/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetails orderDetails = db.OrderDetails.Find(id);
            if (orderDetails == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemID = new SelectList(db.Items, "ItemID", "Name", orderDetails.ItemID);
            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "OrderID", orderDetails.OrderID);
            return View(orderDetails);
        }

        // POST: OrderDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SSN,OrderID,ItemID,OrderTime,Preparing,PreTime,Served,ServedTime,Paid,PaidTime,InvoiceGrp")] OrderDetails orderDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ItemID = new SelectList(db.Items, "ItemID", "Name", orderDetails.ItemID);
            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "OrderID", orderDetails.OrderID);
            return View(orderDetails);
        }

        // GET: OrderDetails/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetails orderDetails = db.OrderDetails.Find(id);
            if (orderDetails == null)
            {
                return HttpNotFound();
            }
            return View(orderDetails);
        }

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            OrderDetails orderDetails = db.OrderDetails.Find(id);
            db.OrderDetails.Remove(orderDetails);
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

        public ActionResult CheckBills(string id)
        {
            GetData gd = new GetData();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = db.Orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            string sql = "spGetSplitBill";
            List<SqlParameter> list = new List<SqlParameter> {
                new SqlParameter("id",id)
            };
            var ms = gd.TableQueryBySP(sql, list);

            ViewBag.orders = orders.OrderID;
            return View(ms);
        }
    }
}
