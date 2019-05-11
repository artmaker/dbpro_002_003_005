using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataHandler;
using System.Data.SqlClient;
using System.Configuration;

namespace DataDoctor.Controllers
{
    [Authorize]
    public class DiseasesController : Controller
    {
        private DataDoctorEntities2 db = new DataDoctorEntities2();
        private SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
        static public int Pat_Id { get; set; }
        // GET: Diseases
        [Authorize(Roles = "Doctor")]
        public ActionResult Index()
        {
            return View(db.Diseases.ToList());
        }
        [Authorize(Roles = "General")]
        public ActionResult GeneralIndex()
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
        [Authorize(Roles = "Doctor")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Diseases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Doctor")]
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
        [Authorize(Roles = "Doctor")]
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
        [Authorize(Roles = "Doctor")]
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
        [Authorize(Roles = "Doctor")]
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
        [Authorize(Roles = "Doctor")]
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
        [Authorize(Roles ="Doctor")]
        public ActionResult PredictDisease()
        {
            return View(db.Diseases.ToList());
        }
        [Authorize(Roles = "General")]
        public ActionResult GeneralPredictDisease()
        {
            return View(db.Diseases.ToList());
        }
        [Authorize(Roles = "Doctor")]
        public ActionResult GetPrediction(string disease)
        {

            string[] symp = disease.Split(',');
            List<Symptom> Syptoms = new List<Symptom>();
            List<int> pre = new List<int>();
            //Syptoms = db.Symptoms.Include(d => d.Disease).ToList();

            foreach (string str in symp)
            {
                Syptoms = db.Symptoms.Include(d => d.Disease).ToList();
                Syptoms = Syptoms.Where(s => s.symptom1 == str).ToList();
                if (Syptoms.Count() > 0)
                {
                    //pre.Add(Syptoms[0].Disease_Id);
                    foreach (var item in Syptoms)
                    {
                        pre.Add(item.Disease_Id);
                    }
                }
            }
            int max = 0;
            int idd = -9999;
            foreach (int item in pre)
            {
                int count = pre.Where(x => x.Equals(item)).Count();
                if (max < count)
                {
                    idd = item;
                    max = count;
                }
            }

            //foreach (string str in symp)
            //{
            //    Syptoms = Syptoms.Where(s => s.symptom1 == str).ToList();
            //}

            if (idd != -9999)
            {

                //if (Syptoms.Count() > 0)
                //{
                //    return RedirectToAction("GeneralPredictions", new { ide = Syptoms[0].Disease_Id });
                //}
                return RedirectToAction("Predictions", new { ide = idd });
            }
            else
            {
                return RedirectToAction("Predictions", new { ide = -1 });
            }
        }
        [Authorize(Roles = "General")]
        public ActionResult GeneralGetPrediction(string disease)
        {

            string[] symp = disease.Split(',');
            List<Symptom> Syptoms = new List<Symptom>();
            List<int> pre = new List<int>();
            //Syptoms = db.Symptoms.Include(d => d.Disease).ToList();

            foreach (string str in symp)
                {
                    Syptoms = db.Symptoms.Include(d => d.Disease).ToList();
                    Syptoms = Syptoms.Where(s => s.symptom1 == str).ToList();
                    if (Syptoms.Count() > 0)
                    {
                    //pre.Add(Syptoms[0].Disease_Id);
                        foreach (var item in Syptoms)
                        {
                        pre.Add(item.Disease_Id);
                        }   
                    }
               }
            int max = 0;
            int idd = -9999;
            foreach (int item in pre)
            {
                int count = pre.Where(x => x.Equals(item)).Count();
                if (max < count)
                {
                    idd = item;
                    max = count;
                }
            }

            //foreach (string str in symp)
            //{
            //    Syptoms = Syptoms.Where(s => s.symptom1 == str).ToList();
            //}

            if (idd != -9999)
            {

                //if (Syptoms.Count() > 0)
                //{
                //    return RedirectToAction("GeneralPredictions", new { ide = Syptoms[0].Disease_Id });
                //}
                return RedirectToAction("GeneralPredictions", new { ide = idd });
            }
            else
            {
                return RedirectToAction("GeneralPredictions", new { ide = -1 });
            }
        }
        [Authorize(Roles = "Doctor")]
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
        [Authorize(Roles = "General")]
        public ActionResult GeneralPredictions(int ide)
        {
            if (ide == -1)
            {
                return View(db.Diseases.Where(d => d.Disease_Id == -999999).ToList());
            }
            else {
                return View(db.Diseases.Where(d => d.Disease_Id == ide).ToList());
            }
        }
        [Authorize(Roles = "Doctor")]
        public ActionResult PatientDiseases(int ide)
        {
            Pat_Id = ide;
            //string query = string.Format("select Disease from  Pat_Die inner join  Patient  ON Pat_Die.Patient_Id=Patient.Patient_Id where Patient.Doctor_Id=(Select Id from AspNetUsers where UserName='{0}')",User.Identity.Name);
            string query = string.Format("select Disease.Disease_Id,Disease.Disease_Name from Pat_Doc inner join (Disease inner join (Pat_Die inner join  Patient  ON Pat_Die.Patient_Id = Patient.Patient_Id) on Disease.Disease_Id = Pat_Die.Disease_Id) on Pat_Doc.Patient_Id = Patient.Patient_Id where Pat_Doc.Doctor_Id = (Select Id from AspNetUsers where UserName = '{0}') and Patient.Patient_Id={1}", User.Identity.Name,ide);
            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader= cmd.ExecuteReader();
            List<Disease> list = new List<Disease>();
            List<string> diseases = new List<string>();
            while (reader.Read())
            {
                Disease dis = new Disease();
                dis.Disease_Id = Convert.ToInt32(reader[0]);
                dis.Disease_Name = reader[1].ToString();
                diseases.Add(dis.Disease_Name);
                list.Add(dis);
            }
            ViewBag.Diseases = new SelectList(db.Diseases,"Disease_Id","Disease_Name");
            connection.Close();
            //return View(db.Patients.Include(d => d.Disease).Where(p => p.Patient_Id == ide).ToList());
            return View(list);
        }
        [Authorize(Roles = "General")]
        public ActionResult GeneralPatientDiseases(int ide)
        {
            Pat_Id = ide;
            //string query = string.Format("select Disease from  Pat_Die inner join  Patient  ON Pat_Die.Patient_Id=Patient.Patient_Id where Patient.Doctor_Id=(Select Id from AspNetUsers where UserName='{0}')",User.Identity.Name);
            //string query = string.Format("select Disease.Disease_Id,Disease.Disease_Name from Pat_Doc inner join (Disease inner join (Pat_Die inner join  Patient  ON Pat_Die.Patient_Id = Patient.Patient_Id) on Disease.Disease_Id = Pat_Die.Disease_Id) on Pat_Doc.Patient_Id = Patient.Patient_Id where Pat_Doc.Doctor_Id = (Select Id from AspNetUsers where UserName = '{0}') and Patient.Patient_Id={1}", User.Identity.Name, ide);
            string query = string.Format("select Disease.Disease_Id,Disease.Disease_Name from Pat_Doc inner join (Disease inner join (Pat_Die inner join  Patient  ON Pat_Die.Patient_Id = Patient.Patient_Id) on Disease.Disease_Id = Pat_Die.Disease_Id) on Pat_Doc.Patient_Id = Patient.Patient_Id where Patient.Patient_Id={0}",ide);
            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<Disease> list = new List<Disease>();
            List<string> diseases = new List<string>();
            while (reader.Read())
            {
                Disease dis = new Disease();
                dis.Disease_Id = Convert.ToInt32(reader[0]);
                dis.Disease_Name = reader[1].ToString();
                diseases.Add(dis.Disease_Name);
                list.Add(dis);
            }
            ViewBag.Diseases = new SelectList(db.Diseases, "Disease_Id", "Disease_Name");
            connection.Close();
            //return View(db.Patients.Include(d => d.Disease).Where(p => p.Patient_Id == ide).ToList());
            return View(list);
        }
        [Authorize(Roles = "Doctor")]
        public ActionResult AddPatientDisease()
        {
            return View(db.Diseases.ToList());

        }
        [Authorize(Roles = "Doctor")]
        public ActionResult AddDis(FormCollection formcollection)
        {
            int a = Pat_Id;
            try {
                int Dis_Id = Convert.ToInt32(formcollection["Diseases"]);
                string query = string.Format("Insert into Pat_Die values({0},{1})", Pat_Id, Dis_Id);
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch 
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return RedirectToAction("PatientDiseases", new { ide = Pat_Id });
            }
            return RedirectToAction("PatientDiseases",new { ide=Pat_Id});
        }
    }
}
