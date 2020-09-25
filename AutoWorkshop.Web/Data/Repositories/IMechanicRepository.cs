using AutoWorkshop.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoWorkshop.Web.Data.Repositories
{
    public interface IMechanicRepository : IGenericRepository<Mechanic>
    {
        IEnumerable<SelectListItem> GetComboMecanics();
       
        Mechanic GetMecanicByUserId(string id);
        Task<Mechanic> GetMechanicWithSpecialtyById(int id);
    }
}
