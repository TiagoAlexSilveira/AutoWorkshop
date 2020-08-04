using AutoWorkshop.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoWorkshop.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private Random _random;

        public SeedDb(DataContext context)
        {
            _context = context;
            _random = new Random();
        }


        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();         

            if (!_context.Brands.Any())
            {
                this.AddBrand("Peugeot");
                this.AddBrand("Toryota");
                this.AddBrand("Ferrari");

                await _context.SaveChangesAsync();
            }

            var brandd = _context.Brands.FirstOrDefault(e => e.Id == 1);
            
            if (!_context.Vehicles.Any())
            {
                this.AddVehicle(brandd, "Preto");
                this.AddVehicle(brandd, "Azul");
                this.AddVehicle(brandd, "Cinzento");
                await _context.SaveChangesAsync();
            }

        }

        private void AddBrand(string brandname)
        {
            _context.Brands.Add(new Brand
            {
                BrandName = brandname
            });
        }

        private void AddVehicle(Brand brand, string color)
        {
            _context.Vehicles.Add(new Vehicle
            {
                Brand = brand,
                Color = color,
                Mileage = _random.Next(150000),
                LicensePlate = "XX-YY-ZZ",
                Transmission = "Automatic",
                EnginePower = _random.Next(200),  //horsepower
                //LastMaintenance = Convert.ToDateTime("23/04/2018"),
                Type = "Type Teste",
                Model = "teste"
            }) ;
        }
    }
}
