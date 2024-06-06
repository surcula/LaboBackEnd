using Event_API_BLL.Models;
using Event_API_DAL.Models;

namespace Event_API_BLL.Interfaces
{
    public interface IPersonsServices
    {
        void Register(string email, string firstName, string lastName, string password);
        Persons Login(string email, string password);
        void Edit(Persons p);
        List<PersonsComplete> GetAll();
        PersonsComplete GetById(int id);
        void IsBanned(bool isbanned, int personId);
        public void EditStatus(int id, int status);
    }
}

