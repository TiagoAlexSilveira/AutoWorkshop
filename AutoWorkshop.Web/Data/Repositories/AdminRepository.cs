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

        public List<int> GetAllCount()
        {
            List<int> allCount = new List<int>();

            allCount.Add(_context.Clients.Count());
            allCount.Add(_context.Secretaries.Count());
            allCount.Add(_context.Mechanics.Count());
            allCount.Add(_context.Admins.Count());
            allCount.Add(_context.Vehicles.Count());
            allCount.Add(_context.Brands.Count());
            allCount.Add(_context.Appointments.Count());
            allCount.Add(_context.AppointmentTypes.Count());
            allCount.Add(_context.Repairs.Count());
            allCount.Add(_context.Specialties.Count());           

            return allCount;
        }

    }
}
