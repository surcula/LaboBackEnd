using Event_API_DAL.Models;

namespace Event_API_BLL.Interfaces
{
    public interface IRolesServices
    {
        void Create(Roles role);
        void Edit(Roles r);
        List<Roles> GetAll();
        Roles GetById(int id);
    }
}