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
    public class ExposantService_d : IExposantRepo_d
    {
        private readonly string _connectionString;

        public ExposantService_d(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("Default");
        }

        private Exposant_d convert(SqlDataReader reader)
        {
            return new Exposant_d
            {
                PersonEventId = (int)reader["PersonEventId"],
                PersonId = (int)reader["PersonId"],
                EventId = (int)reader["eventId"],
                Name = (string)reader["Name"],
                Date = (DateTime)reader["Date"],
                Gsm = (string)reader["Gsm"],
                Comments = (string)reader["comments"],
                Status = (int)reader["status"]
            };
        }


        public void Create(Exposant_d e)
        {

            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = sql.CreateCommand())
                {
                    cmd.CommandText = " Insert INTO Exposants(PersonId, EventId,Name,Date,Gsm, Comments,Status) " +
                        " VALUES(@PersonId, @EventId,@Name, @Date, @Gsm, @Comments,@Status)";

                    cmd.Parameters.AddWithValue("PersonId", e.PersonId);
                    cmd.Parameters.AddWithValue("EventId", e.EventId);
                    cmd.Parameters.AddWithValue("Name", e.Name);
                    cmd.Parameters.AddWithValue("Date", e.Date);
                    cmd.Parameters.AddWithValue("Gsm", e.Gsm);
                    cmd.Parameters.AddWithValue("Comments", e.Comments);
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



        public List<Exposant_d> GetAll()
        {
            List<Exposant_d> exposants = new List<Exposant_d>();

            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = sql.CreateCommand())
                {
                    cmd.CommandText = " SELECT * FROM Exposants";

                    try
                    {
                        sql.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                exposants.Add(convert(reader));
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
            return exposants;
        }

        public Exposant_d GetById(int id)
        {
            Exposant_d e = new Exposant_d();

            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = sql.CreateCommand())
                {
                    cmd.CommandText = " SELECT * FROM Exposants " +
                        " WHERE PersonEventId = @Id";
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

        public List<Exposant_d> GetAllByEventId(int eventId)
        {
            List<Exposant_d> e = new List<Exposant_d>();

            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = sql.CreateCommand())
                {
                    cmd.CommandText = " SELECT * FROM Exposants " +
                        " WHERE eventId = @Id";
                    cmd.Parameters.AddWithValue("Id", eventId);

                    try
                    {
                        sql.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                e.Add(convert(reader));
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


        public void Update(Exposant_d e)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = sql.CreateCommand())
                {
                    cmd.CommandText = " UPDATE Exposants " +
                        " SET PersonId = @PersonId, " +
                        " EventId = @EventId, " +
                        " Name = @Name, " +
                        " Date = @Date, " +
                        " Gsm = @Gsm, " +
                        " Comments = @Comments " +
                        " Status = @Status " +
                        " WHERE PersonEventId = @id";

                    cmd.Parameters.AddWithValue("id", e.PersonEventId);
                    cmd.Parameters.AddWithValue("EventId", e.EventId);
                    cmd.Parameters.AddWithValue("PersonId", e.PersonId);
                    cmd.Parameters.AddWithValue("Name", e.Name);
                    cmd.Parameters.AddWithValue("Date", e.Date);
                    cmd.Parameters.AddWithValue("Gsm", e.Gsm);
                    cmd.Parameters.AddWithValue("Comments", e.Comments);
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
                    cmd.CommandText = " DELETE Exposants " +
                        " WHERE PersonEventId = @id";
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
