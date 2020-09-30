using AutoWorkshop.Web.Data.Entities;
using AutoWorkshop.Web.Data.Repositories;
using AutoWorkshop.Web.Helpers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Globalization;
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

            #region Admin
            //Admin
            var user = await _userHelper.GetUserByEmailAsync("tsilveira01@gmail.com");
            if (user == null)
            {
                user = new User
                {
                    Email = "tsilveira01@gmail.com",
                    UserName = "tsilveira01@gmail.com"
                };

                var admin = new Admin
                {
                    FirstName = "Tiago",
                    LastName = "Silveira",
                    StreetAddress = "Praceta Adelaide Cabete Nº2 3ºesquerdo",
                    PhoneNumber = "917364887",
                    //DateofBirth = Convert.ToDateTime("05/27/1993"),                    
                    DateofBirth = DateTime.ParseExact("04/29/1993", "MM/dd/yyyy", CultureInfo.InvariantCulture),
                    PostalCode = "2675-537",
                    TaxIdentificationNumber = "111111114",
                    CitizenCardNumber = "11112233",
                    UserId = user.Id,
                    ImageUrl = $"~/images/Placeholder/placeholderUser.png"
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

            #endregion

            #region Client

            // Client
            var userclient = await _userHelper.GetUserByEmailAsync("tsteste@yopmail.com");
            if (userclient == null)
            {
                userclient = new User
                {
                    Email = "tsteste@yopmail.com",
                    UserName = "tsteste@yopmail.com",
                };

                var client = new Client
                {
                    FirstName = "Mário",
                    LastName = "Silveira",
                    StreetAddress = "Praceta Adelaide Cabete Nº2 3ºesquerdo",
                    PhoneNumber = "936837465",
                    //DateofBirth = Convert.ToDateTime("04/29/1993"),
                    DateofBirth = DateTime.ParseExact("04/29/1993", "MM/dd/yyyy", CultureInfo.InvariantCulture),
                    PostalCode = "2687-324",
                    TaxIdentificationNumber = "111111113",
                    CitizenCardNumber = "11223344",
                    ImageUrl = $"~/images/Placeholder/placeholderUser.png",
                    UserId = userclient.Id
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

            #endregion

            #region Client2

            // Client2
            var userclient2 = await _userHelper.GetUserByEmailAsync("tsteste2@yopmail.com");
            if (userclient2 == null)
            {
                userclient2 = new User
                {
                    Email = "tsteste2@yopmail.com",
                    UserName = "tsteste2@yopmail.com",
                };

                var client2 = new Client
                {
                    FirstName = "Pedro",
                    LastName = "Silva",
                    StreetAddress = "Rua das Flores Nº15",
                    PhoneNumber = "968273650",
                    //DateofBirth = Convert.ToDateTime("04/29/1993"),
                    DateofBirth = DateTime.ParseExact("04/29/1993", "MM/dd/yyyy", CultureInfo.InvariantCulture),
                    PostalCode = "1235-786",
                    TaxIdentificationNumber = "947346273",
                    CitizenCardNumber = "62539574",
                    ImageUrl = $"~/images/Placeholder/placeholderUser.png",
                    UserId = userclient2.Id
                };


                var result = await _userHelper.AddUserAsync(userclient2, "123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }

                _context.Clients.Add(client2);
            }


            var isInRole5 = await _userHelper.IsUserInRoleAsync(userclient2, "Client");
            if (!isInRole5)
            {
                await _userHelper.AddUserToRoleAsync(userclient2, "Client");
            }

            var token5 = await _userHelper.GenerateEmailConfirmationTokenAsync(userclient2);
            await _userHelper.ConfirmEmailAsync(userclient2, token5);

            #endregion

            #region Secretary
            //Secretary
            var usersecretary = await _userHelper.GetUserByEmailAsync("tssecret@yopmail.com");
            if (usersecretary == null)
            {
                usersecretary = new User
                {
                    Email = "tssecret@yopmail.com",
                    UserName = "tssecret@yopmail.com",
                };

                var secretary = new Secretary
                {
                    FirstName = "Maria",
                    LastName = "Silva",
                    StreetAddress = "Rua das Santas 10 2º Dir.",
                    PhoneNumber = "916254453",
                    //DateofBirth = Convert.ToDateTime("05/13/1990"),
                    DateofBirth = DateTime.ParseExact("04/29/1993", "MM/dd/yyyy", CultureInfo.InvariantCulture),
                    PostalCode = "2773-677",
                    TaxIdentificationNumber = "111111112",
                    CitizenCardNumber = "33445566",
                    ImageUrl = $"~/images/Placeholder/placeholderUser.png",
                    UserId = usersecretary.Id
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

            #endregion


            if (!_context.Specialties.Any())
            {
                this.AddSpecialty("Maintenance Technician");
                this.AddSpecialty("Painter");
                this.AddSpecialty("Part Technician");

                await _context.SaveChangesAsync();
            }

            #region Mechanic
            // Mechanic
            var usermecha = await _userHelper.GetUserByEmailAsync("tsmecha@yopmail.com");
            if (usermecha == null)
            {
                usermecha = new User
                {
                    Email = "tsmecha@yopmail.com",
                    UserName = "tsmecha@yopmail.com",
                };


                var mechanic = new Mechanic
                {
                    FirstName = "Luis",
                    LastName = "Mechanic",
                    StreetAddress = "Rua dos Mechanics",
                    PhoneNumber = "915242442",
                    //DateofBirth = Convert.ToDateTime("06/20/1976"),
                    DateofBirth = DateTime.ParseExact("04/29/1993", "MM/dd/yyyy", CultureInfo.InvariantCulture),
                    PostalCode = "2655-555",
                    TaxIdentificationNumber = "111111111",
                    CitizenCardNumber = "22334455",
                    Specialty = _context.Specialties.FirstOrDefault(p => p.Id == 3),
                    ImageUrl = $"~/images/Placeholder/placeholderUser.png",
                    UserId = usermecha.Id
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


            #endregion


            if (!_context.Brands.Any())
            {
                this.AddBrand("Peugeot");
                this.AddBrand("Toyota");
                this.AddBrand("Ferrari");

                await _context.SaveChangesAsync();
            }


            if (!_context.Vehicles.Any())
            {
                this.AddVehicle(1, "Black", 1, "AZ-6D-89", "Manual", "Gasoline", "Series X");
                this.AddVehicle(2, "Blue", 1, "B0-HH-D3", "Automatic", "Gas", "Avalon");
                this.AddVehicle(3, "Grey", 1, "GT-J6-02", "Automatic", "Diesel", "F8 Tributo");
                this.AddVehicle(2, "Red", 2, "BN-KK-66", "Manual", "Gasoline", "Corolla");
                this.AddVehicle(2, "White", 2, "LL-RT-54", "Automatic", "Gas", "Yaris");
                this.AddVehicle(1, "Gray", 2, "YU-K8-73", "Manual", "Diesel", "Boxer");
                await _context.SaveChangesAsync();
            }

            if (!_context.AppointmentTypes.Any())
            {
                AddAppointmentType("Maintenance");
                AddAppointmentType("Paint Job");
                AddAppointmentType("Other");
                await _context.SaveChangesAsync();
            }

            if (!_context.Appointments.Any())
            {
                AddAppointment(1, "Fix it", 1, 1, 2, DateTime.ParseExact("10/02/2020 10:00", "MM/dd/yyyy hh:mm", CultureInfo.InvariantCulture), DateTime.ParseExact("10/02/2020 10:30", "MM/dd/yyyy hh:mm", CultureInfo.InvariantCulture), true);
                AddAppointment(2, "Fix it 2", null, 2, 6, DateTime.ParseExact("10/05/2020 11:00", "MM/dd/yyyy hh:mm", CultureInfo.InvariantCulture), DateTime.ParseExact("09/29/2020 11:30", "MM/dd/yyyy hh:mm", CultureInfo.InvariantCulture), false);
                AddAppointment(1, "Fix it 3", null, 1, 2, DateTime.ParseExact("10/03/2020 08:00", "MM/dd/yyyy hh:mm", CultureInfo.InvariantCulture), DateTime.ParseExact("10/03/2020 08:30", "MM/dd/yyyy hh:mm", CultureInfo.InvariantCulture), false);
                AddAppointment(2, "Fix it 4", 1, 2, 4, DateTime.ParseExact("10/07/2020 09:30", "MM/dd/yyyy hh:mm", CultureInfo.InvariantCulture), DateTime.ParseExact("10/07/2020 10:00", "MM/dd/yyyy hh:mm", CultureInfo.InvariantCulture), true);
                await _context.SaveChangesAsync();
            }

            if (!_context.Repairs.Any())
            {
                AddRepair("Repair Fix it", DateTime.ParseExact("10/05/2020 03:00", "MM/dd/yyyy hh:mm", CultureInfo.InvariantCulture), 1);
                AddRepair("Repair Fix it 2", DateTime.ParseExact("10/08/2020 10:00", "MM/dd/yyyy hh:mm", CultureInfo.InvariantCulture), 4);
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

        private void AddVehicle(int brandId, string color, int clientId, string license, string transmission, string type, string model)
        {
            _context.Vehicles.Add(new Vehicle
            {
                BrandId = brandId,
                Color = color,
                Mileage = _random.Next(150000),
                LicensePlate = license,
                Transmission = transmission,
                EnginePower = _random.Next(200),  //horsepower
                LastMaintenance = DateTime.ParseExact("04/23/2019", "MM/dd/yyyy", CultureInfo.InvariantCulture),
                Type = type,
                Model = model,
                ClientId = clientId
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

        private void AddAppointment(int appointmentTypeId, string Info, int? mechanicId, int clientId, int vehicleId,DateTime startTime, DateTime endTime ,bool isConfirmed)
        {
            _context.Appointments.Add(new Appointment
            {
                AppointmentTypeId = appointmentTypeId,
                Information = Info,
                MechanicId = mechanicId,
                ClientId = clientId,
                VehicleId = vehicleId,
                StartTime = startTime,
                EndTime = endTime,
                IsConfirmed = isConfirmed
            });
        }


        private void AddRepair(string repairInfo, DateTime completedAt, int appointmentId)
        {
            _context.Repairs.Add(new Repair
            {
                RepairInfo = repairInfo,
                CompletedAt = completedAt,
                AppointmentId = appointmentId
            });
        }
      
    }
}
