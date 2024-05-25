using System;
using System.Data.SqlClient;
using System.Data;

namespace EduZone.Repositories
{
    public class DbConnection
    {
        // Hardcoded connection string
        private string connectionString = "Data Source=MiYuranga;Initial Catalog=EduZone;Integrated Security=True;Encrypt=False";

        public DataTable ExecuteQuery(string sqlQuery, SqlParameter[] parameters = null)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

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

        public bool CheckConnection()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    connection.Close();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}