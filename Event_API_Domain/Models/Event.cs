using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event_API_Domain.Models
{
    public  class Event
    {
        public int EventId { get; set; } 
        public string Title { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Address { get; set; }
        public int Status { get; set; }
    }
}
