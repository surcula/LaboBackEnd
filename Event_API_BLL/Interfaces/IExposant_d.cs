using Event_API_Domain.Models;

namespace Event_API_BLL.Interfaces
{
    public interface IExposant_d
    {
        void Create(Exposant_d e);
        void Delete(int id);
        List<Exposant_d> GetAll();
        List<Exposant_d> GetAllByEventId(int eventId);
        Exposant_d GetById(int id);
        void Update(Exposant_d e);
    }
}