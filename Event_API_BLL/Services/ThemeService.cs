using Event_API_BLL.Interfaces;
using Event_API_DAL.Repository;
using Event_API_Domain.Models;
using Microsoft.Data.SqlClient;

namespace Event_API_BLL.Services
{
    public class ThemeService : IThemes
    {
        private readonly IThemesRepo _themeRepos;

        public ThemeService(IThemesRepo themeRepos)
        {
            _themeRepos = themeRepos;
        }

        public void Create(Themes theme)
        {
            try
            {
                _themeRepos.Create(theme);
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
                _themeRepos.Delete(id);
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

        public List<Themes> getAll()
        {
            try
            {
                return _themeRepos.getAll();
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

        public Themes getById(int id)
        {
            try
            {
                return _themeRepos.getById(id);
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
      
        public void update(Themes theme)
        {
            try
            {
                _themeRepos.update(theme);
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
