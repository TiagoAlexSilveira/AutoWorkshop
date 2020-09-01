using AutoWorkshop.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoWorkshop.Web.Data.Repositories
{
    public class MecanicRepository : GenericRepository<Mecanic>, IMecanicRepository
    {
        private readonly DataContext _context;



        public MecanicRepository(DataContext context) : base(context)
        {
            _context = context;
        }



        public Mecanic GetMecanicByUserId(string id)
        {
            var mecanic = _context.Mecanics.FirstOrDefault(u => u.User.Id == id);

            return mecanic;
        }

        public IEnumerable<SelectListItem> GetComboMecanics()
        {
            var list = _context.Mecanics.Select(b => new SelectListItem
            {
                Text = b.FirstName,
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
