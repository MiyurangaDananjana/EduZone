using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace EduZone.Repositories
{
    public class DbConnection
    {
        // Hardcoded connection string
        private string connectionString = "Data Source=HQ-IT-PC36\\SQLEXPRESS;Initial Catalog=EduZone;Integrated Security=True;Encrypt=False";

        public DataTable ExecuteQuery(string sqlQuery)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                DataTable dataTable = new DataTable();
                dataTable.Load(command.ExecuteReader());
                return dataTable;
            }
        }

        public void ExecuteInsertQuery(string sqlQuery, SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddRange(parameters);
                command.ExecuteNonQuery();
            }
        }

        public void ExecuteUpdateQuery(string sqlQuery, SqlParameter[] parameters)
        {
            ExecuteInsertQuery(sqlQuery, parameters);
        }

        public void ExecuteDeleteQuery(string sqlQuery, SqlParameter[] parameters)
        {
            ExecuteInsertQuery(sqlQuery, parameters);
        }

    }
}