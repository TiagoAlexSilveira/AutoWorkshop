using System;
using System.ComponentModel.DataAnnotations;

namespace AutoWorkshop.Web.Data.Entities
{
    public class Appointment : IEntity
    {

        public int Id { get; set; }


        public int AppointmentTypeId { get; set; }


        //public string AppointmenType  //TODO: APPOINTMENT TYPE
        public AppointmentType AppointmentType { get; set; }


        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }


        [Required]
        [DataType(DataType.Time)]
        public DateTime Time { get; set; }


        public DateTime WorkEstimate { get; set; }


        public string Information { get; set; }


        public int? MecanicId { get; set; }


        public Mecanic Mecanic { get; set; }


        public int ClientId { get; set; }


        public Client Client { get; set; }


        public int VehicleId { get; set; }


        public Vehicle Vehicle { get; set; }


        public bool IsConfirmed { get; set; }


        public bool IsUrgent { get; set; }

    }
}
