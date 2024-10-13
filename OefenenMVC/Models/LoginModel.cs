using System.ComponentModel.DataAnnotations;

namespace OefenenMVC.Models
{
    public class LoginModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Voer een geldig e-mailadres in.")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
