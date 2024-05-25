using EduZone.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

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

        public void DeleteUser(int id)
        {
            // Create a DbConnection instance with the connection string
            DbConnection dbConnection = new DbConnection();

            // Prepare the SQL query
            string query = "DELETE FROM [USER_DETAILS] WHERE USER_ID = @UserId";

            // Set up parameters for the query
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@UserId", id)
            };

            // Execute the query
            dbConnection.ExecuteDeleteQuery(query, parameters);
        }

        internal RegisterModel GetUserById(int id)
        {
            DbConnection dbConnection = new DbConnection();
            string query = "SELECT USER_ID, FIRST_NAME, LAST_NAME, EMAIL, ACC_ROLE, PASSWORD, PROFILE_IMG, CREATE_DATETIME " +
                           "FROM [USER_DETAILS] WHERE USER_ID = @UserId";

            var parameters = new SqlParameter[]
            {
        new SqlParameter("@UserId", id)
            };

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
                    Password = row["PASSWORD"].ToString(),
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

        public bool UpdateUser(RegisterModel user)
        {
            DbConnection dbConnection = new DbConnection();
            string query = "UPDATE [USER_DETAILS] SET FIRST_NAME = @FirstName, LAST_NAME = @LastName, EMAIL = @Email, " +
                           "ACC_ROLE = @AccRole, PASSWORD = @Password, PROFILE_IMG = @ProfileImg WHERE USER_ID = @UserId";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@FirstName", user.FirstName),
                new SqlParameter("@LastName", user.LastName),
                new SqlParameter("@Email", user.Email),
                new SqlParameter("@AccRole", user.AccRole),
                new SqlParameter("@Password", user.Password),
                new SqlParameter("@ProfileImg", user.ProfilePath),
                new SqlParameter("@UserId", user.UserId)
            };

            try
            {
                dbConnection.ExecuteUpdateQuery(query, parameters);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}