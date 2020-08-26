using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoWorkshop.Web.Data.Entities
{
    public class Repair : IEntity
    {
        public int Id { get; set; }


        public string RepairInfo { get; set; }


        public DateTime CompletedAt { get; set; }

            
        public int AppointmentId { get; set; }


        public Appointment Appointment { get; set; }


    }
}
