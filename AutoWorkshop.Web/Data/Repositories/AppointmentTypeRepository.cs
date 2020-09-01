using AutoWorkshop.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoWorkshop.Web.Data.Repositories
{
    public class AppointmentTypeRepository : GenericRepository<AppointmentType>, IAppointmentTypeRepository
    {
        private readonly DataContext _context;

        public AppointmentTypeRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
