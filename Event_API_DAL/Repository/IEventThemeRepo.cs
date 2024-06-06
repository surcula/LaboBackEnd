using Event_API_Domain.Models;

namespace Event_API_DAL.Repository
{
    public interface IEventThemeRepo
    {
        void Create(EventTheme eventTheme);
        List<EventTheme> getThemesByEventId(int eventId);
        void update(EventTheme eventTheme);
        void Delete(int id);
    }
}