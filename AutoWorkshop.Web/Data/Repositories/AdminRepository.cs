using AutoWorkshop.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoWorkshop.Web.Data.Repositories
{
    public class AdminRepository : GenericRepository<Admin>, IAdminRepository
    {
        private readonly DataContext _context;

        public AdminRepository(DataContext context) : base(context)
        {
            _context = context;           
        }


        public Admin GetAdminByUserId(string id)
        {
            var admin = _context.Admins.FirstOrDefault(u => u.User.Id == id);

            return admin;
        }
    }
}
