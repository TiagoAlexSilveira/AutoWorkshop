using AutoWorkshop.Web.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoWorkshop.Web.Models
{
    public class CreateAccountViewModel : Person
    {
        
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }


        [Required]
        public string Password { get; set; }


        [Required]
        [Compare("Password")]
        public string Confirm { get; set; }

        public string Role { get; set; }


        public IEnumerable<SelectListItem> Roles { get; set; }



        public int SpecialtyId { get; set; }


        public Specialty Specialty { get; set; }


        public IEnumerable<SelectListItem> Specialties { get; set; }



    }
}
