using System.ComponentModel.DataAnnotations;

namespace Event_API.Models
{
    public class PersonsEditForm
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        
    }
}
