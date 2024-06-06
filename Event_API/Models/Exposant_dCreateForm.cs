using System.ComponentModel.DataAnnotations;

namespace Event_API.Models
{
    public class Exposant_dCreateForm
    {
        [Required]
        public int PersonId { get; set; }
        [Required]
        public int EventId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Gsm { get; set; }
        [Required]
        public string Comments { get; set; }
    }
}
