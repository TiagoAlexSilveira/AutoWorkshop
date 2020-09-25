using AutoWorkshop.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoWorkshop.Web.Data.Repositories
{
    public class SecretaryRepository : GenericRepository<Secretary>, ISecretaryRepository
    {

        private readonly DataContext _context;



        public SecretaryRepository(DataContext context) : base(context)
        {
            _context = context;
        }



        public Secretary GetSecretaryByUserId(string id)
        {
            var secretary = _context.Secretaries.FirstOrDefault(u => u.UserId == id);

            return secretary;
        }

    }
}
