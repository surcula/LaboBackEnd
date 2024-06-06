using Event_API_BLL.Models;
using Event_API_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event_API_BLL.Mapper
{
    public static class Mapper
    {
        public static Event ToDall(this EventComplete e)
        {
            return new Event
            {
                Title = e.Title,
                BeginDate = e.BeginDate,
                EndDate = e.EndDate,
                Address = e.Address,
            };
        }
    }
}
