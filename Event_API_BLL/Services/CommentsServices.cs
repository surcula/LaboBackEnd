using Event_API_BLL.Interfaces;
using Event_API_DAL.Repository;
using Event_API_Domain.Interfaces;
using Event_API_Domain.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event_API_BLL.Services
{
    public class CommentsServices : ICommentsServices
    {
        private readonly ICommentsRepo _commentsRepo;
        private readonly ICrud<Event, Event, Event> _eventRepo;

        public CommentsServices(ICommentsRepo commentsRepo, ICrud<Event, Event, Event> eventRepo)
        {
            _commentsRepo = commentsRepo;
            _eventRepo = eventRepo;
        }

        public void Create(Comments comments)
        {            

            try
            {
                Event e = _eventRepo.GetById(comments.EventId);
                if (e.EndDate > comments.Date) throw new Exception("Désolé mais l'event n'est pas terminé. Vous ne pouvez pas le commenter.");
                _commentsRepo.Create(comments);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex) 
            { 
                throw ex ; 
            }
        }
        public void Delete(int Id)
        {
            try
            {
                _commentsRepo.Delete(Id);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex ;
            }
        }


    }
}
