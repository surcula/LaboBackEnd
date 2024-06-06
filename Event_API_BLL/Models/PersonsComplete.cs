using Event_API_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event_API_BLL.Models
{
    public class PersonsComplete {

        public Roles Role {  get; set; }
        public int PersonId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsBanned { get; set; }
        


    }
}
