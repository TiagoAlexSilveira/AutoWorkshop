using AutoWorkshop.Web.Data.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoWorkshop.Web.Models
{
    public class ChangeUserViewModel 
    {
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }


        [Display(Name = "Image")]
        public string ImageUrl { get; set; }


        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }


        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        
        public string StreetAddress { get; set; }



        public string PhoneNumber { get; set; }


       
        public string PostalCode { get; set; }



        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime DateofBirth { get; set; }


        
        public string TaxIdentificationNumber { get; set; }


        
        public string CitizenCardNumber { get; set; }



        public int SpecialtyId { get; set; }


        public Specialty Specialty { get; set; }

    }
}
