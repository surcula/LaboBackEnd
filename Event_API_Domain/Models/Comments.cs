using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event_API_Domain.Models
{
    public class Comments
    {
        public int CommentId { get; set; }
        public string Title { get; set; }
        public DateTime Date {  get; set; }
        public string Message { get; set; }
        public int EventId { get; set; }
        public int PersonId { get; set; }
    }
}
