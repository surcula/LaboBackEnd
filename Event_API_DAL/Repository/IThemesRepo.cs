using Event_API_DAL.Models;
using Event_API_Domain.Models;

namespace Event_API_DAL.Repository
{
    public interface IThemesRepo
    {
        public void Create(Themes theme);
        List<Themes> getAll();
        Themes getById(int id);
        public List<Themes> getAllById(int id);
        void update(Themes theme);
        void Delete(int id);
    }
}