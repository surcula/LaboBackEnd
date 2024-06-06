using Event_API_DAL.Models;

namespace Event_API_DAL.Repository
{
    public interface IPersonsRepo
    {
        
        void Edit(Persons p);
        List<Persons> GetAll();
        Persons GetById(int id);
        string GetHashPwd(string email);
        void IsBanned(bool isbanned, int personId);
        Persons Login(string email, string password);
        void Register(string email, string firstName, string lastName, string password);
        public void EditStatus(int id, int status);
    }
}