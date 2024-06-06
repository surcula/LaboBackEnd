using Event_API_DAL.Models;
using Event_API_DAL.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event_API_DAL.Services
{
    public class PersonsServices : IPersonsRepo
    {

        private string _connectionString;

        public PersonsServices(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("default");
        }

        private Persons Converter(SqlDataReader reader)
        {
            return new Persons
            {
                PersonId = (int)reader["PersonId"],
                Email = (string)reader["Email"],
                FirstName = (string)reader["Firstname"],
                LastName = (string)reader["Lastname"],
                IsBanned = (bool)reader["IsBanned"],
                RoleId = (int)reader["RoleId"]
            };
        }

        public List<Persons> GetAll()
        {
            List<Persons> list = new List<Persons>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Persons";
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(Converter(reader));
                            }
                        }
                        connection.Close();
                        return list;
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



        public Persons GetById(int id)
        {
            Persons m = new Persons();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Persons WHERE PersonId = @personId";
                    command.Parameters.AddWithValue("personid", id);
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                m = Converter(reader);
                            }
                        }
                        connection.Close();
                        return m;
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

        public void Edit(Persons p)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "UPDATE Persons SET Email = @email, FirstName = @fn, LastName = @ln " +
                        "WHERE PersonId = @personId";

                    cmd.Parameters.AddWithValue("email", p.Email);
                    cmd.Parameters.AddWithValue("fn", p.FirstName);
                    cmd.Parameters.AddWithValue("ln", p.LastName);
                    cmd.Parameters.AddWithValue("personId", p.PersonId);
                    try
                    {
                        connection.Open();
                        cmd.ExecuteNonQuery();
                        connection.Close();
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
        public void IsBanned(bool isbanned, int personId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "UPDATE Persons SET IsBanned = @ib" +
                        "WHERE PersonId = @personId";

                    cmd.Parameters.AddWithValue("ib", isbanned ? 1 : 0);
                    cmd.Parameters.AddWithValue("personId", personId);
                    try
                    {
                        connection.Open();
                        cmd.ExecuteNonQuery();
                        connection.Close();
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
        public void Register(string email, string firstName, string lastName, string password)
        {
            using (SqlConnection cnx = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = cnx.CreateCommand())
                {
                    string sql = "INSERT INTO Persons (Email, FirstName, LastName, Password) " +
                        "VALUES (@email, @fn, @ln, @pwd)";
                    cmd.CommandText = sql;

                    cmd.Parameters.AddWithValue("email", email);
                    cmd.Parameters.AddWithValue("fn", firstName);
                    cmd.Parameters.AddWithValue("ln", lastName);
                    cmd.Parameters.AddWithValue("pwd", password);

                    
                    try
                    {
                        cnx.Open();
                        cmd.ExecuteNonQuery();
                        cnx.Close();
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

        public Persons Login(string email, string password)
        {
            using (SqlConnection cnx = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = cnx.CreateCommand())
                {
                    cmd.CommandText = "SELECT PersonId, FirstName, LastName, Email, RoleId, IsBanned " +
                        "FROM Persons WHERE Email = @email AND Password = @pwd";

                    try
                    {
                        cnx.Open();

                        cmd.Parameters.AddWithValue("email", email);
                        cmd.Parameters.AddWithValue("pwd", password);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return Converter(reader);
                            }
                            else throw new InvalidOperationException("Compte utilisateur inexistant");
                        }
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

        public string GetHashPwd(string email)
        {
            using (SqlConnection cnx = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = cnx.CreateCommand())
                {
                    cmd.CommandText = "SELECT Password " +
                        "FROM Persons WHERE Email = @email";

                    try
                    {
                        cnx.Open();

                        cmd.Parameters.AddWithValue("email", email);
                        string pwd = (string)cmd.ExecuteScalar();
                        cnx.Close();
                        return pwd;
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
        public void EditStatus(int id, int status)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "UPDATE Persons " +
                        "SET RoleId = @status WHERE PersonId = @personId";

                    cmd.Parameters.AddWithValue("status", status);
                    cmd.Parameters.AddWithValue("personId", id);
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
