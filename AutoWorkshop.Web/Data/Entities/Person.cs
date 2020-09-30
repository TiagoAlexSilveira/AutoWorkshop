using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoWorkshop.Web.Data.Entities
{
    public abstract class Person : IEntity  
    {
        public int Id { get; set; }

       
        [Display(Name = "First Name")]
        public string FirstName { get; set; }


        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        [Display(Name = "Image")]
        public string ImageUrl { get; set; }


        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }


        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }


        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime DateofBirth { get; set; }


        [Display(Name = "Tax Number")]
        public string TaxIdentificationNumber { get; set; }


        [Display(Name = "Citizen Card Number")]
        public string CitizenCardNumber { get; set; }


        public string UserId { get; set; }


        public User User { get; set; }



        [Display(Name = "Full Name")]
        public string FullName { get { return $"{this.FirstName} {this.LastName}"; } }

    }
}
