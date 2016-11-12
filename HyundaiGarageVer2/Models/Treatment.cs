using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HyundaiGarageVer2.Models
{
    public class Treatment
    {
        public int TreatmentID { get; set; }

        public int WorkHours { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime TreatmentDate { get; set; }


        public int CarID { get; set; }
        public virtual Car Car { get; set; }

        

    }
}