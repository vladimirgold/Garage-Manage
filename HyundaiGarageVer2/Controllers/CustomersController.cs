using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HyundaiGarageVer2.DAL;
using HyundaiGarageVer2.Models;
using HyundaiGarageVer2.ViewModels;
using DotNet.Highcharts;
using DotNet.Highcharts.Options;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Enums;

namespace HyundaiGarageVer2.Controllers
{
    public class CustomersController : Controller
    {
        private GarageContext db = new GarageContext();

        // GET: Customers
        public ActionResult Index(string searchString)
        {
            var customers = from s in db.Customers
                            select s;


            // SearchString
            if (!String.IsNullOrEmpty(searchString))
            {
                customers = customers.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstName.Contains(searchString));
            }



            return View(customers.ToList());
        }


        public ActionResult CustomerStatGraph()
        {



            // var paidmembers = (from c in db.Customers
            //                    where c.FirstName == "Diana"
            //                    join treatment in db.Treatments on c.ID equals treatment.CustomerID
            //                    select c).Count();

            IQueryable<CustomTreatCount> data = from customer in db.Customers

                                                join treatment in db.Treatments on customer.CustomerID equals treatment.Car.CustomerID

                                                group customer by customer.FirstName into custGroup


                                                select new CustomTreatCount()
                                                {
                                                    CustomerFirstName = custGroup.Key,
                                                    TreatCount = custGroup.Count(),

                                                };
            var list = new List<CustomTreatCount>(data);

            var yDataCounts = list.Select(i => new object[] { i.TreatCount }).ToArray();

            var xDataNames = data.Select(i => i.CustomerFirstName).ToArray();




            Highcharts chart = new Highcharts("chart")
           .SetCredits(new Credits { Enabled = false })
           .InitChart(new Chart { DefaultSeriesType = ChartTypes.Column })
           .SetTitle(new Title { Text = "Hyundai Garage Overview" })
           .SetXAxis(new XAxis { Categories = xDataNames })
           .SetYAxis(new YAxis
           {
               Min = 0,
               Title = new YAxisTitle { Text = "Quantity of Treatments" }
           })
           .SetTooltip(new Tooltip { Formatter = "function() { return ''+ this.series.name +': '+ this.y +''; }" })
           .SetPlotOptions(new PlotOptions { Bar = new PlotOptionsBar { Stacking = Stackings.Normal } })
           .SetSeries(new[]
                      {
                      new Series {
            Name = "Overall Quantity",
            Data = new Data(yDataCounts)
}
                       });
            return View(chart);
        }




        public ActionResult CustomerStatistics()
        {
            IQueryable<CustomTreatCount> data = from customer in db.Customers

                                                join treatment in db.Treatments on customer.CustomerID equals treatment.Car.CustomerID

                                                group customer by customer.FirstName into custGroup


                                                select new CustomTreatCount()
                                                {
                                                    CustomerFirstName = custGroup.Key,
                                                    TreatCount = custGroup.Count()
                                                };
            return View(data.ToList());
        }



        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,FirstName,LastName,Phone")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,FirstName,LastName,Phone")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
