namespace VehicleInfoSystem.Client
{
    using Data;
    using Models.Vehicles.NonMotors;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Startup
    {
        public static void Main(string[] args)
        {
            VehicleSystemContext db = new VehicleSystemContext();
            Bike bicycle = new Bike()
            {
                Manufacturer = "Queen",
                Model = "Bicycle Album",
                Price = 1000m,
                MaxSpeed = 9999,
                ShiftsCount = 7,
                Color = "Red"
            };

            db.Vehicles.Add(bicycle);
            db.SaveChanges();
            db.Database.Initialize(true);
        }
    }
}
