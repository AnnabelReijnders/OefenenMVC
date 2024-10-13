using System.ComponentModel.DataAnnotations;

namespace OefenenMVC.Models
{
    public class RegisterModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Voer een geldig e-mailadres in.")]
        public string Email { get; set; }

        [Required]
        [Phone(ErrorMessage = "Voer een geldig telefoonnummer in.")]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
