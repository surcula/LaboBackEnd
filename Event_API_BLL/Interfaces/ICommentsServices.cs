using Event_API_Domain.Models;

namespace Event_API_BLL.Interfaces
{
    public interface ICommentsServices
    {
        void Create(Comments comments);
        void Delete(int Id);
    }
}