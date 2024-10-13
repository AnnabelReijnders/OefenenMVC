using System.ComponentModel.DataAnnotations;

namespace OefenenMVC.Models
{
    public class ForgotPasswordModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Voer een geldig e-mailadres in.")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Nieuw Wachtwoord")]
        public string NewPassword { get; set; }
    }
}
