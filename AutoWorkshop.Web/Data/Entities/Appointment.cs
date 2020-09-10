using System;
using System.ComponentModel.DataAnnotations;

namespace AutoWorkshop.Web.Data.Entities
{
    public class Appointment : IEntity
    {

        public int Id { get; set; }


        public int AppointmentTypeId { get; set; }

    
        public AppointmentType AppointmentType { get; set; }


        [Required]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = false)]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }


        //TODO: a data está a guardar com campos a mais (para o data e a hora), o syncfusion na view mostra uma linha enorme
        [Required]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]  
        [DataType(DataType.Time)]
        public DateTime Time { get; set; }

        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Time)]
        public DateTime WorkEstimate { get; set; }  //TODO: retirar Work Estimate


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
