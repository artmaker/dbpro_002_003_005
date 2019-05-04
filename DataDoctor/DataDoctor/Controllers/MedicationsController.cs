using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataHandler;
using DataDoctor.Models;

namespace DataDoctor.Controllers
{
    
    public class MedicationsController : Controller
    {
        private DataDoctorEntities2 db = new DataDoctorEntities2();
        static public int pres_id { get; set; }
        // GET: Medications
        [Authorize(Roles = "Doctor")]
        public ActionResult Index(int? ide)
        {
            
            if (ide != null)
            {
                pres_id = Convert.ToInt32(ide);
                //IEnumerable<SelectListItem> list = new IEnumerable<SelectListItem>();
                //var medications = db.Medications.Include(m => m.Medicin).Include(m => m.Medicin1);
                var medications = db.Medications.Where(me => me.Pres_Id == ide);
                List<Medicates> med = new List<Medicates>();
                foreach (var item in medications)
                {
                    Medicates md = new Medicates();
                    var med_names = db.Database.SqlQuery<string>(string.Format("Select Name from dbo.Medicin where Med_Id={0}", item.Med_Id)).ToList();
                    string name = med_names[0];
                    md.Pres_Id = item.Pres_Id;
                    md.Med_Name = name;
                    md.timings = item.timings;
                    med.Add(md);

                }

                //return View(medications.ToList());
                return View(med);
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
                pres_id = Convert.ToInt32(ide);
                //IEnumerable<SelectListItem> list = new IEnumerable<SelectListItem>();
                //var medications = db.Medications.Include(m => m.Medicin).Include(m => m.Medicin1);
                var medications = db.Medications.Where(me => me.Pres_Id == ide);
                List<Medicates> med = new List<Medicates>();
                foreach (var item in medications)
                {
                    Medicates md = new Medicates();
                    var med_names = db.Database.SqlQuery<string>(string.Format("Select Name from dbo.Medicin where Med_Id={0}", item.Med_Id)).ToList();
                    string name = med_names[0];
                    md.Pres_Id = item.Pres_Id;
                    md.Med_Name = name;
                    md.timings = item.timings;
                    med.Add(md);

                }

                //return View(medications.ToList());
                return View(med);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        // GET: Medications/Details/5
        [Authorize(Roles = "Doctor")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medication medication = db.Medications.Find(id);
            if (medication == null)
            {
                return HttpNotFound();
            }
            return View(medication);
        }

        // GET: Medications/Create
        [Authorize(Roles = "Doctor")]
        public ActionResult Create()
        {
            ViewBag.Med_Id = new SelectList(db.Medicins, "Med_Id", "Name");
            ViewBag.Pres_Id = new SelectList(db.Prescriptions.Where(p => p.Pres_Id==pres_id), "Pres_Id", "Pres_Id");
            return View();
        }

        // POST: Medications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Doctor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Pres_Id,Med_Name,timings")] Medicates medication)
        {
            Medication med = new Medication();
            try
            {
                if (ModelState.IsValid)
                {

                    var med_ids = db.Database.SqlQuery<int>(string.Format("Select Med_Id from dbo.Medicin where Name='{0}'", medication.Med_Name)).ToList();
                    int med_id = med_ids[0];
                    med.Med_Id = med_id;
                    med.Pres_Id = medication.Pres_Id;
                    med.timings = medication.timings;
                    //med.Med_Id = db.Medicins.Where(me => me.Name == medication.Med_Name);

                    db.Medications.Add(med);
                    db.SaveChanges();
                    return RedirectToAction("Index", medication.Pres_Id);
                }
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.Med_Id = new SelectList(db.Medicins, "Med_Id", "Name", med.Med_Id);
            ViewBag.Med_Id = new SelectList(db.Medicins, "Med_Id", "Name", med.Med_Id);
            //return View(medication);
            return RedirectToAction("Index", med.Pres_Id);
        }




        // GET: Medications/Edit/5
        [Authorize(Roles = "Doctor")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medication medication = db.Medications.Find(id);
            if (medication == null)
            {
                return HttpNotFound();
            }
            ViewBag.Med_Id = new SelectList(db.Medicins, "Med_Id", "Name", medication.Med_Id);
            ViewBag.Med_Id = new SelectList(db.Medicins, "Med_Id", "Name", medication.Med_Id);
            return View(medication);
        }

        // POST: Medications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Doctor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Pres_Id,Med_Id,timings")] Medication medication)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medication).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Med_Id = new SelectList(db.Medicins, "Med_Id", "Name", medication.Med_Id);
            ViewBag.Med_Id = new SelectList(db.Medicins, "Med_Id", "Name", medication.Med_Id);
            return View(medication);
        }

        // GET: Medications/Delete/5
        [Authorize(Roles = "Doctor")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medication medication = db.Medications.Find(id);
            if (medication == null)
            {
                return HttpNotFound();
            }
            return View(medication);
        }

        // POST: Medications/Delete/5
        [Authorize(Roles = "Doctor")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Medication medication = db.Medications.Find(id);
            db.Medications.Remove(medication);
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
