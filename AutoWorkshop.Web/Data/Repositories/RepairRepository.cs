using AutoWorkshop.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoWorkshop.Web.Data.Repositories
{
    public class RepairRepository : GenericRepository<Repair>, IRepairRepository
    {
        private readonly DataContext _context;

        public RepairRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Repair> GetByIdWithAppointment(int id)    //preciso deste método para ir buscar o objecto brand através do id(porque ele não leva nenhuma brand no controlador)
        {
            var repair = await _context.Repairs.FindAsync(id);
            if (repair == null)
            {
                return repair;
            }

            repair.Appointment = await _context.Appointments.FindAsync(repair.AppointmentId);

            return repair;
        }
    }
}
