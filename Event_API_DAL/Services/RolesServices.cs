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
    public class RolesServices : IRolesRepo
    {
        private string _connectionString;

        public RolesServices(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("default");
        }
        private Roles Converter(SqlDataReader reader)
        {
            return new Roles
            {
                RoleId = (int)reader["RoleId"],
                Role = (string)reader["Role"]
            };
        }
        public List<Roles> GetAll()
        {
            List<Roles> list = new List<Roles>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Roles";
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
        public Roles GetById(int id)
        {
            Roles r = new Roles();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Roles WHERE RoleId = @roleId";
                    command.Parameters.AddWithValue("roleId", id);
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                r = Converter(reader);
                            }
                        }
                        connection.Close();
                        return r;
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
        public void Create(Roles role)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Roles (Role) " + "VALUES (@role)";                 
                    cmd.Parameters.AddWithValue("role", role.Role);
                    
                    
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
        public void Edit(Roles r)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "UPDATE Roles SET Role = @role WHERE RoleId = @roleId";
                    cmd.Parameters.AddWithValue("role", r.Role);
                    cmd.Parameters.AddWithValue("roleId", r.RoleId);
                    
                    
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
    }
}
