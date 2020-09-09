using AutoWorkshop.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoWorkshop.Web.Models
{
    public class ClientMyAppointmentsViewModel : Appointment
    {

        public ICollection<Appointment> Appointments { get; set; }
    }
}
