using System.ComponentModel.DataAnnotations;

namespace AutoWorkshop.Web.Models
{
    public class RecoverPasswordViewModel
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
