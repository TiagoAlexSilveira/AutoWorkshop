using AutoWorkshop.Web.Data.Entities;
using AutoWorkshop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoWorkshop.Web.Helpers
{
    public interface IConverterHelper  
    {
        Vehicle ToVehicle(VehicleViewModel model, string path, bool isNew);

        VehicleViewModel ToVehicleViewModel(Vehicle model);
    }
}
