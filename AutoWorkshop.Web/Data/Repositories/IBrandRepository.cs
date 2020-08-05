using AutoWorkshop.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoWorkshop.Web.Data.Repositories
{
    public interface IBrandRepository : IGenericRepository<Brand>
    {

        IEnumerable<SelectListItem> GetComboBrands();
    }
}
