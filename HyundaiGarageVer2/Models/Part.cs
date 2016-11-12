using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HyundaiGarageVer2.Models
{
    public class Part
    {
        public int PartID { get; set; }

        

        [StringLength(20, ErrorMessage = "Part name cannot be longer than 40 characters.")]
        public string PartName { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]

        public DateTime ManuDate { get; set; }

        [DataType(DataType.Currency)]
        public int PartPrice { get; set; }


        public int? TreatmentID { get; set; }
        public virtual Treatment Treatment { get; set; }
    }
}