using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoWorkshop.Web.Data.Entities
{
    public class Brand : IEntity
    {
        public int Id { get; set; }



        [Display(Name = "Brand")]
        [StringLength(40, ErrorMessage = "A {0} deverá ter entre {2} e {1} caracteres", MinimumLength = 1)]
        [Required(ErrorMessage = "Tem de inserir uma {0}")]
        public string BrandName { get; set; }




    }
}
