using Event_API_BLL.Interfaces;
using Event_API_DAL.Repository;
using Event_API_Domain.Models;
using Microsoft.Data.SqlClient;

namespace Event_API_BLL.Services
{
    public class EventThemeService : IEventTheme
    {

        private IEventThemeRepo _eventThemeRepo;

        public EventThemeService(IEventThemeRepo eventThemeRepo)
        {
            _eventThemeRepo = eventThemeRepo;
        }

        public void Create(EventTheme eventTheme)
        {
            try
            {
                _eventThemeRepo.Create(eventTheme);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public void Delete(int id)
        {
            try
            {
                _eventThemeRepo.Delete(id);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<EventTheme> getThemesByEventId(int eventId)
        {
           return _eventThemeRepo.getThemesByEventId(eventId);
        }

        public void update(EventTheme eventTheme)
        {
            try
            {
                _eventThemeRepo.update(eventTheme);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    }
}
