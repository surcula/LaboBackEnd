using Event_API_DAL.Repository;
using Event_API_Domain.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Event_API_DAL.Services
{
    public class CommentsService : ICommentsRepo
    {
        private string connectionString;
        public CommentsService(IConfiguration config)
        {
            connectionString = config.GetConnectionString("default");
        }
        private Comments Converter(SqlDataReader reader)
        {
            return new Comments
            {
                CommentId = (int)reader["CommentId"],
                Title = (string)reader["Title"],
                Date = (DateTime)reader["Date"],
                Message = (string)reader["Message"],
                EventId = (int)reader["EventId"],
                PersonId = (int)reader["PersonId"]
            };
        }

        public List<Comments> GetAllByEventId(int id)
        {

            List<Comments> comments = new List<Comments>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Comments WHERE EventId = @eventId";
                    command.Parameters.AddWithValue("eventId", id);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                comments.Add(Converter(reader));
                            }
                        }
                        connection.Close();
                        return comments;
                    }
                    catch (SqlException ex) { throw ex; }
                    catch (Exception ex) { throw ex; }


                }
            }
        }
        public void Create(Comments comments)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "INSERT INTO Comments (Title, Date, Message, EventId, PersonId) VALUES (@title, @date, @message, @eventId, @personId)";
                    cmd.Parameters.AddWithValue("title", comments.Title);
                    cmd.Parameters.AddWithValue("date", comments.Date);
                    cmd.Parameters.AddWithValue("message", comments.Message);
                    cmd.Parameters.AddWithValue("eventId", comments.EventId);
                    cmd.Parameters.AddWithValue("personId", comments.PersonId);

                    try
                    {
                        connection.Open();
                        cmd.ExecuteNonQuery();
                        connection.Close();

                    }
                    catch (SqlException ex) { throw ex; }
                    catch (Exception ex) { throw ex; }
                }
            }
        }
        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE FROM Comments WHERE CommentId = @commentId";
                    cmd.Parameters.AddWithValue("commentId", id);
                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
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

