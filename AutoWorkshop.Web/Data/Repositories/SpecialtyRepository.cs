using AutoWorkshop.Web.Data.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoWorkshop.Web.Data.Entities
{
    public class SpecialtyRepository : GenericRepository<Specialty>, ISpecialtyRepository
    {
        private readonly DataContext _context;

        public SpecialtyRepository(DataContext context) : base(context)
        {
            _context = context;
        }


        public IEnumerable<SelectListItem> GetComboSpecialty()
        {
            var list = _context.Specialties.Select(b => new SelectListItem
            {
                Text = b.Type,
                Value = b.Id.ToString()
            }).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Select a Specialty)",
                Value = "0"
            });

            return list;
        }
    }
}
