using Event_API_Domain.Models;

namespace Event_API_DAL.Repository
{
    public interface ICommentsRepo
    {
        void Create(Comments comments);
        void Delete(int id);
        List<Comments> GetAllByEventId(int id);
    }
}