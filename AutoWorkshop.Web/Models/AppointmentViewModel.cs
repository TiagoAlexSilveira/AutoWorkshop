using AutoWorkshop.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AutoWorkshop.Web.Models
{
    public class AppointmentViewModel
    {
        public int Id { get; set; }


        public int AppointmentTypeId { get; set; }


        public AppointmentType AppointmentType { get; set; }


        public string Information { get; set; }


        public int? MechanicId { get; set; }

      
        public Mechanic Mechanic { get; set; }


        public int ClientId { get; set; }


        public Client Client { get; set; }


        public int VehicleId { get; set; }


        public Vehicle Vehicle { get; set; }


        public bool IsConfirmed { get; set; }


        public bool IsUrgent { get; set; }


        public IEnumerable<Client> Clients { get; set; }


        public List<Appointment> Appointments { get; set; }


        public IEnumerable<Vehicle> Vehicles { get; set; }


        public IEnumerable<AppointmentType> AppointmentTypes { get; set; }


        public IEnumerable<Mechanic> Mechanics { get; set; }


        public IEnumerable<SelectListItem> MechanicsCombo { get; set; }


        public IEnumerable<Appointment> UnconfirmedAppointments { get; set; }


        [NotMapped]
        public string Subject { get; set; }


        public DateTime StartTime { get; set; }


        public DateTime EndTime { get; set; }

    }
}
