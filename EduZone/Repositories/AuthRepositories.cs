using EduZone.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EduZone.Repositories
{
    public class AuthRepositories
    {
        public void RegisterUser(RegisterModel register)
        {
            // Create a DbConnection instance with the connection string
            DbConnection dbConnection = new DbConnection();

            // Prepare the SQL query
            string query = "INSERT INTO [Table] (Name, Password, Email) VALUES (@Name, @Password, @Email)";

            // Set up parameters for the query
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Email", register.Email),
                new SqlParameter("@Password", register.Password),
                new SqlParameter("@Name", register.Name)
            };

            // Execute the query
            dbConnection.ExecuteInsertQuery(query, parameters);
        }
    }
}