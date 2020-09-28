using AutoWorkshop.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoWorkshop.Web.Models
{
    public class VehicleViewModel
    {
        public int Id { get; set; }


        public int BrandId { get; set; }


        public Brand Brand { get; set; }



        [Required(ErrorMessage = "You must insert a value for {0}")]
        [StringLength(150, ErrorMessage = "{0} should be {2} to {1} characters long", MinimumLength = 2)]
        public string Model { get; set; }

        //public string SubModel { get; set; }

        [StringLength(150, ErrorMessage = "{0} should be {2} to {1} characters long", MinimumLength = 2)]
        [Required(ErrorMessage = "You must insert a value for {0}")]
        public string Transmission { get; set; } //automática ou manual


        [StringLength(150, ErrorMessage = "{0} should be {2} to {1} characters long", MinimumLength = 2)]
        [Required(ErrorMessage = "You must insert a value for {0}")]
        public string Type { get; set; }   //ligeiro ou pesado


        [Required(ErrorMessage = "You must insert a value for {0}")]
        [StringLength(50, ErrorMessage = "{0} should be {2} to {1} characters long", MinimumLength = 3)]
        public string Color { get; set; }


        [Range(2,10000000, ErrorMessage = "{0} should be {1} to 8 characters long")]
        [Required(ErrorMessage = "You must insert a value for {0}")]
        public int? Mileage { get; set; }


        [Range(2, 9999, ErrorMessage = "{0} should be {1} to 4 characters long")]
        [Required(ErrorMessage = "You must insert a value for {0}")]
        [Display(Name = "Engine Power")]
        public int? EnginePower { get; set; }


        [StringLength(8, ErrorMessage = "A {0} deverá ter entre {2} e {1} caracteres", MinimumLength = 8)]
        [Display(Name = "License Plate")]
        [Required(ErrorMessage = "You must insert a {0}")]
        public string LicensePlate { get; set; }



        [Required(ErrorMessage = "You must insert a {0} date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        [Display(Name = "Last Maintenance")]
        public DateTime? LastMaintenance { get; set; }


        //public VIN_Number { get; set; }  //número que vem quando se monta o carro(para saber que é legit)

        public string UserId { get; set; }

        public User User { get; set; }

        public IEnumerable<SelectListItem> Brands { get; set; }

        public int ClientId { get; set; }

        public IEnumerable<SelectListItem> Clients { get; set; }
    }
}
