using System.ComponentModel.DataAnnotations;

namespace Event_API.Models
{
    public class PersonsRegisterForm
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [Required]
        [MinLength(8)]
        [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$",
            ErrorMessage = "Doit contenir une majuscule et 8 characteres ")]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password), ErrorMessage = "Les deux mots de passe doivent correspondre")]
        public string PasswordConfirm { get; set; }
    }
}
