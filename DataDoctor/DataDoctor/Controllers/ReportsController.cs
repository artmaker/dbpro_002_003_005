using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataHandler;
using DataDoctor.Models;
using DataDoctor.Reports;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;

namespace DataDoctor.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        private DataDoctorEntities2 db = new DataDoctorEntities2();
        private SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GeneralIndex()
        {
            return View();
        }

        //Doctor's Patients
        public ActionResult DP()
        {
            //NorthwindEntities db = new NorthwindEntities();


            //var c = (from b in db.Customers select b).ToList();
            string query = string.Format("Select AspNetUsers.Name, Pat_Doc.Patient_Id from AspNetUsers inner join Pat_Doc on Pat_Doc.Doctor_Id=AspNetUsers.Id");
            SqlCommand cmd = new SqlCommand(query,connection);
            connection.Open();
            //SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            //DataSet ds = new DataSet("CRDataSet");
            //ds.Tables.Add(datatable);
            SqlDataReader reader = cmd.ExecuteReader();
            List<P_D> Pats = new List<P_D>();
            while (reader.Read())
            {
                P_D pa = new P_D();
                pa.Name = reader[0].ToString();
                pa.Patient_Id = Convert.ToInt32(reader[1]);
                //pa.E_Date = Convert.ToDateTime(reader[2]);
                //pa.Area = reader[3].ToString();
                //pa.Remarks = reader[4].ToString();
                //pa.Gender = reader[5].ToString();
                //pa.Status = reader[6].ToString();
                Pats.Add(pa);
            }
            connection.Close();

            
            check4 rptH = new check4();

            
            rptH.Load();
            rptH.SetDataSource(Pats);
            
            Stream s = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            
            return File(s, "application/pdf");



            //PatDoc rpt = new PatDoc();
            //rpt.Load();
            //rpt.SetDataSource(Pats);
            //Stream s = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            //return File(s, "application/pdf");
        }
        //Patient Disease Count
        public ActionResult PDC()
        {
            //NorthwindEntities db = new NorthwindEntities();


            //var c = (from b in db.Customers select b).ToList();
            //string query = string.Format("Select AspNetUsers.Name, Pat_Doc.Patient_Id from AspNetUsers inner join Pat_Doc on Pat_Doc.Doctor_Id=AspNetUsers.Id");
            string query = string.Format("Select Disease.Disease_Name,COUNT(*) from Patient inner join  (Pat_Die inner join Disease on Pat_Die.Disease_Id = Disease.Disease_Id) on Patient.Patient_Id = Pat_Die.Patient_Id Group by Disease.Disease_Name");
            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();
            //SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            //DataSet ds = new DataSet("CRDataSet");
            //ds.Tables.Add(datatable);
            SqlDataReader reader = cmd.ExecuteReader();
            List<Patient_Diseases_Count> Pats = new List<Patient_Diseases_Count>();
            while (reader.Read())
            {
                Patient_Diseases_Count pa = new Patient_Diseases_Count();
                pa.Disease_Name = reader[0].ToString();
                pa.Count = Convert.ToInt32(reader[1]);
                //pa.E_Date = Convert.ToDateTime(reader[2]);
                //pa.Area = reader[3].ToString();
                //pa.Remarks = reader[4].ToString();
                //pa.Gender = reader[5].ToString();
                //pa.Status = reader[6].ToString();
                Pats.Add(pa);
            }
            connection.Close();


            PatientDiseasesCount rptH = new PatientDiseasesCount();


            rptH.Load();
            rptH.SetDataSource(Pats);

            Stream s = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);

            return File(s, "application/pdf");



            //PatDoc rpt = new PatDoc();
            //rpt.Load();
            //rpt.SetDataSource(Pats);
            //Stream s = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            //return File(s, "application/pdf");
        }
        //Patient Diseases Count Dead
        public ActionResult PDCD()
        {
            //NorthwindEntities db = new NorthwindEntities();



            //string query = string.Format("Select Disease.Disease_Name,COUNT(*) from Patient inner join  (Pat_Die inner join Disease on Pat_Die.Disease_Id = Disease.Disease_Id) on Patient.Patient_Id = Pat_Die.Patient_Id Group by Disease.Disease_Name");
            string query = string.Format("Select Disease.Disease_Name,COUNT(*) from Patient inner join (Pat_Die inner join Disease on Pat_Die.Disease_Id=Disease.Disease_Id) on Patient.Patient_Id=Pat_Die.Patient_Id  where Patient.Status='Dead' Group by Disease.Disease_Name");
            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();
            //SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            //DataSet ds = new DataSet("CRDataSet");
            //ds.Tables.Add(datatable);
            SqlDataReader reader = cmd.ExecuteReader();
            List<Patient_Diseases_Count> Pats = new List<Patient_Diseases_Count>();
            while (reader.Read())
            {
                Patient_Diseases_Count pa = new Patient_Diseases_Count();
                pa.Disease_Name = reader[0].ToString();
                pa.Count = Convert.ToInt32(reader[1]);
                //pa.E_Date = Convert.ToDateTime(reader[2]);
                //pa.Area = reader[3].ToString();
                //pa.Remarks = reader[4].ToString();
                //pa.Gender = reader[5].ToString();
                //pa.Status = reader[6].ToString();
                Pats.Add(pa);
            }
            connection.Close();


            PatientDiseaseCountDead rptH = new PatientDiseaseCountDead();


            rptH.Load();
            rptH.SetDataSource(Pats);

            Stream s = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);

            return File(s, "application/pdf");



            //PatDoc rpt = new PatDoc();
            //rpt.Load();
            //rpt.SetDataSource(Pats);
            //Stream s = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            //return File(s, "application/pdf");
        }

        //Medicin Comapny
        public ActionResult MC()
        {
            //NorthwindEntities db = new NorthwindEntities();



            string query = string.Format("Select Medicin.Name,Medicin.Company from Medicin");
            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();
            //SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            //DataSet ds = new DataSet("CRDataSet");
            //ds.Tables.Add(datatable);
            SqlDataReader reader = cmd.ExecuteReader();
            List<Medicin_Company> Pats = new List<Medicin_Company>();
            while (reader.Read())
            {
                Medicin_Company pa = new Medicin_Company();
                pa.Midicin_Name = reader[0].ToString();
                pa.Company_Name = reader[1].ToString();
                //pa.E_Date = Convert.ToDateTime(reader[2]);
                //pa.Area = reader[3].ToString();
                //pa.Remarks = reader[4].ToString();
                //pa.Gender = reader[5].ToString();
                //pa.Status = reader[6].ToString();
                Pats.Add(pa);
            }
            connection.Close();


            MedicinCompany1 rptH = new MedicinCompany1();


            rptH.Load();
            rptH.SetDataSource(Pats);

            Stream s = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);

            return File(s, "application/pdf");



            //PatDoc rpt = new PatDoc();
            //rpt.Load();
            //rpt.SetDataSource(Pats);
            //Stream s = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            //return File(s, "application/pdf");
        }

        // Patient Year
        public ActionResult PY()
        {
            //NorthwindEntities db = new NorthwindEntities();



            string query = string.Format("Select YEAR(Patient.S_Date),COUNT(*) from Patient Group by YEAR(Patient.S_Date)");
            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();
            //SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            //DataSet ds = new DataSet("CRDataSet");
            //ds.Tables.Add(datatable);
            SqlDataReader reader = cmd.ExecuteReader();
            List<Patient_Year> Pats = new List<Patient_Year>();
            while (reader.Read())
            {
                Patient_Year pa = new Patient_Year();
                pa.Year = Convert.ToInt32(reader[0]);
                pa.Count = Convert.ToInt32(reader[1]);
                //pa.E_Date = Convert.ToDateTime(reader[2]);
                //pa.Area = reader[3].ToString();
                //pa.Remarks = reader[4].ToString();
                //pa.Gender = reader[5].ToString();
                //pa.Status = reader[6].ToString();
                Pats.Add(pa);
            }
            connection.Close();


            PatientYear rptH = new PatientYear();


            rptH.Load();
            rptH.SetDataSource(Pats);

            Stream s = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);

            return File(s, "application/pdf");



            //PatDoc rpt = new PatDoc();
            //rpt.Load();
            //rpt.SetDataSource(Pats);
            //Stream s = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            //return File(s, "application/pdf");
        }
        // Diseases Symptoms

        public ActionResult DS()
        {
            //NorthwindEntities db = new NorthwindEntities();



            string query = string.Format("Select Disease_Name,symptom from Disease inner join Symptoms on Disease.Disease_Id=Symptoms.Disease_Id");
            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();
            //SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            //DataSet ds = new DataSet("CRDataSet");
            //ds.Tables.Add(datatable);
            SqlDataReader reader = cmd.ExecuteReader();
            List<Diseases_Symptoms> Pats = new List<Diseases_Symptoms>();
            while (reader.Read())
            {
                Diseases_Symptoms pa = new Diseases_Symptoms();
                pa.Disease_Name = reader[0].ToString();
                pa.symptom = reader[1].ToString();
                //pa.E_Date = Convert.ToDateTime(reader[2]);
                //pa.Area = reader[3].ToString();
                //pa.Remarks = reader[4].ToString();
                //pa.Gender = reader[5].ToString();
                //pa.Status = reader[6].ToString();
                Pats.Add(pa);
            }
            connection.Close();


            DiseasesSymptoms rptH = new DiseasesSymptoms();


            rptH.Load();
            rptH.SetDataSource(Pats);

            Stream s = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);

            return File(s, "application/pdf");



            //PatDoc rpt = new PatDoc();
            //rpt.Load();
            //rpt.SetDataSource(Pats);
            //Stream s = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            //return File(s, "application/pdf");
        }

        //Patient Country
        public ActionResult PC()
        {
            //NorthwindEntities db = new NorthwindEntities();



            string query = string.Format("Select Patient.Area,Count(*) from Patient Group by Patient.Area");
            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();
            //SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            //DataSet ds = new DataSet("CRDataSet");
            //ds.Tables.Add(datatable);
            SqlDataReader reader = cmd.ExecuteReader();
            List<Patient_Country> Pats = new List<Patient_Country>();
            while (reader.Read())
            {
                Patient_Country pa = new Patient_Country();
                pa.Country = reader[0].ToString();
                pa.Count = Convert.ToInt32(reader[1]);
                //pa.E_Date = Convert.ToDateTime(reader[2]);
                //pa.Area = reader[3].ToString();
                //pa.Remarks = reader[4].ToString();
                //pa.Gender = reader[5].ToString();
                //pa.Status = reader[6].ToString();
                Pats.Add(pa);
            }
            connection.Close();


            PatientCountry rptH = new PatientCountry();


            rptH.Load();
            rptH.SetDataSource(Pats);

            Stream s = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);

            return File(s, "application/pdf");



            //PatDoc rpt = new PatDoc();
            //rpt.Load();
            //rpt.SetDataSource(Pats);
            //Stream s = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            //return File(s, "application/pdf");
        }
        // All Medicins
        public ActionResult AM()
        {
            //NorthwindEntities db = new NorthwindEntities();



            string query = string.Format("Select Medicin.Name,Medicin.Potency,Medicin.Company from Medicin");
            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();
            //SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            //DataSet ds = new DataSet("CRDataSet");
            //ds.Tables.Add(datatable);
            SqlDataReader reader = cmd.ExecuteReader();
            List<All_Medicins> Pats = new List<All_Medicins>();
            while (reader.Read())
            {
                All_Medicins pa = new All_Medicins();
                pa.Name = reader[0].ToString();
                pa.Potency = (float)Convert.ToDouble(reader[1]);
                pa.Company= reader[2].ToString();
                //pa.E_Date = Convert.ToDateTime(reader[2]);
                //pa.Area = reader[3].ToString();
                //pa.Remarks = reader[4].ToString();
                //pa.Gender = reader[5].ToString();
                //pa.Status = reader[6].ToString();
                Pats.Add(pa);
            }
            connection.Close();


            AllMedicins rptH = new AllMedicins();


            rptH.Load();
            rptH.SetDataSource(Pats);

            Stream s = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);

            return File(s, "application/pdf");



            //PatDoc rpt = new PatDoc();
            //rpt.Load();
            //rpt.SetDataSource(Pats);
            //Stream s = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            //return File(s, "application/pdf");
        }
        // Prescription in Each Year

        public ActionResult PIY()
        {
            //NorthwindEntities db = new NorthwindEntities();



            string query = string.Format("Select YEAR(Prescription.St_Date),COUNT(*) from Prescription Group by YEAR(Prescription.St_Date)");
            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();
            //SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            //DataSet ds = new DataSet("CRDataSet");
            //ds.Tables.Add(datatable);
            SqlDataReader reader = cmd.ExecuteReader();
            List<Patient_Year> Pats = new List<Patient_Year>();
            while (reader.Read())
            {
                Patient_Year pa = new Patient_Year();
                pa.Year = Convert.ToInt32(reader[0]);
                pa.Count = Convert.ToInt32(reader[1]);
                //pa.E_Date = Convert.ToDateTime(reader[2]);
                //pa.Area = reader[3].ToString();
                //pa.Remarks = reader[4].ToString();
                //pa.Gender = reader[5].ToString();
                //pa.Status = reader[6].ToString();
                Pats.Add(pa);
            }
            connection.Close();


            PrescriptionYear rptH = new PrescriptionYear();

            
            
            rptH.Load();
            rptH.SetDataSource(Pats);

            Stream s = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);

            return File(s, "application/pdf");



            //PatDoc rpt = new PatDoc();
            //rpt.Load();
            //rpt.SetDataSource(Pats);
            //Stream s = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            //return File(s, "application/pdf");
        }

        //Doctor Prescription Medicines

        public ActionResult DPM()
        {
            //NorthwindEntities db = new NorthwindEntities();



            string query = string.Format("Select AspNetUsers.Name,Prescription.Pres_Id,Medicin.Name from AspNetUsers inner join (Prescription inner join (Medication inner join Medicin on Medicin.Med_Id=Medication.Med_Id)  on Medication.Pres_Id = Prescription.Pres_Id) on AspNetUsers.Id = Prescription.Doctor_Id");
            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();
            //SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            //DataSet ds = new DataSet("CRDataSet");
            //ds.Tables.Add(datatable);
            SqlDataReader reader = cmd.ExecuteReader();
            List<Doctor_Prescription_Medicins> Pats = new List<Doctor_Prescription_Medicins>();
            while (reader.Read())
            {
                Doctor_Prescription_Medicins pa = new Doctor_Prescription_Medicins();
                pa.Doctor =reader[0].ToString();
                pa.Pres_Id = Convert.ToInt32(reader[1]);
                pa.Medicin = reader[2].ToString();
                //pa.E_Date = Convert.ToDateTime(reader[2]);
                //pa.Area = reader[3].ToString();
                //pa.Remarks = reader[4].ToString();
                //pa.Gender = reader[5].ToString();
                //pa.Status = reader[6].ToString();
                Pats.Add(pa);
            }
            connection.Close();


            DoctorPrescriptionMedicins rptH = new DoctorPrescriptionMedicins();



            rptH.Load();
            rptH.SetDataSource(Pats);

            Stream s = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);

            return File(s, "application/pdf");



            //PatDoc rpt = new PatDoc();
            //rpt.Load();
            //rpt.SetDataSource(Pats);
            //Stream s = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            //return File(s, "application/pdf");
        }

    }
}