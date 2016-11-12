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
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;

namespace HyundaiGarageVer2.Controllers
{
    public class PartsController : Controller
    {
        private GarageContext db = new GarageContext();

        // GET: Parts
        public ActionResult Index()
        {
            var parts = db.Parts.Include(p => p.Treatment);
            return View(parts.ToList());
        }

        public ActionResult PartStatistics()
        {
            IQueryable<PartsCountSimple> data = from parts in db.Parts
                                                group parts by parts.PartName into partGroup
                                                select new PartsCountSimple()
                                                {
                                                    PartName = partGroup.Key,
                                                    PartsCount = partGroup.Count()
                                                };
            return View(data.ToList());
        }

        public ActionResult PartStatisticsGraph()
        {
            IQueryable<PartsCountSimple> data = from parts in db.Parts
                                                group parts by parts.PartName into partGroup
                                                select new PartsCountSimple()
                                                {
                                                    PartName = partGroup.Key,
                                                    PartsCount = partGroup.Count()
                                                };



            var list = new List<PartsCountSimple>(data);

            var yDataCounts = list.Select(i => new object[] { i.PartsCount }).ToArray();

            var xDataNames = data.Select(i => i.PartName).ToArray();




            Highcharts chart = new Highcharts("chart")
           .SetCredits(new Credits { Enabled = false })
           .InitChart(new Chart { DefaultSeriesType = ChartTypes.Column })
           .SetTitle(new Title { Text = "Hyundai Garage Parts Stock" })
           .SetXAxis(new XAxis { Categories = xDataNames })
           .SetYAxis(new YAxis
           {
               Min = 0,
               Title = new YAxisTitle { Text = "Quantity" }
           })
           .SetTooltip(new Tooltip { Formatter = "function() { return ''+ this.series.name +': '+ this.y +''; }" })
           .SetPlotOptions(new PlotOptions { Bar = new PlotOptionsBar { Stacking = Stackings.Normal } })
           .SetSeries(new[]
                      {
                      new Series {
            Name = "Quantity Available",
            Data = new Data(yDataCounts)
}
                       });
            return View(chart);




           
        }



        // GET: Parts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Part part = db.Parts.Find(id);
            if (part == null)
            {
                return HttpNotFound();
            }
            return View(part);
        }

        // GET: Parts/Create
        public ActionResult Create()
        {
            ViewBag.TreatmentID = new SelectList(db.Treatments, "TreatmentID", "TreatmentID");
            return View();
        }

        // POST: Parts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PartID,PartName,ManuDate,PartPrice,TreatmentID")] Part part)
        {
            if (ModelState.IsValid)
            {
                db.Parts.Add(part);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TreatmentID = new SelectList(db.Treatments, "TreatmentID", "TreatmentID", part.TreatmentID);
            return View(part);
        }

        // GET: Parts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Part part = db.Parts.Find(id);
            if (part == null)
            {
                return HttpNotFound();
            }
            ViewBag.TreatmentID = new SelectList(db.Treatments, "TreatmentID", "TreatmentID", part.TreatmentID);
            return View(part);
        }

        // POST: Parts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PartID,PartName,ManuDate,PartPrice,TreatmentID")] Part part)
        {
            if (ModelState.IsValid)
            {
                db.Entry(part).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TreatmentID = new SelectList(db.Treatments, "TreatmentID", "TreatmentID", part.TreatmentID);
            return View(part);
        }

        // GET: Parts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Part part = db.Parts.Find(id);
            if (part == null)
            {
                return HttpNotFound();
            }
            return View(part);
        }

        // POST: Parts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Part part = db.Parts.Find(id);
            db.Parts.Remove(part);
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
