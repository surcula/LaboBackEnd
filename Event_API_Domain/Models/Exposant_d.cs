using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event_API_Domain.Models
{
    public class Exposant_d
    {
        public int PersonEventId { get; set; }
        public int PersonId { get; set; }
        public int EventId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Gsm { get; set; }
        public string Comments { get; set; }
        public int Status { get; set; }
    }
}
