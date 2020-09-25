using AutoWorkshop.Web.Data;
using AutoWorkshop.Web.Data.Entities;
using AutoWorkshop.Web.Models;
using Microsoft.AspNetCore.Identity;
using Syncfusion.EJ2.DropDowns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoWorkshop.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {

        private readonly DataContext _dataContext;
        private readonly UserManager<User> _userManager;

        public ConverterHelper(DataContext dataContext, 
                               UserManager<User> userManager)
        {
            _dataContext = dataContext;
            _userManager = userManager;
        }


        public Vehicle ToVehicle(VehicleViewModel model, string path, bool isNew)
        {
            return new Vehicle
            {
                Id = isNew ? 0 : model.Id,
                Brand = model.Brand,
                Model = model.Model,
                Type = model.Type,
                Transmission = model.Transmission,
                Mileage = model.Mileage,
                Color = model.Color,
                EnginePower = model.EnginePower,
                LastMaintenance = model.LastMaintenance,
                LicensePlate = model.LicensePlate
            };
        }

        public VehicleViewModel ToVehicleViewModel(Vehicle model)
        {
            return new VehicleViewModel
            {
                Id = model.Id,
                Brand = model.Brand,
                Model = model.Model,
                Type = model.Type,
                Transmission = model.Transmission,
                Mileage = model.Mileage,
                Color = model.Color,
                EnginePower = model.EnginePower,
                LastMaintenance = model.LastMaintenance,
                LicensePlate = model.LicensePlate
            };
        }


        public Client ToClientInfo(InfoAfterLoginViewModel model)
        {
            return new Client
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                StreetAddress = model.StreetAddress,
                PhoneNumber = model.PhoneNumber,
                PostalCode = model.PostalCode,
                DateofBirth = model.DateofBirth,
                TaxIdentificationNumber = model.TaxIdentificationNumber,
                CitizenCardNumber = model.CitizenCardNumber,
                ImageUrl = null
            };
        }

        public InfoAfterLoginViewModel ToInfoAfterLoginViewModel(Client client)
        {
            return new InfoAfterLoginViewModel
            {
                Id = client.Id,
                FirstName = client.FirstName,
                LastName = client.LastName,
                StreetAddress = client.StreetAddress,
                PhoneNumber = client.PhoneNumber,
                PostalCode = client.PostalCode,
                DateofBirth = client.DateofBirth,
                TaxIdentificationNumber = client.TaxIdentificationNumber,
                CitizenCardNumber = client.CitizenCardNumber,
                ImageUrl = null
            };
        }



        public Repair ToRepair(RepairViewModel model)
        {
            return new Repair
            {
                Id = model.Id,
                RepairInfo = model.RepairInfo,
                CompletedAt = model.CompletedAt,
                AppointmentId = model.AppointmentId,
                Appointment = model.Appointment
            };
        }

        public RepairViewModel ToRepairViewModel(Repair repair)
        {
            return new RepairViewModel
            {
                Id = repair.Id,
                RepairInfo = repair.RepairInfo,
                CompletedAt = repair.CompletedAt,
                AppointmentId = repair.AppointmentId,
            };
        }



        public ChangeUserViewModel ToChangeUserViewModelAdmin (Admin admin) //recebe todas as entidades
        {
            return new ChangeUserViewModel
            {
                Id = admin.Id,
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                StreetAddress = admin.StreetAddress,
                PhoneNumber = admin.PhoneNumber,
                PostalCode = admin.PostalCode,
                DateofBirth = admin.DateofBirth,
                TaxIdentificationNumber = admin.TaxIdentificationNumber,
                CitizenCardNumber = admin.CitizenCardNumber,
                ImageUrl = admin.ImageUrl
            };
        }

        public ChangeUserViewModel ToChangeUserViewModelClient(Client client) //recebe todas as entidades
        {
            return new ChangeUserViewModel
            {
                Id = client.Id,
                FirstName = client.FirstName,
                LastName = client.LastName,
                StreetAddress = client.StreetAddress,
                PhoneNumber = client.PhoneNumber,
                PostalCode = client.PostalCode,
                DateofBirth = client.DateofBirth,
                TaxIdentificationNumber = client.TaxIdentificationNumber,
                CitizenCardNumber = client.CitizenCardNumber,
                ImageUrl = client.ImageUrl
            };
        }

        public ChangeUserViewModel ToChangeUserViewModelSecretary(Secretary secretary) //recebe todas as entidades
        {
            return new ChangeUserViewModel
            {
                Id = secretary.Id,
                FirstName = secretary.FirstName,
                LastName = secretary.LastName,
                StreetAddress = secretary.StreetAddress,
                PhoneNumber = secretary.PhoneNumber,
                PostalCode = secretary.PostalCode,
                DateofBirth = secretary.DateofBirth,
                TaxIdentificationNumber = secretary.TaxIdentificationNumber,
                CitizenCardNumber = secretary.CitizenCardNumber,
                ImageUrl = secretary.ImageUrl
            };
        }

        public ChangeUserViewModel ToChangeUserViewModelMecanic(Mechanic mecanic) //recebe todas as entidades
        {
            return new ChangeUserViewModel
            {
                Id = mecanic.Id,
                FirstName = mecanic.FirstName,
                LastName = mecanic.LastName,
                StreetAddress = mecanic.StreetAddress,
                PhoneNumber = mecanic.PhoneNumber,
                PostalCode = mecanic.PostalCode,
                DateofBirth = mecanic.DateofBirth,
                TaxIdentificationNumber = mecanic.TaxIdentificationNumber,
                CitizenCardNumber = mecanic.CitizenCardNumber,
                Specialty = mecanic.Specialty,
                ImageUrl = mecanic.ImageUrl
            };
        }



        public Admin ToAdmin(ChangeUserViewModel model, string path)
        {
            return new Admin
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                StreetAddress = model.StreetAddress,
                PhoneNumber = model.PhoneNumber,
                PostalCode = model.PostalCode,
                DateofBirth = model.DateofBirth,
                TaxIdentificationNumber = model.TaxIdentificationNumber,
                CitizenCardNumber = model.CitizenCardNumber, 
                ImageUrl = path
            };
        }

        public Client ToClient(ChangeUserViewModel model, string path)
        {
            return new Client
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                StreetAddress = model.StreetAddress,
                PhoneNumber = model.PhoneNumber,
                PostalCode = model.PostalCode,
                DateofBirth = model.DateofBirth,
                TaxIdentificationNumber = model.TaxIdentificationNumber,
                CitizenCardNumber = model.CitizenCardNumber,
                ImageUrl = path
            };
        }

        public Secretary ToSecretary(ChangeUserViewModel model, string path)
        {
            return new Secretary
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                StreetAddress = model.StreetAddress,
                PhoneNumber = model.PhoneNumber,
                PostalCode = model.PostalCode,
                DateofBirth = model.DateofBirth,
                TaxIdentificationNumber = model.TaxIdentificationNumber,
                CitizenCardNumber = model.CitizenCardNumber,
                ImageUrl = path
            };
        }

        public Mechanic ToMecanic(ChangeUserViewModel model, string path)
        {
            return new Mechanic
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                StreetAddress = model.StreetAddress,
                PhoneNumber = model.PhoneNumber,
                PostalCode = model.PostalCode,
                DateofBirth = model.DateofBirth,
                TaxIdentificationNumber = model.TaxIdentificationNumber,
                CitizenCardNumber = model.CitizenCardNumber,
                Specialty = model.Specialty,
                ImageUrl = path
            };
        }



        public Admin ToAdminCreate(CreateAccountViewModel model)
        {
            return new Admin
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                StreetAddress = model.StreetAddress,
                PhoneNumber = model.PhoneNumber,
                PostalCode = model.PostalCode,
                DateofBirth = model.DateofBirth,
                TaxIdentificationNumber = model.TaxIdentificationNumber,
                CitizenCardNumber = model.CitizenCardNumber,               
            };
        }

        public Client ToClientCreate(CreateAccountViewModel model)
        {
            return new Client
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                StreetAddress = model.StreetAddress,
                PhoneNumber = model.PhoneNumber,
                PostalCode = model.PostalCode,
                DateofBirth = model.DateofBirth,
                TaxIdentificationNumber = model.TaxIdentificationNumber,
                CitizenCardNumber = model.CitizenCardNumber,
            };
        }

        public Mechanic ToMechanicCreate(CreateAccountViewModel model)
        {
            return new Mechanic
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                StreetAddress = model.StreetAddress,
                PhoneNumber = model.PhoneNumber,
                PostalCode = model.PostalCode,
                DateofBirth = model.DateofBirth,
                TaxIdentificationNumber = model.TaxIdentificationNumber,
                CitizenCardNumber = model.CitizenCardNumber,
                Specialty = model.Specialty
            };
        }

        public Secretary ToSecretaryCreate(CreateAccountViewModel model)
        {
            return new Secretary
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                StreetAddress = model.StreetAddress,
                PhoneNumber = model.PhoneNumber,
                PostalCode = model.PostalCode,
                DateofBirth = model.DateofBirth,
                TaxIdentificationNumber = model.TaxIdentificationNumber,
                CitizenCardNumber = model.CitizenCardNumber,
            };
        }



        //public Appointment ToAppointment(AppointmentViewModel model)
        //{
        //    return new Appointment
        //    {
        //        Id = model,
        //        Date = model.Date,
        //        Time = model.Time,
        //        Information = model.Information,
        //        ClientId = model.ClientId,
        //        Client = model.Client,
        //        VehicleId = model.VehicleId,
        //        Vehicle = model.Vehicle,
        //        AppointmentType = model.AppointmentType,
        //        AppointmentTypeId = model.AppointmentTypeId,
        //        IsConfirmed = false,
        //        IsUrgent = model.IsUrgent
        //    };
        //}


        public PersonEditViewModel ToPersonEditViewModel(Client client)
        {
            return new PersonEditViewModel
            {
                Id = client.Id,
                FirstName = client.FirstName,
                LastName = client.LastName,
                StreetAddress = client.StreetAddress,
                PhoneNumber = client.PhoneNumber,
                PostalCode = client.PostalCode,
                DateofBirth = client.DateofBirth,
                TaxIdentificationNumber = client.TaxIdentificationNumber,
                CitizenCardNumber = client.CitizenCardNumber,
                ImageUrl = client.ImageUrl
            };
        }

        public PersonEditViewModel ToPersonEditViewModel(Admin admin)
        {
            return new PersonEditViewModel
            {
                Id = admin.Id,
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                StreetAddress = admin.StreetAddress,
                PhoneNumber = admin.PhoneNumber,
                PostalCode = admin.PostalCode,
                DateofBirth = admin.DateofBirth,
                TaxIdentificationNumber = admin.TaxIdentificationNumber,
                CitizenCardNumber = admin.CitizenCardNumber,
                ImageUrl = admin.ImageUrl
            };
        }

        public PersonEditViewModel ToPersonEditViewModel(Mechanic mechanic)
        {
            return new PersonEditViewModel
            {
                Id = mechanic.Id,
                FirstName = mechanic.FirstName,
                LastName = mechanic.LastName,
                StreetAddress = mechanic.StreetAddress,
                PhoneNumber = mechanic.PhoneNumber,
                PostalCode = mechanic.PostalCode,
                DateofBirth = mechanic.DateofBirth,
                TaxIdentificationNumber = mechanic.TaxIdentificationNumber,
                CitizenCardNumber = mechanic.CitizenCardNumber,
                Specialty = mechanic.Specialty,
                ImageUrl = mechanic.ImageUrl
            };
        }

        public PersonEditViewModel ToPersonEditViewModel(Secretary secretary)
        {
            return new PersonEditViewModel
            {
                Id = secretary.Id,
                FirstName = secretary.FirstName,
                LastName = secretary.LastName,
                StreetAddress = secretary.StreetAddress,
                PhoneNumber = secretary.PhoneNumber,
                PostalCode = secretary.PostalCode,
                DateofBirth = secretary.DateofBirth,
                TaxIdentificationNumber = secretary.TaxIdentificationNumber,
                CitizenCardNumber = secretary.CitizenCardNumber,
                ImageUrl = secretary.ImageUrl
            };
        }


        public Client ToClientEdit(PersonEditViewModel model)
        {
            return new Client
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                StreetAddress = model.StreetAddress,
                PhoneNumber = model.PhoneNumber,
                PostalCode = model.PostalCode,
                DateofBirth = model.DateofBirth,
                TaxIdentificationNumber = model.TaxIdentificationNumber,
                CitizenCardNumber = model.CitizenCardNumber,
                ImageUrl = model.ImageUrl
            };
        }

        public Mechanic ToMechanicEdit(PersonEditViewModel model)
        {
            return new Mechanic
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                StreetAddress = model.StreetAddress,
                PhoneNumber = model.PhoneNumber,
                PostalCode = model.PostalCode,
                DateofBirth = model.DateofBirth,
                TaxIdentificationNumber = model.TaxIdentificationNumber,
                CitizenCardNumber = model.CitizenCardNumber,
                ImageUrl = model.ImageUrl,
                Specialty = model.Specialty
                
            };
        }

        public Secretary ToSecretaryEdit(PersonEditViewModel model)
        {
            return new Secretary
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                StreetAddress = model.StreetAddress,
                PhoneNumber = model.PhoneNumber,
                PostalCode = model.PostalCode,
                DateofBirth = model.DateofBirth,
                TaxIdentificationNumber = model.TaxIdentificationNumber,
                CitizenCardNumber = model.CitizenCardNumber,
                ImageUrl = model.ImageUrl
            };
        }

        public Admin ToAdminEdit(PersonEditViewModel model)
        {
            return new Admin
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                StreetAddress = model.StreetAddress,
                PhoneNumber = model.PhoneNumber,
                PostalCode = model.PostalCode,
                DateofBirth = model.DateofBirth,
                TaxIdentificationNumber = model.TaxIdentificationNumber,
                CitizenCardNumber = model.CitizenCardNumber,
                ImageUrl = model.ImageUrl
            };
        }

    }
}
