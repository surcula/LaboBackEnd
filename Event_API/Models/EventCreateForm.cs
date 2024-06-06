using Event_API_Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Event_API.Models
{
    public class EventCreateForm
    {
        [Required]
        [MaxLength(255)]
        public string Title { get; set; }
        [Required]
        
        public DateTime BeginDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        [MaxLength(255)]
        public string Address { get; set; }

        [Required]
        public List<Themes> Themes { get; set; }
    }
}
