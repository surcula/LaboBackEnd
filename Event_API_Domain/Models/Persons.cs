using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event_API_DAL.Models
{
    public class Persons
    {
        public int PersonId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsBanned { get; set; }
        public int RoleId { get; set; }
    }
}
