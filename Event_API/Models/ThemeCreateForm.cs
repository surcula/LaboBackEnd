using System.ComponentModel.DataAnnotations;

namespace Event_API.Models
{
    public class ThemeCreateForm
    {
        [Required]
        [MaxLength(255)] 
        public string Theme { get; set; }
    }
}
