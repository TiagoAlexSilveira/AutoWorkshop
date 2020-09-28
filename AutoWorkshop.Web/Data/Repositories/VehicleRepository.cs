using AutoWorkshop.Web.Data.Entities;
using AutoWorkshop.Web.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AutoWorkshop.Web.Data.Repositories
{
    public class VehicleRepository : GenericRepository<Vehicle>, IVehicleRepository
    {
        private readonly DataContext _context;

        public VehicleRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        //public async AddClientToVehicleById(int id)
        //{
        //    var vehicle = await _context.Vehicles.FirstOrDefaultAsync(v => v.Id == id);
        //    var client = await _context.Vehicles

        //    return vehicle;
        //}

        //public async Task AddBrandToVehicle(VehicleViewModel model)
        //{
        //    var brand = await _context.Brands.FindAsync(model.BrandId);
        //    if (brand == null)
        //    {
        //        return;
        //    }

        //    var vehic = new Vehicle
        //    {
        //        Brand = brand,
        //        Transmission = model.Transmission,
        //        Type = model.Type,
        //        Color = model.Color,
        //        EnginePower = model.EnginePower,
        //        LastMaintenance = model.LastMaintenance,
        //        Mileage = model.Mileage,
        //        Model = model.Model,
        //        LicensePlate = model.LicensePlate,
        //    };

        //    _context.Vehicles.Add(vehic);

        //    await _context.SaveChangesAsync();
        //}
    }
}
