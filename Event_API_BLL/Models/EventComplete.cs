using Event_API_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event_API_BLL.Models
{
    public class EventComplete : Event
    {
        public List<Themes> Themes { get; set; }
        public List<Comments> comments { get; set; }

        public List<Exposant_d> exposant_Ds { get; set; }
    }
}
