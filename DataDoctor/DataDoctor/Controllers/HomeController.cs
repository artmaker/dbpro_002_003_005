using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataHandler;

namespace DataDoctor.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            List<Prescription> PrescriptionsList;
            using (DataDoctorEntities2 entity = new DataDoctorEntities2())
            {
                var doctorsCount = entity.Database.SqlQuery<int>("Select Count(*) from dbo.AspNetUsers where Licence is not null").ToList();

                ViewBag.doctors = doctorsCount[0];
                var users = entity.Database.SqlQuery<string>(string.Format("Select Id from dbo.AspNetUsers where UserName='{0}'",User.Identity.Name)).ToList();
                string user = users[0];
                //var ListOfPrescriptions = entity.Database.SqlQuery<int>("Select Count(*) from dbo.AspNetUsers where Licence is not null").ToList();
                //var innerGroupJoinQuery =from pres in Prescription
                //    join users in AspNetUser on pres.Doctor_Id equals users.Id 
                //    select pres;
                //var PrescriptionsList = from pres in Prescription
                //                         where pres.Doctor_ID == "2f9f3535-5d63-460c-a2fd-6bc5e21e9814"
                //                         select pres;
                //var user = entity.AspNetUsers.Where(u => u.UserName == User.Identity.Name);
                PrescriptionsList = entity.Prescriptions.Where(e => e.Doctor_Id == user).ToList();

            }
                //check the changes
                return View(PrescriptionsList);

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}