using AutoWorkshop.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoWorkshop.Web.Data.Repositories
{
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {

        private readonly DataContext _context;



        public ClientRepository(DataContext context) : base(context)
        {
            _context = context;
        }



        public Client GetClientByUserId(string id)
        {
            var client = _context.Clients.FirstOrDefault(u => u.UserId == id);

            return client;
        }


        public Client GetClientByUserEmail(string email)
        {
            var client = _context.Clients.FirstOrDefault(e => e.User.Email == email);

            return client;
        }


        public IEnumerable<SelectListItem> GetComboClients()
        {
            var list = _context.Clients.Select(b => new SelectListItem
            {
                Text = b.FullName + " - " + b.PhoneNumber,
                Value = b.Id.ToString()
            }).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Select a client)",
                Value = "0"
            });

            return list;
        }
    }
}
