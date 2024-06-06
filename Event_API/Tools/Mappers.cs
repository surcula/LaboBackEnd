using Event_API.Models;
using Event_API_DAL.Models;
using Event_API_Domain.Models;
using System.Reflection;

namespace Event_API.Tools
{
    static class Mappers
    {
        
        public static Roles ToDomain (this RolesCreateForm form)
        {
            return new Roles
            {
                Role = form.Role,
            };
        }
        public static Persons ToDomain (this PersonsEditForm form)
        {
            return new Persons
            {
                Email = form.Email,
                FirstName = form.FirstName,
                LastName = form.LastName
            };
        }
        public static Comments ToDomain(this CommentsCreateForm form)
        {
            return new Comments
            {
                Title = form.Title,
                Date = form.Date,
                Message = form.Message,
                PersonId = form.PersonId,
                EventId = form.EventId,
            };
        }
    }
}
