using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoWorkshop.Web.Data.Entities
{
    public class Vehicle
    {
        public int Id { get; set; }


        public string Transmission { get; set; } //automática ou manual

       
        public string Type { get; set; }



        [StringLength(50, ErrorMessage = "{0} should be {2} to {1} characters long", MinimumLength = 3)]
        public string Color { get; set; }


        [Required(ErrorMessage = "You must insert a value for {0}")]
        public int Mileage { get; set; }


        [Display(Name = "Engine Power")]
        public string EnginePower { get; set; }


        //[StringLength(7, ErrorMessage = "A {0} deverá ter entre {2} e {1} caracteres", MinimumLength = 1)]
        [Display(Name = "License Plate")]
        [Required(ErrorMessage = "You must insert a {0}")]
        public string LicensePlate { get; set; }


        [DataType(DataType.Date)]
        [Required(ErrorMessage = "You must insert a {0} date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Last Maintenance")]
        public DateTime LastMaintenance { get; set; }
    }
}
