using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TrackingWorldProject.Models;

namespace TrackingWorldProject
{
    public class FleetContext : DbContext
    {
        public FleetContext():base("DefaultConnection")
        {
            //Database.SetInitializer<FleetContext>(new CreateDatabaseIfNotExists<FleetContext>());

        }


    public DbSet<Vehicle> Vehicles { get; set; }
    }
}