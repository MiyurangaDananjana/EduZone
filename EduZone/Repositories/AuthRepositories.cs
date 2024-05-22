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
        private readonly string _connectionString;

        public AuthRepositories(string connectionString)
        {
            _connectionString = _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public IEnumerable<Product> GetProducts()
        {
            var products = new List<Product>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT Id, Name, Price FROM Products";

                using (var command = new SqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new Product
                        {
                            Id = (int)reader["Id"],
                            Name = reader["Name"].ToString(),
                            Price = (decimal)reader["Price"]
                        });
                    }
                }
            }

            return products;
        }
    }
}