using Event_API_DAL.Repository;
using Event_API_Domain.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event_API_DAL.Services
{
    public class ThemesServices : IThemesRepo
    {
        private string _connectionString;
        public ThemesServices(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("default");
        }
        private Themes convert(SqlDataReader reader)
        {
            return new Themes
            {
                ThemeId = (int)reader["themeId"],
                Theme = (string)reader["theme"]
            };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns> List of all themes </returns>
        public List<Themes> getAll()
        {
            List<Themes> themes = new List<Themes>();

            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = sql.CreateCommand())
                {
                    cmd.CommandText = " SELECT * FROM Themes";

                    try
                    {
                        sql.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                themes.Add(convert(reader));
                            }
                        }
                        sql.Close();
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }
                }
            }
            return themes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>1 theme by id</returns>
        public Themes getById(int id)
        {
            Themes theme = new Themes();

            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = sql.CreateCommand())
                {
                    cmd.CommandText = " SELECT * FROM Themes " +
                        " WHERE themeId = @Id";
                    cmd.Parameters.AddWithValue("Id", id);
                    try
                    {
                        sql.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                theme = convert(reader);
                            }
                        }
                        sql.Close();
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }
                }
            }
            return theme;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>All theme by id</returns>
        public List<Themes> getAllById(int id)
        {
            List<Themes> themes = new List<Themes>();

            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = sql.CreateCommand())
                {

                    cmd.CommandText = " select distinct t.Theme,t.ThemeId from Events e " +
                        " join EventTheme et on e.eventId = et.eventId " +
                        " join Themes t on t.themeId = et.themeId " +
                        " where e.eventId = @Id ";


                    cmd.Parameters.AddWithValue("@Id", id);


                    try
                    {
                        sql.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                themes.Add(convert(reader));
                            }
                        }
                        sql.Close();
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }
                }
            }
            return themes;
        }


        /// <summary>
        /// Update a theme
        /// </summary>
        /// <param name="theme"></param>
        public void update(Themes theme)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = sql.CreateCommand())
                {
                    cmd.CommandText = " UPDATE Themes " +
                        " SET theme = @theme " +
                        " WHERE themeId = @id";
                    cmd.Parameters.AddWithValue("id", theme.ThemeId);
                    cmd.Parameters.AddWithValue("theme", theme.Theme);

                    try
                    {
                        sql.Open();
                        cmd.ExecuteNonQuery();
                        sql.Close();
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }
                }
            }
        }


        /// <summary>
        /// Delete theme
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = sql.CreateCommand())
                {
                    cmd.CommandText = " DELETE Themes " +
                        " WHERE themeId = @Id";
                    cmd.Parameters.AddWithValue("Id", id);

                    try
                    {
                        sql.Open();
                        cmd.ExecuteNonQuery();
                        sql.Close();
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

        public void Create(Themes theme)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = sql.CreateCommand())
                {
                    cmd.CommandText = " Insert INTO Themes(Theme) VALUES(@theme)";
                    cmd.Parameters.AddWithValue("theme", theme.Theme);

                    try
                    {
                        sql.Open();
                        cmd.ExecuteNonQuery();
                        sql.Close();
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
    }
}
