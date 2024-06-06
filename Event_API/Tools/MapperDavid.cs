using Event_API.Models;
using Event_API_BLL.Models;
using Event_API_Domain.Models;
using System.Runtime.CompilerServices;

namespace Event_API.Tools
{
    public static class MapperDavid
    {
        public static Themes ToBLL(this ThemeCreateForm form)
        {  
            return new Themes { Theme = form.Theme }; 
        }

        public static Themes ToBLL(this ThemeUpdateForm form)
        {
            return new Themes { Theme = form.Theme };
        }

        public static EventComplete ToBLLEvent(this EventCreateForm form)
        {
            return new EventComplete { 
                Title = form.Title,
                BeginDate = form.BeginDate,
                EndDate = form.EndDate,
                Address = form.Address,
                Themes = form.Themes,              
                
            };
        }
        public static Event ToBLLEvent(this EventUpdateForm form)
        {
            return new Event {                 
                Title = form.Title,
                BeginDate = form.BeginDate,
                EndDate = form.EndDate,
                Address = form.Address,
                Status = form.Status
            };
        }
        public static Exposant_d ToBll( this Exposant_dCreateForm form) 
        {
            return new Exposant_d {
                EventId = form.EventId,
                PersonId = form.PersonId,
                Comments = form.Comments,
                Date = form.Date,
                Gsm = form.Gsm,
                Name = form.Name,
            };
        }

        public static Exposant_d ToBll(this Exposant_dUpdateForm form)
        {
            return new Exposant_d
            {
                EventId = form.EventId,
                PersonId = form.PersonId,
                Comments = form.Comments,
                Date = form.Date,
                Gsm = form.Gsm,
                Name = form.Name,
                Status = form.Statut
            };
        }





    }
}
