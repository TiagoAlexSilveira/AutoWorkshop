using AutoWorkshop.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoWorkshop.Web.Models
{
    public class MecAppointViewModel
    {
        public int Id { get; set; }


        public int AppointmentTypeId { get; set; }


        public AppointmentType AppointmentType { get; set; }


        [Display(Name="Start Time")]
        public DateTime StartTime { get; set; }


        public string Information { get; set; }


        public int? MechanicId { get; set; }


        public Mechanic Mechanic { get; set; }


        public int ClientId { get; set; }


        public Client Client { get; set; }


        public int VehicleId { get; set; }


        public Vehicle Vehicle { get; set; }


        public bool IsConfirmed { get; set; }


        public bool IsUrgent { get; set; }


        public IEnumerable<Appointment> Appointments { get; set; }
    }
}
