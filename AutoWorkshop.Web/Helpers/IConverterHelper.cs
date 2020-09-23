﻿using AutoWorkshop.Web.Data.Entities;
using AutoWorkshop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoWorkshop.Web.Helpers
{
    public interface IConverterHelper  
    {
        ChangeUserViewModel ToChangeUserViewModelAdmin(Admin admin);

        ChangeUserViewModel ToChangeUserViewModelClient(Client client);

        ChangeUserViewModel ToChangeUserViewModelSecretary(Secretary secretary);

        ChangeUserViewModel ToChangeUserViewModelMecanic(Mechanic mecanic);

        Vehicle ToVehicle(VehicleViewModel model, string path, bool isNew);

        VehicleViewModel ToVehicleViewModel(Vehicle model);


        Admin ToAdmin(ChangeUserViewModel model, string path);

        Client ToClient(ChangeUserViewModel model, string path);

        Secretary ToSecretary(ChangeUserViewModel model, string path);

        Mechanic ToMecanic(ChangeUserViewModel model, string path);

        Appointment ToAppointment(AppointmentViewModel model);

        Repair ToRepair(RepairViewModel model);

        RepairViewModel ToRepairViewModel(Repair repair);


        Admin ToAdminCreate(CreateAccountViewModel model);
        Client ToClientCreate(CreateAccountViewModel model);
        Mechanic ToMechanicCreate(CreateAccountViewModel model);
        Secretary ToSecretaryCreate(CreateAccountViewModel model);
    }
}
