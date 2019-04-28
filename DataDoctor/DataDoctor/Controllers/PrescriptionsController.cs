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
    public class PrescriptionsController : Controller
    {
        private DataDoctorEntities2 db = new DataDoctorEntities2();
        static public int Pat_Id { get; set; }

        // GET: Prescriptions
        public ActionResult Index(int? ide)
        {
            ViewBag.Pat_Id = Convert.ToInt32(ide);
            if (ide != null)
            {
                Pat_Id = Convert.ToInt32(ide);
                var prescriptions = db.Prescriptions.Include(p => p.AspNetUser).Include(p => p.Patient).Where(a => a.AspNetUser.UserName == User.Identity.Name).Where(p => p.patient_Id==Pat_Id);
                return View(prescriptions.ToList());
            }
            else {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        // GET: Prescriptions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prescription prescription = db.Prescriptions.Find(id);
            if (prescription == null)
            {
                return HttpNotFound();
            }
            return View(prescription);
        }

        // GET: Prescriptions/Create
        public ActionResult Create()
        {
            ViewBag.Doctor_Id = new SelectList(db.AspNetUsers, "Id", "Email");
            //Patient pat = new Patient();
            //pat.Patient_Id = Pat_Id;
            ViewBag.patient_Id = new SelectList(db.Patients.Where(P => P.Patient_Id==Pat_Id), "Patient_Id", "Patient_Id");
            //ViewBag.patient_Id = Pat_Id;
            return View();
        }

        // POST: Prescriptions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Pres_Id,patient_Id,St_Date,E_Date,Results,Doctor_Id")] Prescription prescription,int? ide)
        {
            var users = db.Database.SqlQuery<string>(string.Format("Select Id from dbo.AspNetUsers where UserName='{0}'", User.Identity.Name)).ToList();
            string user = users[0];
            if (ModelState.IsValid)
            {
                prescription.Doctor_Id = user;
                //prescription.patient_Id = Convert.ToInt32(ide);
                //prescription.patient_Id = Convert.ToInt32(Pat_Id);
                //prescription.patient_Id = ViewBag.Pat_Id;
                db.Prescriptions.Add(prescription);
                db.SaveChanges();
                return RedirectToAction("Index",new { ide=prescription.patient_Id});
            }

            ViewBag.Doctor_Id = new SelectList(db.AspNetUsers, "Id", "Email", prescription.Doctor_Id);
            ViewBag.patient_Id = new SelectList(db.Patients, "Patient_Id", "Area", prescription.patient_Id);
            return View(prescription);
        }

        // GET: Prescriptions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prescription prescription = db.Prescriptions.Find(id);
            if (prescription == null)
            {
                return HttpNotFound();
            }
            ViewBag.Doctor_Id = new SelectList(db.AspNetUsers, "Id", "Email", prescription.Doctor_Id);
            ViewBag.patient_Id = new SelectList(db.Patients, "Patient_Id", "Area", prescription.patient_Id);
            return View(prescription);
        }

        // POST: Prescriptions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Pres_Id,patient_Id,St_Date,E_Date,Results,Doctor_Id")] Prescription prescription)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prescription).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Doctor_Id = new SelectList(db.AspNetUsers, "Id", "Email", prescription.Doctor_Id);
            ViewBag.patient_Id = new SelectList(db.Patients, "Patient_Id", "Area", prescription.patient_Id);
            return View(prescription);
        }

        // GET: Prescriptions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prescription prescription = db.Prescriptions.Find(id);
            if (prescription == null)
            {
                return HttpNotFound();
            }
            return View(prescription);
        }

        // POST: Prescriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prescription prescription = db.Prescriptions.Find(id);
            db.Prescriptions.Remove(prescription);
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
