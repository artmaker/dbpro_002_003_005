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
    public class DiseasesController : Controller
    {
        private DataDoctorEntities2 db = new DataDoctorEntities2();

        // GET: Diseases
        public ActionResult Index()
        {
            return View(db.Diseases.ToList());
        }

        // GET: Diseases/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Disease disease = db.Diseases.Find(id);
            if (disease == null)
            {
                return HttpNotFound();
            }
            return View(disease);
        }

        // GET: Diseases/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Diseases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Disease_Id,Disease_Name")] Disease disease)
        {
            if (ModelState.IsValid)
            {
                try {
                    db.Diseases.Add(disease);
                    db.SaveChanges();
                    
                }
                catch{
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                return RedirectToAction("Index");
            }

            return View(disease);
        }

        // GET: Diseases/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Disease disease = db.Diseases.Find(id);
            if (disease == null)
            {
                return HttpNotFound();
            }
            return View(disease);
        }

        // POST: Diseases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Disease_Id,Disease_Name")] Disease disease)
        {
            if (ModelState.IsValid)
            {
                db.Entry(disease).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(disease);
        }

        // GET: Diseases/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Disease disease = db.Diseases.Find(id);
            if (disease == null)
            {
                return HttpNotFound();
            }
            return View(disease);
        }

        // POST: Diseases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Disease disease = db.Diseases.Find(id);
            db.Diseases.Remove(disease);
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

        public ActionResult PredictDisease()
        {
            return View(db.Diseases.ToList());
        }
        public ActionResult GetPrediction(string disease)
        {

            string[] symp = disease.Split(',');
            List<Symptom> Syptoms = new List<Symptom>();
            Syptoms = db.Symptoms.Include(d => d.Disease).ToList();
            foreach (string str in symp)
            {
                Syptoms = Syptoms.Where(s => s.symptom1==str).ToList();
            }

            int arf = 4;
            //return View(db.Diseases.ToList());
            if (Syptoms.Count() >= 0)
            {
                return RedirectToAction("Predictions", new { ide = Syptoms[0].Disease_Id });
            }
            else
            {
                return RedirectToAction("Predictions", new { ide = -1 });
            }
        }
        public ActionResult Predictions(int ide)
        {
            if (ide == -1)
            {
                return View(db.Diseases.Where(d => d.Disease_Id == -999999).ToList());
            }
            else {
                return View(db.Diseases.Where(d => d.Disease_Id == ide).ToList());
            }
        }

        public ActionResult PatientDiseases(int ide)
        {
            
            return View(db.Patients.Include(d => d.Disease).Where(p => p.Patient_Id == ide).ToList());
        }
    }
}
