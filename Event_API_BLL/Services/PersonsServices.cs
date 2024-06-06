
using Event_API_BLL.Interfaces;
using Event_API_BLL.Models;
using Event_API_DAL.Models;
using Event_API_DAL.Repository;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event_API_BLL.Services
{
    public class PersonsServices : IPersonsServices
    {
        private readonly IPersonsRepo _personsRepo;
        private readonly IRolesRepo _rolesRepo;
        public PersonsServices(IPersonsRepo personsRepo, IRolesRepo rolesRepo)
        {
            _personsRepo = personsRepo;
            _rolesRepo = rolesRepo;
        }
        public void Register(string email, string firstName, string lastName, string password)
        {
            try
            {
                string hashpassword = BCrypt.Net.BCrypt.HashPassword(password);
                _personsRepo.Register( email, firstName, lastName, hashpassword);
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

        public void Edit(Persons p)
        {
            try
            {
                _personsRepo.Edit(p);
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

        public List<PersonsComplete> GetAll()
        {
            List<PersonsComplete> personsCompletes = new List<PersonsComplete>();

            List<Persons> list = _personsRepo.GetAll();
            for (int i = 0; i < list.Count; i++)
            {
                personsCompletes.Add(new PersonsComplete
                {
                    PersonId = list[i].PersonId,
                    Email = list[i].Email,
                    FirstName = list[i].FirstName,
                    LastName = list[i].LastName,
                    Role = _rolesRepo.GetById(list[i].RoleId),
                    IsBanned = list[i].IsBanned,
                });
            }
            return personsCompletes;
        }

        public PersonsComplete GetById(int id)
        {
            Persons p = _personsRepo.GetById(id);
            return new PersonsComplete
            {
                PersonId = p.PersonId,
                Email = p.Email,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Role = _rolesRepo.GetById(p.RoleId),
                IsBanned = p.IsBanned,
            };

        }

        public void IsBanned(bool isbanned, int personId)
        {
            try
            {
                _personsRepo.IsBanned(isbanned, personId);
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

        public Persons Login(string email, string password)
        {
            string verifyPWD = _personsRepo.GetHashPwd(email);
            if (BCrypt.Net.BCrypt.Verify(password, verifyPWD))
            {
                try
                {
                    Persons connecterUser = _personsRepo.Login(email, verifyPWD);
                    return connecterUser;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                throw new InvalidOperationException("Mot de passe incorrect");
            }

        }
        public void EditStatus(int id, int status)
        {
            try
            {
                _personsRepo.EditStatus(id, status);
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
