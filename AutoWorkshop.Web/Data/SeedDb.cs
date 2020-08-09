using AutoWorkshop.Web.Data.Entities;
using AutoWorkshop.Web.Helpers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AutoWorkshop.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly Random _random;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _random = new Random();
        }


        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            var user = await _userHelper.GetUserByEmailAsync("tsilveira01@gmail.com");
            if (user == null)
            {
                user = new User
                {
                    FirstName = "Tiago",
                    LastName = "Silveira",
                    Email = "tsilveira01@gmail.com",
                    UserName = "tsilveira01@gmail.com",
                    PhoneNumber = "123456789"
                };

                var result = await _userHelper.AddUserAsync(user, "123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }
            }

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
                this.AddVehicle(brandd, "Preto", user);
                this.AddVehicle(brandd, "Azul", user);
                this.AddVehicle(brandd, "Cinzento", user);
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

        private void AddVehicle(Brand brand, string color, User user)
        {
            _context.Vehicles.Add(new Vehicle
            {
                Brand = brand,
                Color = color,
                Mileage = _random.Next(150000),
                LicensePlate = "XX-YY-ZZ",
                Transmission = "Automatic",
                EnginePower = _random.Next(200),  //horsepower
                LastMaintenance = Convert.ToDateTime("23/04/2018"),
                Type = "Type Teste",
                Model = "teste",
                User = user
            });
        }
    }
}
