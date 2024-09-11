using System.Collections.Generic;
using System.Data;
using System;
using EduZone.Models;
using System.Data.SqlClient;

namespace EduZone.Repositories
{
    public class DashboardRepositories
    {
        public List<UserModel> GetAllUserDetails()
        {
            List<UserModel> userDetails = new List<UserModel>();
            DbConnection dbConnection = new DbConnection();

            // Prepare the SQL query
            string query = "SELECT [USER_ID], [FIRST_NAME], [LAST_NAME], [EMAIL], [ACC_ROLE], [PASSWORD], [PROFILE_IMG], [CREATE_DATETIME] FROM [USER_DETAILS]";

            // Execute the query
            var dataTable = dbConnection.ExecuteQuery(query);

            // Process each row in the result set
            foreach (DataRow row in dataTable.Rows)
            {
                UserModel user = new UserModel
                {
                    UserId = Convert.ToInt32(row["USER_ID"]),
                    FirstName = row["FIRST_NAME"].ToString(),
                    LastName = row["LAST_NAME"].ToString(),
                    Email = row["EMAIL"].ToString(),
                    AccRole = Convert.ToInt32(row["ACC_ROLE"]),
                    Password = row["PASSWORD"].ToString(),
                    ProfileImg = row["PROFILE_IMG"].ToString(),
                    CreateDateTime = Convert.ToDateTime(row["CREATE_DATETIME"])
                };
                userDetails.Add(user);
            }

            return userDetails;
        }

        public List<BlogModel> GetAllBlogPostsWithUsers()
        {
            List<BlogModel> blogPosts = new List<BlogModel>();

            DbConnection dbConnection = new DbConnection();
            string query = @"
        SELECT 
            p.[POST_ID], p.[TITLE], p.[CONTENT], p.[STATUS], p.[CREATEDATETIME], p.[CREATE_BY], p.[IMG],
            u.[FIRST_NAME], u.[LAST_NAME], u.[EMAIL], u.[ACC_ROLE], u.[PROFILE_IMG], u.[CREATE_DATETIME]
        FROM 
            [POST] p
        JOIN 
            [USER_DETAILS] u ON p.[CREATE_BY] = u.[USER_ID]";

            // Execute the query
            var dataTable = dbConnection.ExecuteQuery(query);

            // Process each row in the result set
            foreach (DataRow row in dataTable.Rows)
            {
                BlogModel blogPost = new BlogModel
                {
                    Id = Convert.ToInt32(row["POST_ID"]),
                    Title = row["TITLE"].ToString(),
                    Description = row["CONTENT"].ToString(),
                    Status = Convert.ToInt32(row["STATUS"]),
                    CreateDate = Convert.ToDateTime(row["CREATEDATETIME"]),
                    CreateBy = Convert.ToInt32(row["CREATE_BY"]),
                    ImgName = row["IMG"].ToString(),
                    CreateUser = row["FIRST_NAME"].ToString() + " " + row["LAST_NAME"].ToString(),
                    UserRole = Convert.ToInt32(row["ACC_ROLE"])
                };
                blogPosts.Add(blogPost);
            }

            return blogPosts;
        }

        internal int GetTotalUserCount()
        {
            int userCount = 0;

            DbConnection dbConnection = new DbConnection();

            string query = "SELECT COUNT(*) FROM [USER_DETAILS]";

            var dataTable = dbConnection.ExecuteQuery(query);

            if (dataTable.Rows.Count > 0)
            {
                userCount = Convert.ToInt32(dataTable.Rows[0][0]);
            }

            return userCount;
        }

        internal int GetTotalBlogs()
        {
            int blogCount = 0;

            DbConnection dbConnection = new DbConnection();

            string query = "SELECT COUNT(*) FROM [POST]";

            var dataTable = dbConnection.ExecuteQuery(query);

            if (dataTable.Rows.Count > 0)
            {
                blogCount = Convert.ToInt32(dataTable.Rows[0][0]);
            }

            return blogCount;
        }
    }
}