using AutoWorkshop.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoWorkshop.Web.Models
{
    public class SecUnconfAppointViewModel : Appointment
    {

        public IEnumerable<Appointment> ConfirmedAppointments { get; set; }


        public IEnumerable<Mecanic> Mecanics { get; set; }


        public IEnumerable<SelectListItem> Mechanics { get; set; }


        public IEnumerable<Appointment> UnconfirmedAppointments { get; set; }
    }
}
