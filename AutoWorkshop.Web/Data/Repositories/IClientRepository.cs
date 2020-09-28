using AutoWorkshop.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoWorkshop.Web.Data.Repositories
{
    public interface IClientRepository : IGenericRepository<Client>
    {
        Client GetClientByUserEmail(string email);


        Client GetClientByUserId(string id);
        IEnumerable<SelectListItem> GetComboClients();
    }
}
