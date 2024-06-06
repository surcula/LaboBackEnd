using System.ComponentModel.DataAnnotations;

namespace Event_API.Models
{
    public class RolesCreateForm
    {
        [Required]
        public string Role { get; set; }
    }
}
