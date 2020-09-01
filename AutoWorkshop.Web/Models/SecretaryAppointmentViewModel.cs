using AutoWorkshop.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoWorkshop.Web.Models
{
    public class SecretaryAppointmentViewModel : Appointment
    {

        public IEnumerable<Appointment> Appointments { get; set; }


        public IEnumerable<SelectListItem> Mechanics { get; set; }
    }
}
