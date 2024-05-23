using EduZone.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace EduZone.Repositories
{
    public class AuthRepositories
    {
        public void RegisterUser(RegisterModel register)
        {
            // Create a DbConnection instance with the connection string
            DbConnection dbConnection = new DbConnection();

            DateTime createDateTime = DateTime.Now;

            // Prepare the SQL query
            string query = "INSERT INTO [USER_DETAILS] (FIRST_NAME, LAST_NAME, EMAIL,ACC_ROLE,PASSWORD,PROFILE_IMG,CREATE_DATETIME) VALUES (@firstName, @lastName, @Email,@accRole,@Pass,@img,@CreateDateTime)";

            // Set up parameters for the query
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@firstName", register.FirstName),
                new SqlParameter("@lastName", register.LastName),
                new SqlParameter("@Email", register.Email),
                new SqlParameter("@accRole", register.AccRole),
                new SqlParameter("@Pass", register.Password),
                new SqlParameter("@img", register.ProfilePath),
                new SqlParameter("@CreateDateTime", createDateTime)
            };

            // Execute the query
            dbConnection.ExecuteInsertQuery(query, parameters);
        }

        public bool CheckDbConnection()
        {
            DbConnection dbConnection = new DbConnection();
            return dbConnection.CheckConnection();
        }


        public RegisterModel GetUserByEmailAndPassword(string email, string password)
        {
            // Create a DbConnection instance with the connection string
            DbConnection dbConnection = new DbConnection();

            // Prepare the SQL query
            string query = "SELECT USER_ID, FIRST_NAME, LAST_NAME, EMAIL, ACC_ROLE, PROFILE_IMG, CREATE_DATETIME " +
                           "FROM [USER_DETAILS] WHERE EMAIL = @Email AND PASSWORD = @Password";

            // Set up parameters for the query
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Email", email),
                new SqlParameter("@Password", password)
            };

            // Execute the query and get the result
            DataTable dataTable = dbConnection.ExecuteQuery(query, parameters);

            if (dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[0];
                RegisterModel user = new RegisterModel
                {
                    UserId = Convert.ToInt32(row["USER_ID"]),
                    FirstName = row["FIRST_NAME"].ToString(),
                    LastName = row["LAST_NAME"].ToString(),
                    Email = row["EMAIL"].ToString(),
                    AccRole = Convert.ToInt32(row["ACC_ROLE"]),
                    ProfilePath = row["PROFILE_IMG"].ToString(),
                    CreateDateTime = Convert.ToDateTime(row["CREATE_DATETIME"])
                };
                return user;
            }
            else
            {
                return null; // No user found
            }
        }

    }
}