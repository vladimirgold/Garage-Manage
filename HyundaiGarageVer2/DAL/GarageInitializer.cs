using HyundaiGarageVer2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HyundaiGarageVer2.DAL
{
    public class GarageInitializer : DropCreateDatabaseIfModelChanges<GarageContext>
    {

        protected override void Seed(GarageContext context)
        {
            var customers = new List<Customer>
            {
                new Customer{FirstName="Vladimir",LastName="Gold",Phone="0501234577"},
                new Customer{FirstName="Alex",LastName="Bale",Phone="0531244557"},
                new Customer{FirstName="Nino",LastName="Olivetto",Phone="0541704950"},
                new Customer{FirstName="Diana",LastName="Gold",Phone="0541704950"}
            };

            customers.ForEach(s => context.Customers.Add(s));
            context.SaveChanges();


            var cars = new List<Car>
            {
                    new Car{CarID=111,Model=Model.i25,ManufYear=DateTime.Parse("2016"),CustomerID=1},
                    new Car{CarID=222,Model=Model.i35,ManufYear=DateTime.Parse("2014"),CustomerID=2},
                    new Car{CarID=333,Model=Model.i10,ManufYear=DateTime.Parse("2013"),CustomerID=3},
                    new Car{CarID=444,Model=Model.i30,ManufYear=DateTime.Parse("2015"),CustomerID=4}

            };
            cars.ForEach(s => context.Cars.Add(s));
            base.Seed(context);



            var parts = new List<Part>
            {
            new Part{PartName = "LeftWheel", ManuDate =DateTime.Parse("07-01-2016"),PartPrice=100,TreatmentID=null},
            new Part{PartName = "Tube", ManuDate =DateTime.Parse("07-01-2016"),PartPrice=100,TreatmentID=1},
            new Part{PartName = "RightWheel", ManuDate =DateTime.Parse("07-01-2016"),PartPrice=100,TreatmentID=1},
            new Part{PartName = "LeftWheel", ManuDate =DateTime.Parse("07-01-2016"),PartPrice=100,TreatmentID=2},
            new Part{PartName = "LeftRearMirror", ManuDate =DateTime.Parse("07-01-2016"),PartPrice=100,TreatmentID=3}


            };
            parts.ForEach(s => context.Parts.Add(s));
            context.SaveChanges();

           

            var treatments = new List<Treatment>
            {
                new Treatment {WorkHours=5,TreatmentDate=DateTime.Parse("02-02-2016"),CarID=222 },
                new Treatment {WorkHours=2,TreatmentDate=DateTime.Parse("05-07-2016"),CarID=111 },
                new Treatment {WorkHours=7,TreatmentDate=DateTime.Parse("04-03-2016"),CarID=333 }
            };
            treatments.ForEach(s => context.Treatments.Add(s));
            context.SaveChanges();
        }


    }
}