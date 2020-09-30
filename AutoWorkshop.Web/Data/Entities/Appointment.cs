using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoWorkshop.Web.Data.Entities
{
    public class Appointment : IEntity
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



        [NotMapped]
        public string Subject { get; set; }


        public DateTime StartTime { get; set; }

 
        public DateTime EndTime { get; set; }
    }
}
