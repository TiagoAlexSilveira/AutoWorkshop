﻿using AutoWorkshop.Web.Data.Entities;
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
    }
}
