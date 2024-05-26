using EduZone.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace EduZone.Repositories
{
    public class BlogRepositories
    {
        internal void CreateNewBlogPost(BlogModel blog)
        {
            // Create a DbConnection instance with the connection string
            DbConnection dbConnection = new DbConnection();

            DateTime createDateTime = DateTime.Now;

            // Prepare the SQL query
            string query = "INSERT INTO [POST] ([TITLE], [CONTENT], [STATUS],[CREATEDATETIME],[CREATE_BY],[IMG]) VALUES (@title, @content, @status,@createDateTime,@CreateBy,@img)";

            // Set up parameters for the query
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@title", blog.Title),
                new SqlParameter("@content", blog.Description),
                new SqlParameter("@status", blog.Status),
                new SqlParameter("@createDateTime", blog.CreateDate),
                new SqlParameter("@CreateBy", blog.CreateBy),
                new SqlParameter("@img", blog.ImgName)
            };

            // Execute the query
            dbConnection.ExecuteInsertQuery(query, parameters);


        }

        public List<BlogModel> GetUserBlogPosts(int userId)
        {
            List<BlogModel> blogPosts = new List<BlogModel>();
            DbConnection dbConnection = new DbConnection();

            string query = "SELECT [POST_ID], [TITLE], [CONTENT], [STATUS], [CREATEDATETIME], [CREATE_BY], [IMG] FROM [POST] WHERE [CREATE_BY] = @userId";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@userId", userId)
            };

            var dataTable = dbConnection.ExecuteQuery(query, parameters);

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
                    ImgName = row["IMG"].ToString()
                };
                blogPosts.Add(blogPost);
            }

            return blogPosts;
        }

        public List<BlogModel> GetAllUsersBlogPosts()
        {
            List<BlogModel> blogPosts = new List<BlogModel>();
            DbConnection dbConnection = new DbConnection();

            // Prepare the SQL query
            string query = "SELECT [POST_ID],[TITLE], [CONTENT], [STATUS], [CREATEDATETIME], [CREATE_BY], [IMG] FROM [POST]";

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
                    ImgName = row["IMG"].ToString()
                };
                blogPosts.Add(blogPost);
            }

            return blogPosts;
        }

        public void DeleteBlog(int id)
        {
            DbConnection dbConnection = new DbConnection();

            string query = "DELETE FROM [POST] WHERE [POST_ID] = @id";

            var parameters = new SqlParameter[]
            {
                 new SqlParameter("@id", id)
            };
            dbConnection.ExecuteDeleteQuery(query, parameters);
        }

        public BlogModel GetBlogById(int id)
        {
            BlogModel blogPost = new BlogModel();
            DbConnection dbConnection = new DbConnection();

            string query = "SELECT [POST_ID],[TITLE], [CONTENT], [STATUS], [CREATEDATETIME], [CREATE_BY], [IMG] FROM [POST] WHERE [POST_ID] = @id";

            var parameters = new SqlParameter[]
            {
        new SqlParameter("@id", id)
            };

            var dataTable = dbConnection.ExecuteQuery(query, parameters);

            if (dataTable.Rows.Count == 1)
            {
                var row = dataTable.Rows[0];
                blogPost.Id = Convert.ToInt32(row["POST_ID"]);
                blogPost.Title = row["TITLE"].ToString();
                blogPost.Description = row["CONTENT"].ToString();
                blogPost.Status = Convert.ToInt32(row["STATUS"]);
                blogPost.CreateDate = Convert.ToDateTime(row["CREATEDATETIME"]);
                blogPost.CreateBy = Convert.ToInt32(row["CREATE_BY"]);
                blogPost.ImgName = row["IMG"].ToString();
            }

            return blogPost;
        }

        public void UpdateBlog(BlogModel blog)
        {
            DbConnection dbConnection = new DbConnection();

            string query = "UPDATE [POST] SET [TITLE] = @title, [CONTENT] = @content, [IMG] = @img WHERE [POST_ID] = @id";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@title", blog.Title),
                new SqlParameter("@content", blog.Description),
                new SqlParameter("@img", blog.ImgName),
            };

            dbConnection.ExecuteUpdateQuery(query, parameters);
        }

        internal List<BlogModel> GetAllBlogPosts()
        {
            List<BlogModel> blogPosts = new List<BlogModel>();
            DbConnection dbConnection = new DbConnection();

            // Updated query to remove the WHERE clause
            string query = "SELECT [POST_ID], [TITLE], [CONTENT], [STATUS], [CREATEDATETIME], [CREATE_BY], [IMG] FROM [POST]";

            // Execute the query without any parameters
            var dataTable = dbConnection.ExecuteQuery(query);

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
                    ImgName = row["IMG"].ToString()
                };
                blogPosts.Add(blogPost);
            }

            return blogPosts;
        }
    }
}