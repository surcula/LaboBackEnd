using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event_API_Domain.Models
{
    public class EventTheme
    {
        public int EventThemeId { get; set; }
        public int EventId { get; set; }
        public int ThemeId { get; set; }
    }
}
