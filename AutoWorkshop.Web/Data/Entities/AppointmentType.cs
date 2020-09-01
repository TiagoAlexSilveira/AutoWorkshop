using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoWorkshop.Web.Data.Entities
{
    public class AppointmentType : IEntity
    {
        public int Id { get; set; }


        public string Type { get; set; }
    }
}
