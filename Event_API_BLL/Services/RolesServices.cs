
using Event_API_BLL.Interfaces;
using Event_API_DAL.Models;
using Event_API_DAL.Repository;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event_API_BLL.Services
{
    public class RolesServices : IRolesServices
    {
        private readonly IRolesRepo _rolesRepo;
        public RolesServices(IRolesRepo rolesRepo)
        {
            _rolesRepo = rolesRepo;
        }
        public void Create(Roles role)
        {
            try
            {
                _rolesRepo.Create(role);
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

        public void Edit(Roles r)
        {
            try
            {
                _rolesRepo.Edit(r);
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

        public List<Roles> GetAll()
        {
            return _rolesRepo.GetAll();
        }

        public Roles GetById(int id)
        {
            return _rolesRepo.GetById(id);
        }
    }
}
