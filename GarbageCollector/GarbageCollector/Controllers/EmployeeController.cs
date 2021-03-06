﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
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
    public class EmployeeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Employee
        public ActionResult Index()
        {
            string zip = Request.Form["Zip"];
            var pickupModels = db.PickupModels.Include(p => p.User);
            return View(pickupModels.ToList()
               .Where(n => n.PickupDate.ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd"))
               .Where(n => n.User.Zip.ToString() == zip));
        }

        // GET: Employee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PickupModels pickupModels = db.PickupModels.Find(id);
            if (pickupModels == null)
            {
                return HttpNotFound();
            }
            return View(pickupModels);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.CustomerModels, "UserId", "FirstName");
            return View();
        }

        // POST: Employee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserId,PickupDate")] PickupModels pickupModels)
        {
            if (ModelState.IsValid)
            {
                db.PickupModels.Add(pickupModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.CustomerModels, "UserId", "FirstName", pickupModels.UserId);
            return View(pickupModels);
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PickupModels pickupModels = db.PickupModels.Find(id);
            if (pickupModels == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.CustomerModels, "UserId", "FirstName", pickupModels.UserId);
            return View(pickupModels);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserId,PickupDate")] PickupModels pickupModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pickupModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.CustomerModels, "UserId", "FirstName", pickupModels.UserId);
            return View(pickupModels);
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PickupModels pickupModels = db.PickupModels.Find(id);
            if (pickupModels == null)
            {
                return HttpNotFound();
            }
            return View(pickupModels);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PickupModels pickupModels = db.PickupModels.Find(id);
            db.PickupModels.Remove(pickupModels);
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
