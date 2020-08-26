using AutoWorkshop.Web.Data.Entities;
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
    }
}
