using Event_API_DAL.Models;

namespace Event_API_DAL.Repository
{
    public interface IRolesRepo
    {
        public List<Roles> GetAll();
        public Roles GetById(int id);
        public void Create(Roles role);
        public void Edit(Roles r);
    }
}