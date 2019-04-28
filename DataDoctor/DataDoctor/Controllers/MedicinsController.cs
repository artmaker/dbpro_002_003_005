using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataHandler;

namespace DataDoctor.Controllers
{
    public class MedicinsController : Controller
    {
        private DataDoctorEntities2 db = new DataDoctorEntities2();

        // GET: Medicins
        public ActionResult Index()
        {
            var medicins = db.Medicins.Include(m => m.MedicinRank);
            return View(medicins.ToList());
        }

        // GET: Medicins/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicin medicin = db.Medicins.Find(id);
            if (medicin == null)
            {
                return HttpNotFound();
            }
            return View(medicin);
        }

        // GET: Medicins/Create
        public ActionResult Create()
        {
            ViewBag.Med_Id = new SelectList(db.MedicinRanks, "Med_Id", "Doctor_Id");
            return View();
        }

        // POST: Medicins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Med_Id,Name,Potency,Company")] Medicin medicin)
        {
            if (ModelState.IsValid)
            {
                db.Medicins.Add(medicin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Med_Id = new SelectList(db.MedicinRanks, "Med_Id", "Doctor_Id", medicin.Med_Id);
            return View(medicin);
        }

        // GET: Medicins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicin medicin = db.Medicins.Find(id);
            if (medicin == null)
            {
                return HttpNotFound();
            }
            ViewBag.Med_Id = new SelectList(db.MedicinRanks, "Med_Id", "Doctor_Id", medicin.Med_Id);
            return View(medicin);
        }

        // POST: Medicins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Med_Id,Name,Potency,Company")] Medicin medicin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medicin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Med_Id = new SelectList(db.MedicinRanks, "Med_Id", "Doctor_Id", medicin.Med_Id);
            return View(medicin);
        }

        // GET: Medicins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicin medicin = db.Medicins.Find(id);
            if (medicin == null)
            {
                return HttpNotFound();
            }
            return View(medicin);
        }

        // POST: Medicins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Medicin medicin = db.Medicins.Find(id);
            db.Medicins.Remove(medicin);
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
