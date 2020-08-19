using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoWorkshop.Web.Data.Entities
{
    public abstract class Person : IEntity  // TODO: HOLY SHIT
    {
        public int Id { get; set; }


     
        public string FirstName { get; set; }


    
        public string LastName { get; set; }


   
        public string StreetAddress { get; set; }


     
        public string PhoneNumber { get; set; }


        public string PostalCode { get; set; }



        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime DateofBirth { get; set; }


        public string TaxIdentificationNumber { get; set; }


        public string CitizenCardNumber { get; set; }


        public User User { get; set; }

    }
}
