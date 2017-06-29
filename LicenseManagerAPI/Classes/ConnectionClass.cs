using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LicenseManagerAPI.Classes
{
    public class ConnectionClass
    {
        private SqlConnection connection;
        private SqlCommand command;
        private static string sqlHostAddress = "DESKTOP-91UQLPO\\SQLEXPRESS";
        private static string sqlDatabaseName = "LicenseServer";
        private static string sqlUser = "testuser";
        private static string sqlPassword = "testuser";
        private string connectionString = "Data Source=" + sqlHostAddress + ";Initial Catalog = " + sqlDatabaseName + "; User ID = " + sqlUser + "; Password=" + sqlPassword + ";";

        public ConnectionClass()
        {
            connection = new SqlConnection(connectionString);
            command = new SqlCommand();
            command.Connection = connection;
        }

        public SqlDataReader ExecuteReader(string query)
        {
            SqlDataReader result = null;
            command.CommandText = query;
            try
            {
                connection.Open();
                result = command.ExecuteReader();
            }
            catch (Exception Ex)
            {
                System.Diagnostics.Debug.Print(Ex.Message);
            }

            return result;
        }

        public void ExecuteQuery(string Query)
        {
            command.CommandText = Query;
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                System.Diagnostics.Debug.Print(Ex.Message);
            }
        }

        public void CloseConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }
    }
}