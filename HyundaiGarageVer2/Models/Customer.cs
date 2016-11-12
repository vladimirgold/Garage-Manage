using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HyundaiGarageVer2.Models
{
    public class Customer
    {
        
        public int CustomerID { get; set; }

        [StringLength(20, ErrorMessage = "Last name cannot be longer than 20 characters.")]
        public string FirstName { get; set; }

        [StringLength(20, ErrorMessage = "First name cannot be longer than 20 characters.")]
        public string LastName { get; set; }

        public string Phone { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public virtual ICollection<Car> Cars { get; set; }
    }
}