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
    public class SymptomsController : Controller
    {
        private DataDoctorEntities2 db = new DataDoctorEntities2();
        static public int Diseas_Id { get; set; }

        // GET: Symptoms
        [Authorize(Roles = "Doctor")]
        public ActionResult Index(int? ide)
        {
            if (ide != null)
            {
                Diseas_Id = Convert.ToInt32(ide);
                var symptoms = db.Symptoms.Include(s => s.Disease).Where(d =>d.Disease_Id==ide);
                return View(symptoms.ToList());
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }
        [Authorize(Roles = "General")]
        public ActionResult GeneralIndex(int? ide)
        {
            if (ide != null)
            {
                Diseas_Id = Convert.ToInt32(ide);
                var symptoms = db.Symptoms.Include(s => s.Disease).Where(d => d.Disease_Id == ide);
                return View(symptoms.ToList());
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        // GET: Symptoms/Details/5
        [Authorize(Roles = "Doctor")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Symptom symptom = db.Symptoms.Find(id);
            if (symptom == null)
            {
                return HttpNotFound();
            }
            return View(symptom);
        }

        // GET: Symptoms/Create
        [Authorize(Roles = "Doctor")]
        public ActionResult Create()
        {
            ViewBag.Disease_Id = new SelectList(db.Diseases.Where(d => d.Disease_Id==Diseas_Id), "Disease_Id", "Disease_Name");
            return View();
        }

        // POST: Symptoms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Doctor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Disease_Id,symptom1")] Symptom symptom)
        {
            if (ModelState.IsValid)
            {
                //var dis=db.Diseases.Where(d => d.Disease_Name==symptom.Disease_Id)
                db.Symptoms.Add(symptom);
                db.SaveChanges();
                return RedirectToAction("Index",new { ide=symptom.Disease_Id });
            }

            ViewBag.Disease_Id = new SelectList(db.Diseases, "Disease_Id", "Disease_Name", symptom.Disease_Id);
            return View(symptom);
            //return RedirectToAction("Index", Diseas_Id);
        }

        // GET: Symptoms/Edit/5
        [Authorize(Roles = "Doctor")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Symptom symptom = db.Symptoms.Find(id);
            if (symptom == null)
            {
                return HttpNotFound();
            }
            ViewBag.Disease_Id = new SelectList(db.Diseases, "Disease_Id", "Disease_Name", symptom.Disease_Id);
            return View(symptom);
        }

        // POST: Symptoms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Doctor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Disease_Id,symptom1")] Symptom symptom)
        {
            if (ModelState.IsValid)
            {
                db.Entry(symptom).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Disease_Id = new SelectList(db.Diseases, "Disease_Id", "Disease_Name", symptom.Disease_Id);
            return View(symptom);
        }

        // GET: Symptoms/Delete/5
        [Authorize(Roles = "Doctor")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Symptom symptom = db.Symptoms.Find(id);
            if (symptom == null)
            {
                return HttpNotFound();
            }
            return View(symptom);
        }

        // POST: Symptoms/Delete/5
        [Authorize(Roles = "Doctor")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Symptom symptom = db.Symptoms.Find(id);
            db.Symptoms.Remove(symptom);
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
