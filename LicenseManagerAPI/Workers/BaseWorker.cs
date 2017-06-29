using DraftworxData.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace LicenseManagerAPI.Workers
{
    public class BaseWorker
    {
        public static void DisposeDatabaseConnection(SqlConnection cn, SqlDataAdapter da)
        {
            if (da != null)
            {
                if (da.SelectCommand != null)
                    da.SelectCommand.Dispose();

                if (da.InsertCommand != null)
                    da.InsertCommand.Dispose();

                if (da.DeleteCommand != null)
                    da.DeleteCommand.Dispose();

                if (da.UpdateCommand != null)
                    da.UpdateCommand.Dispose();

                da.Dispose();

                cn.Close();
                cn.Dispose();
            }
        }

        public static int ExecuteCommand(ConnectionInformation connectionInformation, IDbCommand command)
        {
            string connectionString = string.Empty;
            int result = 0;

            using (SqlConnection connection = GetSQLServerConnection(connectionInformation))
            {
                try
                {
                    connection.Open();
                    command.Connection = connection;
                    result = command.ExecuteNonQuery();
                    command.Dispose();
                    connection.Close();
                    connection.Dispose();
                }
                catch (SqlException ex)
                {
                    string commandText = command.CommandText;
                    command.Dispose();
                    connection.Close();
                    connection.Dispose();
                }
            }
            return result;
        }

        public static SqlConnection GetSQLServerConnection(ConnectionInformation connectionInformation, bool allowEmptyDatabaseName = false)
        {
            SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder();

            bool complete = true;

            if (String.IsNullOrEmpty(connectionInformation.Server))
                complete = false;

            if (!allowEmptyDatabaseName && String.IsNullOrEmpty(connectionInformation.Database))
                complete = false;

            if (!connectionInformation.Trusted)
            {
                if (String.IsNullOrEmpty(connectionInformation.User))
                    complete = false;

                if (string.IsNullOrEmpty(connectionInformation.Password))
                    complete = false;
            }

            if (complete)
            {
                stringBuilder.DataSource = connectionInformation.Server;
                stringBuilder.InitialCatalog = connectionInformation.Database;
                stringBuilder.ConnectTimeout = 60;

                if (connectionInformation.Trusted)
                {
                    stringBuilder.IntegratedSecurity = true;
                }
                else
                {
                    stringBuilder.IntegratedSecurity = false;
                    stringBuilder.UserID = connectionInformation.User;
                    stringBuilder.Password = connectionInformation.Password;

                }

                return new SqlConnection(stringBuilder.ConnectionString.ToString());
            }
            else
            {
                Exception ex = new Exception("Connection Information Incomplete");
                throw ex;
            }
        }
    }
}