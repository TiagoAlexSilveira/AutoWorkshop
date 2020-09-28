using AutoWorkshop.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoWorkshop.Web.Models
{
    public class SecConfAppointViewModel 
    {
        public int Id { get; set; }


        public int AppointmentTypeId { get; set; }


        public AppointmentType AppointmentType { get; set; }


        [Required]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = false)]
        [DataType(DataType.Date)]
        public DateTime EndTime { get; set; }


        [Required]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = false)]
        [DataType(DataType.Time)]
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

        public IEnumerable<Appointment> ConfAppointments { get; set; }
    }
}
