using AutoWorkshop.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoWorkshop.Web.Data.Repositories
{
    public class BrandRepository : GenericRepository<Brand>, IBrandRepository
    {
        private readonly DataContext _context;

        public BrandRepository(DataContext context) : base(context)
        {
            _context = context;

        }

        public IEnumerable<SelectListItem> GetComboBrands()
        {
            var list = _context.Brands.Select(b => new SelectListItem
            {
                Text = b.BrandName,
                Value = b.Id.ToString()
            }).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Select a brand...)",
                Value = "0"
            });

            return list;
        }


        public async Task<Vehicle> GetByIdWithBrand(int id)    //preciso deste método para ir buscar o objecto brand através do id(porque ele não leva nenhuma brand no controlador)
        {   
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return vehicle;
            }

            vehicle.Brand = await _context.Brands.FindAsync(vehicle.BrandId);

            return vehicle;
        }
    }
}
