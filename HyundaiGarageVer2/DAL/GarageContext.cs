using HyundaiGarageVer2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace HyundaiGarageVer2.DAL
{
    public class GarageContext : DbContext
    {
        public GarageContext() : base("GarageContext")
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Car> Cars { get; set; }

        public DbSet<Treatment> Treatments { get; set; }

        public DbSet<Part> Parts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();




        }
    }
}