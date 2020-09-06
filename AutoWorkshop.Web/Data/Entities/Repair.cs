using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoWorkshop.Web.Data.Entities
{
    public class Repair : IEntity
    {
        public int Id { get; set; }

        [Display(Name ="Repair Information")]
        [DataType(DataType.MultilineText)]
        public string RepairInfo { get; set; }

        [Display(Name = "Date and Time of Completion")]
        public DateTime CompletedAt { get; set; }

        
        public int AppointmentId { get; set; }


        public Appointment Appointment { get; set; }


    }
}
