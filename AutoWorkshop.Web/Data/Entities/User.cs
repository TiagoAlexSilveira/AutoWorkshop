using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoWorkshop.Web.Data.Entities
{
    public class User : IdentityUser
    {

        //[MaxLength(50, ErrorMessage = "The field {0} can only contain {1} characters.")]
        //public string FirstName { get; set; }



        //[MaxLength(50, ErrorMessage = "The field {0} can only contain {1} characters.")]
        //public string LastName { get; set; }



        //[MaxLength(100, ErrorMessage = "The field {0} can only contain {1} characters.")]
        //public string Address { get; set; }



        //[Display(Name = "Full Name")]
        //public string FullName { get { return $"{this.FirstName} {this.LastName}"; } }


       
    }
}
