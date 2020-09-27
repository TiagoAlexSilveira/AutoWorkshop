using AutoWorkshop.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoWorkshop.Web.Models
{
    public class RepairViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Repair Information")]
        [DataType(DataType.MultilineText)]
        public string RepairInfo { get; set; }


        [Display(Name = "Date and Time of Completion")]
        public DateTime CompletedAt { get; set; }


        public int AppointmentId { get; set; }


        public Appointment Appointment { get; set; }

        public IEnumerable<SelectListItem> Appointments { get; set; }

    }
}
