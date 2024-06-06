using Event_API_BLL.Interfaces;
using Event_API_DAL.Repository;
using Event_API_Domain.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event_API_BLL.Services
{
    public class ExposantService_d : IExposant_d
    {
        private readonly IExposantRepo_d _exposantRepo;

        public ExposantService_d(IExposantRepo_d exposantRepo)
        {
            _exposantRepo = exposantRepo;
        }



        public void Create(Exposant_d e)
        {
            try
            {
                _exposantRepo.Create(e);
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
                _exposantRepo.Delete(id);
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

        public List<Exposant_d> GetAll()
        {
            try
            {
                return _exposantRepo.GetAll();
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

        public List<Exposant_d> GetAllByEventId(int eventId)
        {
            try
            {
                return _exposantRepo.GetAllByEventId(eventId);
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

        public Exposant_d GetById(int id)
        {
            try
            {
                return _exposantRepo.GetById(id);
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

        public void Update(Exposant_d e)
        {
            try
            {
                _exposantRepo.Update(e);
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
