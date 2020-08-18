using AutoWorkshop.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoWorkshop.Web.Models
{
    public class VehicleViewModel : Vehicle
    {

        //[Display(Name = "Brand")]
        //[Range(1, int.MaxValue, ErrorMessage = "Please insert a brand!")]
        //public int BrandId { get; set; }

        public IEnumerable<SelectListItem> Brands { get; set; }
    }
}
