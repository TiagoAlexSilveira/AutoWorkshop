using AutoWorkshop.Web.Data.Entities;
using AutoWorkshop.Web.Data.Repositories;
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

            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("Client");
            await _userHelper.CheckRoleAsync("Secretary");
            await _userHelper.CheckRoleAsync("Mecanic");

            var user = await _userHelper.GetUserByEmailAsync("tsilveira01@gmail.com");
            if (user == null)
            {
                user = new User
                {
                    Email = "tsilveira01@gmail.com",
                    UserName = "tsilveira01@gmail.com"
                    //IsActive = true
                };

                var admin = new Admin
                {
                    FirstName = "Tiago",
                    LastName = "Silveira",
                    StreetAddress = "Praceta Adelaide Cabete Nº2 3ºesquerdo",
                    PhoneNumber = "123456789",
                    DateofBirth = Convert.ToDateTime("29/04/1993"),
                    PostalCode = "2675-537",
                    TaxIdentificationNumber = "989898989",
                    CitizenCardNumber = "11223344",
                    //Email = user.Email,
                    User = user
                };

                var result = await _userHelper.AddUserAsync(user, "123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }

                _context.Admins.Add(admin);
            }



            var isInRole = await _userHelper.IsUserInRoleAsync(user, "Admin");
            if (!isInRole)
            {
                await _userHelper.AddUserToRoleAsync(user, "Admin");
            }


            var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
            await _userHelper.ConfirmEmailAsync(user, token);

           



            var userclient = await _userHelper.GetUserByEmailAsync("tsteste@yopmail.com");
            if (userclient == null)
            {
                userclient = new User
                {
                    Email = "tsteste@yopmail.com",
                    UserName = "tsteste@yopmail.com",
                    //IsActive = true
                };

                var client = new Client
                {
                    FirstName = "Mário",
                    LastName = "Silveira",
                    StreetAddress = "Praceta Adelaide Cabete Nº2 3ºesquerdo",
                    PhoneNumber = "123456789",
                    DateofBirth = Convert.ToDateTime("29/04/1993"),
                    PostalCode = "2675-537",
                    TaxIdentificationNumber = "989898989",
                    CitizenCardNumber = "11223344",
                    //Email = userclient.Email,                    
                    User = userclient
                };


                var result = await _userHelper.AddUserAsync(userclient, "123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }

                _context.Clients.Add(client);
            }
         

            var isInRole2 = await _userHelper.IsUserInRoleAsync(userclient, "Client");
            if (!isInRole2)
            {
                await _userHelper.AddUserToRoleAsync(userclient, "Client");                
            }


            var token2 = await _userHelper.GenerateEmailConfirmationTokenAsync(userclient);
            await _userHelper.ConfirmEmailAsync(userclient, token2);




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
