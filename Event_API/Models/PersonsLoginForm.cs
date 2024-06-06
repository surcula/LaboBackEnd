using System.ComponentModel.DataAnnotations;

namespace Event_API.Models
{
    public class PersonsLoginForm
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$",
            ErrorMessage = "Doit contenir une majuscule et 8 characteres ")]
        public string Password { get; set; }
    }
}
