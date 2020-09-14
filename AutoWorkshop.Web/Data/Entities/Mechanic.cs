using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoWorkshop.Web.Data.Entities
{
    public class Mechanic : Person
    {
        public int SpecialtyId { get; set; }

        public Specialty Specialty { get; set; }
    }
}
