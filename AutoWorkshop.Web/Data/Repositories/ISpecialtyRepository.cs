using AutoWorkshop.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoWorkshop.Web.Data.Repositories
{
    public interface ISpecialtyRepository : IGenericRepository<Specialty>
    {

        IEnumerable<SelectListItem> GetComboSpecialty();
    }
}
