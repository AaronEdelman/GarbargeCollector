using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GarbageCollector.Models;
using System.Web.Security;
using Microsoft.AspNet.Identity;

namespace GarbageCollector.Controllers
{
    public class CustomerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ApplicationUserManager _userManager;
        
        // GET: Customer
        public ActionResult Index()
        {
            string id = User.Identity.GetUserId();
            CustomerModels customerModels = db.CustomerModels.Find(id);
            if (customerModels == null)
            {
                return View("Address");
            }
            else
            {
                return View(db.CustomerModels.ToList().Where(n => n.UserId == User.Identity.GetUserId()));
            }
        }

        // GET: Customer/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerModels customerModels = db.CustomerModels.Find(id);
            if (customerModels == null)
            {
                return HttpNotFound();
            }
            return View(customerModels);
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,FirstName,LastName,Number,Street,City,State,Zip")] CustomerModels customerModels)
        {
            if (ModelState.IsValid)
            {
                customerModels.UserId = User.Identity.GetUserId();
                db.CustomerModels.Add(customerModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customerModels);
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerModels customerModels = db.CustomerModels.Find(id);
            if (customerModels == null)
            {
                return HttpNotFound();
            }
            return View(customerModels);
        }

        // POST: Customer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,Number,Street,City,State,Zip")] CustomerModels customerModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customerModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customerModels);
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerModels customerModels = db.CustomerModels.Find(id);
            if (customerModels == null)
            {
                return HttpNotFound();
            }
            return View(customerModels);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CustomerModels customerModels = db.CustomerModels.Find(id);
            db.CustomerModels.Remove(customerModels);
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
        public ActionResult Address()
        {
            return View();
        }
        public ActionResult Pickup()
        {
            return View();
        }
        public ActionResult Bill()
        {
            return View();
        }
    }
}
