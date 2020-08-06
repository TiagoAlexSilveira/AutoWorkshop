using AutoWorkshop.Web.Data.Entities;
using AutoWorkshop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoWorkshop.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
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
    }
}
