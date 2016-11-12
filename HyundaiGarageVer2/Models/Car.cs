using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HyundaiGarageVer2.Models
{

    public enum Model
    {
        i10, i20, i25, i30, i35, i40
    }
    public class Car
    {

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("LicensePlate")]
        [Display(Name = "License Plate")]
        public int CarID { get; set; }
              
        public Model Model { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime ManufYear { get; set; }


        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }

        public virtual ICollection<Treatment> Treatments { get; set; }

    }
}