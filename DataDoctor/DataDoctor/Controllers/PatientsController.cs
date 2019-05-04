using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataHandler;
using System.Configuration;
using System.Data.SqlClient;

namespace DataDoctor.Controllers
{
    [Authorize]
    public class PatientsController : Controller
    {
        private DataDoctorEntities2 db = new DataDoctorEntities2();
        private SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());

        // GET: Patients
        [Authorize(Roles ="Doctor")]
        public ActionResult Index()
        {
            string name = User.Identity.Name;
            var doc= db.Database.SqlQuery<string>(string.Format("Select Id from dbo.AspNetUsers where UserName='{0}' and Licence is not null",name)).ToList();
            var patients = db.Database.SqlQuery<int>(string.Format("Select Patient_Id from dbo.Pat_Doc where Doctor_Id='{0}'",doc[0])).ToList();
            List<Patient> PatientsInformations=new List<Patient>();
            foreach (var pa in patients)
            {
                Patient pat = db.Patients.Find(pa);
                //Patient pat = db.Patients.FirstOrDefault();
                PatientsInformations.Add(pat);
            }
            //return View(db.Patients.ToList());
            return View(PatientsInformations.ToList());
        }

        public ActionResult GeneralIndex()
        {
            string name = User.Identity.Name;
            //var doc = db.Database.SqlQuery<string>(string.Format("Select Id from dbo.AspNetUsers where UserName='{0}' and Licence is not null", name)).ToList();
            var patients = db.Database.SqlQuery<int>(string.Format("Select Patient_Id from dbo.Pat_Doc")).ToList();
            List<Patient> PatientsInformations = new List<Patient>();
            foreach (var pa in patients)
            {
                Patient pat = db.Patients.Find(pa);
                //Patient pat = db.Patients.FirstOrDefault();
                PatientsInformations.Add(pat);
            }
            //return View(db.Patients.ToList());
            return View(PatientsInformations.ToList());
        }


        // GET: Patients/Details/5
        [Authorize(Roles = "Doctor")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // GET: Patients/Create
        [Authorize(Roles = "Doctor")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Doctor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Patient_Id,S_Date,E_Date,Area,Remarks,Gender,Status")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                

                db.Patients.Add(patient);
                db.SaveChanges();
                


                var users = db.Database.SqlQuery<string>(string.Format("Select Id from dbo.AspNetUsers where UserName='{0}'", User.Identity.Name)).ToList();
                string user = users[0];
                string query = string.Format("Insert into Pat_Doc values((Select max(Patient_Id) from Patient),'{0}')",user);
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
                return RedirectToAction("Index");
            }

            return View(patient);
        }

        // GET: Patients/Edit/5
        [Authorize(Roles = "Doctor")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Doctor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Patient_Id,S_Date,E_Date,Area,Remarks,Gender,Status")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(patient);
        }

        // GET: Patients/Delete/5
        [Authorize(Roles = "Doctor")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: Patients/Delete/5
        [Authorize(Roles = "Doctor")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            /*
            var patient = db.Patients.FirstOrDefault(o => o.Patient_Id == id);
            if (patient != null)
            {
                db.Patients.Remove(patient);
                db.SaveChanges();
            }*/

            //var doc_pat = db.FirstOrDefault(o => o.Patient_Id == id);
            
            string query = string.Format("Delete from Pat_Doc where Patient_Id={0}", id);
            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();

            //var rows = db.Database.SqlQuery<int>(string.Format("Delete from Pat_Doc where Patient_Id ={0}", id));
            Patient patient = db.Patients.Find(id);
            db.Patients.Remove(patient);
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
