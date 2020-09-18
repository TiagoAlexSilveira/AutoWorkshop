using AutoWorkshop.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoWorkshop.Web.Data.Repositories
{
    public interface IRepairRepository : IGenericRepository<Repair>
    {
        Task<Repair> GetByIdWithAppointment(int id);
    }
}
