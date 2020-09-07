using AutoWorkshop.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoWorkshop.Web.Models
{
    public class ClientIndexViewModel : Vehicle
    {
        public ICollection<Appointment> Appointments { get; set; }


        public ICollection<Vehicle> Vehicles { get; set; }

    }
}
