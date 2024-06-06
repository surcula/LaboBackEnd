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
    public class EventThemeService : IEventThemeRepo
    {
        private string _connectionString;
        public EventThemeService(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("default");
        }
        private EventTheme convert(SqlDataReader reader)
        {
            return new EventTheme
            {
                EventThemeId = (int)reader["eventThemeId"],
                ThemeId = (int)reader["themeId"],
                EventId = (int)reader["eventId"]
            };
        }


        public List<EventTheme> getThemesByEventId(int eventId)
        {
            List<EventTheme> theme = new List<EventTheme>();

            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = sql.CreateCommand())
                {
                    cmd.CommandText = " SELECT * FROM EventTheme " +
                        " WHERE eventId = @Id";
                    cmd.Parameters.AddWithValue("Id", eventId);
                    try
                    {
                        sql.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                theme.Add(convert(reader));
                            }
                        }
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
            return theme;
        }

        public void update(EventTheme eventTheme)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = sql.CreateCommand())
                {
                    cmd.CommandText = " UPDATE EventTheme " +
                        " SET themeId = @themeId," +
                        " eventId = @eventId " +
                        " WHERE eventThemeId = @id";
                    cmd.Parameters.AddWithValue("eventId", eventTheme.EventId);
                    cmd.Parameters.AddWithValue("themeId", eventTheme.ThemeId);
                    cmd.Parameters.AddWithValue("eventThemeId", eventTheme.EventThemeId);

                    try
                    {
                        sql.Open();
                        cmd.BeginExecuteNonQuery();
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

        public void Delete(int id)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = sql.CreateCommand())
                {
                    cmd.CommandText = " DELETE EventTheme " +
                        " WHERE eventThemeId = @id";
                    cmd.Parameters.AddWithValue("eventThemeId", id);

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

                        throw;
                    }
                }
            }
        }

        public void Create(EventTheme eventTheme)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = sql.CreateCommand())
                {
                    cmd.CommandText = " Insert INTO EventTheme(eventId,themeId) VALUES(@eventId, @themeId)";
                    cmd.Parameters.AddWithValue("eventId", eventTheme.EventId);
                    cmd.Parameters.AddWithValue("themeId", eventTheme.ThemeId);

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
                        throw;
                    }
                }
            }
        }


    }
}


