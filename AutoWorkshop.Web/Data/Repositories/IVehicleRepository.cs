using AutoWorkshop.Web.Data.Entities;
using AutoWorkshop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoWorkshop.Web.Data
{
    public interface IVehicleRepository : IGenericRepository<Vehicle>
    {

        Task AddBrandToVehicle(VehicleViewModel model);


        //Task VehicleWithBrandEdit(VehicleViewModel model);
  
    }
}
