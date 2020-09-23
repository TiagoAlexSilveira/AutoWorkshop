using AutoWorkshop.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoWorkshop.Web.Models
{
    public class AppointmentViewModel : Appointment
    {

        public IEnumerable<Vehicle> Vehicles { get; set; }

        public IEnumerable<AppointmentType> AppointmentTypes { get; set; }

    }
}
