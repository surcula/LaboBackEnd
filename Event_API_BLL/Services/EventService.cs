using Event_API_BLL.Interfaces;
using Event_API_BLL.Mapper;
using Event_API_BLL.Models;
using Event_API_DAL.Repository;
using Event_API_Domain.Interfaces;
using Event_API_Domain.Models;
using Microsoft.Data.SqlClient;

namespace Event_API_BLL.Services
{
    public class EventService : ICrud<Event, EventComplete,EventComplete>
    {

        private readonly ICrud<Event, Event,Event> _eventRepo;
        private readonly IThemesRepo _themesRepo;
        private readonly ICommentsRepo _commentsRepo;
        private readonly IExposantRepo_d _exposantRepo;
        private readonly IEventThemeRepo _eventThemeRepo;

        public EventService(ICrud<Event, Event, Event> eventRepo, IThemesRepo themesRepo, ICommentsRepo commentsRepo, IExposantRepo_d exposantRepo, IEventThemeRepo eventThemeRepo)
        {
            _eventRepo = eventRepo;
            _themesRepo = themesRepo;
            _commentsRepo = commentsRepo;
            _exposantRepo = exposantRepo;
            _eventThemeRepo = eventThemeRepo;
        }

        private EventComplete ToApi(Event e, List<Themes> themes, List<Comments> c, List<Exposant_d> exposants)
        {

            return new EventComplete
            {
                EventId = e.EventId,
                Title = e.Title,
                BeginDate = e.BeginDate,
                EndDate = e.EndDate,
                Address = e.Address,
                Status = e.Status,
                Themes = themes,
                comments = c,
                exposant_Ds = exposants
            };
        }

        public int Create(EventComplete e)
        {
            try
            {
                if (e.BeginDate <= e.EndDate && (e.EndDate - e.BeginDate) <= TimeSpan.FromDays(3))
                {
                    int eventId = _eventRepo.Create(e.ToDall());
                    if (e.Themes.Count > 0)
                    {
                        foreach (var eventTheme in e.Themes)
                        {

                            try
                            {
                                EventTheme et = new EventTheme { EventId = eventId, ThemeId = eventTheme.ThemeId };
                                _eventThemeRepo.Create(et);
                            }
                            catch (SqlException ex)
                            {

                                throw ex;
                            }
                            catch (Exception ex)
                            {

                                throw ex;
                            }                       }
                    }
                    return 0;
                }
                
                else
                {
                    throw new Exception(" Désolé mais les dates ne sont pas bonnes.");
                }              



                
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
                _eventRepo.Delete(id);
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

        public List<Event> GetAll()
        {
            try
            {
                return _eventRepo.GetAll();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                // Utiliser eventuellement un compteur en réessayant
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Return all events with theme(s)</returns>
        public List<EventComplete> GetAllComplete()
        {
            try
            {
                List<EventComplete> eventCompletes = new List<EventComplete>();
                List<Event> events = GetAll();
                foreach (Event e in events)
                {
                    eventCompletes.Add(ToApi(e, _themesRepo.getAllById(e.EventId), _commentsRepo.GetAllByEventId(e.EventId),_exposantRepo.GetAllByEventId(e.EventId)));
                }
                return eventCompletes;
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

        public EventComplete GetById(int id)
        {
            try
            {

                Event e = _eventRepo.GetById(id);
                return ToApi(_eventRepo.GetById(id), _themesRepo.getAllById(id), _commentsRepo.GetAllByEventId(id), _exposantRepo.GetAllByEventId(e.EventId));
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

        public void Update(Event e)
        {
            try
            {
                _eventRepo.Update(e);
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
