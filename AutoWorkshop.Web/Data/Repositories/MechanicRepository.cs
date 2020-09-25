using AutoWorkshop.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoWorkshop.Web.Data.Repositories
{
    public class MechanicRepository : GenericRepository<Mechanic>, IMechanicRepository
    {
        private readonly DataContext _context;



        public MechanicRepository(DataContext context) : base(context)
        {
            _context = context;
        }



        public Mechanic GetMecanicByUserId(string id)
        {
            var mecanic = _context.Mechanics.FirstOrDefault(u => u.UserId == id);

            return mecanic;
        }


        public async Task<Mechanic> GetMechanicWithSpecialtyById(int id)
        {
            var mechanic = await _context.Mechanics.FindAsync(id);
            if (mechanic == null)
            {
                return mechanic;
            }

            mechanic.Specialty = await _context.Specialties.FindAsync(mechanic.SpecialtyId);

            return mechanic;
        }

        public IEnumerable<SelectListItem> GetComboMecanics()
        {
            var list = _context.Mechanics.Select(b => new SelectListItem
            {
                Text = b.FullName + " " + "-" + " " + b.Specialty.Type,
                Value = b.Id.ToString()
            }).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Select a Mechanic...)",
                Value = "0"
            });

            return list;
        }
    }
}
