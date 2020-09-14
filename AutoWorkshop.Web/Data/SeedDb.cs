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
            await _userHelper.CheckRoleAsync("Mechanic");

            //Admin
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
                    TaxIdentificationNumber = "111111114",
                    CitizenCardNumber = "11112233",
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
           


            // Client
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
                    TaxIdentificationNumber = "111111113",
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



            //Secretary
            var usersecretary = await _userHelper.GetUserByEmailAsync("tssecret@yopmail.com");
            if (usersecretary == null)
            {
                usersecretary = new User
                {
                    Email = "tssecret@yopmail.com",
                    UserName = "tssecret@yopmail.com",
                    //IsActive = true
                };

                var secretary = new Secretary
                {
                    FirstName = "Maria",
                    LastName = "Silva",
                    StreetAddress = "Rua das Flores 2º Dir.",
                    PhoneNumber = "999999999",
                    DateofBirth = Convert.ToDateTime("13/05/1990"),
                    PostalCode = "2773-677",
                    TaxIdentificationNumber = "111111112",
                    CitizenCardNumber = "33445566",
                    //Email = userclient.Email,                    
                    User = usersecretary
                };


                var result = await _userHelper.AddUserAsync(usersecretary, "123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }

                _context.Secretaries.Add(secretary);
            }

            var isInRole3 = await _userHelper.IsUserInRoleAsync(usersecretary, "Secretary");
            if (!isInRole3)
            {
                await _userHelper.AddUserToRoleAsync(usersecretary, "Secretary");
            }

            var token3 = await _userHelper.GenerateEmailConfirmationTokenAsync(usersecretary);
            await _userHelper.ConfirmEmailAsync(usersecretary, token3);



            if (!_context.Specialties.Any())
            {
                this.AddSpecialty("Maintenance Technician");
                this.AddSpecialty("Painter");
                this.AddSpecialty("Part Technician");

                await _context.SaveChangesAsync();
            }


            // Mechanic
            var usermecha = await _userHelper.GetUserByEmailAsync("tsmecha@yopmail.com");
            if (usermecha == null)
            {
                usermecha = new User
                {
                    Email = "tsmecha@yopmail.com",
                    UserName = "tsmecha@yopmail.com",
                    //IsActive = true
                };


                var mechanic = new Mechanic
                {
                    FirstName = "Luis",
                    LastName = "Mechanic",
                    StreetAddress = "Rua dos Mechanics",
                    PhoneNumber = "111111119",
                    DateofBirth = Convert.ToDateTime("29/04/1976"),
                    PostalCode = "2655-555",
                    TaxIdentificationNumber = "111111111",
                    CitizenCardNumber = "22334455",
                    Specialty = _context.Specialties.FirstOrDefault(p => p.Id == 3),
                    //Email = userclient.Email,                    
                    User = usermecha
                };


                var result4 = await _userHelper.AddUserAsync(usermecha, "123456");
                if (result4 != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }

                _context.Mechanics.Add(mechanic);
            }


            var isInRole4 = await _userHelper.IsUserInRoleAsync(usermecha, "Mechanic");
            if (!isInRole4)
            {
                await _userHelper.AddUserToRoleAsync(usermecha, "Mechanic");
            }

            var token4 = await _userHelper.GenerateEmailConfirmationTokenAsync(usermecha);
            await _userHelper.ConfirmEmailAsync(usermecha, token4);

           


            
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
                this.AddVehicle(brandd, "Black", user);
                this.AddVehicle(brandd, "Blue", user);
                this.AddVehicle(brandd, "Grey", user);
                this.AddVehicle(brandd, "Red", userclient);
                this.AddVehicle(brandd, "White", userclient);
                this.AddVehicle(brandd, "Grey", userclient);
                await _context.SaveChangesAsync();
            }

            if (!_context.AppointmentTypes.Any())
            {
                AddAppointmentType("Manutenção");
                AddAppointmentType("Pintura");
                AddAppointmentType("Outro");
                await _context.SaveChangesAsync();
            }

        


            if (!_context.Appointments.Any())
            {
                //Unassigned appointment (needs mechanic and work estimate)
                _context.Appointments.Add(new Appointment
                {
                    AppointmentType = _context.AppointmentTypes.FirstOrDefault(e => e.Id == 1),
                    Date = Convert.ToDateTime("30/10/2020"),
                    Time = Convert.ToDateTime("12:30"),
                    Information = "Part Replacement",
                    Mechanic = null,                    
                    Client = _context.Clients.FirstOrDefault(e => e.Id == 1),
                    Vehicle = _context.Vehicles.FirstOrDefault(e => e.Id == 4),
                    IsConfirmed = false,
                    IsUrgent = false
                });

                //Unconfirmed appointment (needs IsConfirmed active)
                _context.Appointments.Add(new Appointment
                {
                    AppointmentType = _context.AppointmentTypes.FirstOrDefault(e => e.Id == 2),
                    Date = Convert.ToDateTime("25/09/2020"),
                    Time = Convert.ToDateTime("09:00"),
                    Information = "Paint job",
                    WorkEstimate = Convert.ToDateTime("02:00"),
                    Mechanic = _context.Mechanics.FirstOrDefault(e => e.Id == 1),
                    Client = _context.Clients.FirstOrDefault(e => e.Id == 1),
                    Vehicle = _context.Vehicles.FirstOrDefault(e => e.Id == 5),
                    IsConfirmed = false,
                    IsUrgent = false
                });

                //Confirmed appointment (doesn't need anything)
                _context.Appointments.Add(new Appointment
                {
                    AppointmentType = _context.AppointmentTypes.FirstOrDefault(e => e.Id == 3),
                    Date = Convert.ToDateTime("26/09/2020"),
                    Time = Convert.ToDateTime("09:00"),
                    Information = "Oil Change",
                    Mechanic = _context.Mechanics.FirstOrDefault(e => e.Id == 1),
                    WorkEstimate = Convert.ToDateTime("01:00"),
                    Client = _context.Clients.FirstOrDefault(e => e.Id == 1),
                    Vehicle = _context.Vehicles.FirstOrDefault(e => e.Id == 6),
                    IsConfirmed = true,
                    IsUrgent = false
                });

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

        private void AddAppointmentType(string type)
        {
            _context.AppointmentTypes.Add(new AppointmentType
            {
                Type = type
            });
        }

        private void AddSpecialty(string type)
        {
            _context.Specialties.Add(new Specialty
            {
                Type = type
            });
        }


      
    }
}
