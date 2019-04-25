using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDoctor.Models
{
    class Prescript
    {
        public int Pres_Id { get; set; }
        public int Patient_Id { get; set; }

        public DateTime S_Date { get; set; }
        public DateTime E_Date { get; set; }
        public string Results { get; set; }
        public string Doctor_Id { get; set; }

        public string Area { get; set; }
        public string Reamrks { get; set; }
        public string Gender { get; set; }
        public string Status { get; set; }


    }
}
