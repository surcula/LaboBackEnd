using Event_API_DAL.Repository;
using Event_API_Domain.Interfaces;
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
    public class EventService : ICrud<Event,Event,Event>
    {
        private string _connectionString;

        public EventService(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("default");
        }


        private Event convert(SqlDataReader reader)
        {
            return new Event
            {
                EventId = (int)reader["eventId"],
                Title = (string)reader["title"],
                BeginDate = (DateTime)reader["beginDate"],
                EndDate = (DateTime)reader["EndDate"],
                Address = (string)reader["address"],
                Status = (int)reader["status"]
            };
        }


        public int Create(Event e)
        {
            int id;
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = sql.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Events(Title, BeginDate, EndDate, Address, Status) " +
                                      "OUTPUT inserted.eventId " +
                                      "VALUES(@title, @beginDate, @endDate, @Address, @status)";

                    cmd.Parameters.AddWithValue("title", e.Title);
                    cmd.Parameters.AddWithValue("beginDate", e.BeginDate);
                    cmd.Parameters.AddWithValue("endDate", e.EndDate);
                    cmd.Parameters.AddWithValue("address", e.Address);
                    cmd.Parameters.AddWithValue("status", e.Status);

                    try
                    {
                        sql.Open();
                        id = (int)cmd.ExecuteScalar();
                        sql.Close();
                        return id;
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



        public List<Event> GetAll()
        {
            List<Event> events = new List<Event>();

            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = sql.CreateCommand())
                {
                    cmd.CommandText = " SELECT * FROM Events";

                    try
                    {
                        sql.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                events.Add(convert(reader));
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
            return events;
        }

        public List<Event> GetEventsByDate()
        {
            List<Event> events = new List<Event>();

            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = sql.CreateCommand())
                {
                    cmd.CommandText = " SELECT * FROM Events WHERE begindate > GETDATE();  ";

                    try
                    {
                        sql.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                events.Add(convert(reader));
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
            return events;
        }


        public Event GetById(int id)
        {
            Event e = new Event();

            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = sql.CreateCommand())
                {
                    cmd.CommandText = " SELECT * FROM Events " +
                        " WHERE eventId = @Id";
                    cmd.Parameters.AddWithValue("Id", id);

                    try
                    {
                        sql.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                e = convert(reader);
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
            return e;
        }

        public void Update(Event e)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = sql.CreateCommand())
                {
                    cmd.CommandText = " UPDATE Events " +
                        " SET Title = @title, " +
                        " BeginDate = @beginDate, " +
                        " EndDate = @endDate, " +
                        " Address = @address, " +
                        " Status = @status " +
                        " WHERE eventId = @id";
                    cmd.Parameters.AddWithValue("id", e.EventId);
                    cmd.Parameters.AddWithValue("title", e.Title);
                    cmd.Parameters.AddWithValue("beginDate", e.BeginDate);
                    cmd.Parameters.AddWithValue("endDate", e.EndDate);
                    cmd.Parameters.AddWithValue("address", e.Address);
                    cmd.Parameters.AddWithValue("status", e.Status);

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

        public void Delete(int id)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = sql.CreateCommand())
                {
                    cmd.CommandText = " DELETE Events " +
                        " WHERE eventId = @Id";
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

        
    }
}
