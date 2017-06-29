using DraftworxData.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using LicenseManagerAPI.Models;
using LicenseManagerAPI.Classes;
using System.Reflection;
using DraftworxData.ToolClasses;
namespace LicenseManagerAPI.Workers
{
    public class UserWorker : BaseWorker
    {
        private static string DBConnString = ConfigurationManager.ConnectionStrings["licenseManager"].ConnectionString;

        public static UserCollection GetUsers(ConnectionInformation connectionInformation)
        {
            //• RETRIEVE THE PROPERTIES OF THE CLASS
            PropertyInfo[] propertyInfos = typeof(UserClass).GetProperties();

            UserCollection result = new UserCollection();

            StringBuilder sql = new StringBuilder();
            try
            {
                sql = new StringBuilder();
                sql.Append("SELECT User_ID, Dealer_ID, Name, Email ");
                sql.Append("FROM Users ");

                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = DBConnString;

                    connection.Open();

                    using (SqlCommand command = new SqlCommand(sql.ToString(), connection))
                    {
                        IDataReader reader = command.ExecuteReader();
                        UserClass item = new UserClass();
                        var itemObject = (object)item;
                        result.User.Add((UserClass)itemObject);
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return result;
        }

        public static int AddUser(ConnectionInformation connectionInformation, UserModel user)
        {
            try
            {
                IDbCommand command = DraftworxData.BaseWorker.CreateInsertCommand(connectionInformation, typeof(UserModel).GetProperties(), user, UserModel.TableName, UserModel.ColumnsDictionary());
                return DraftworxData.BaseWorker.ExecuteScalar(connectionInformation, command, 0, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int UpdateUser(ConnectionInformation connectionInformation, UserModel user)
        {
            try
            {

                IDbCommand command = DraftworxData.BaseWorker.CreateUpdateCommand(connectionInformation, typeof(UserModel).GetProperties(), user, UserModel.TableName, UserModel.ColumnsDictionary());
                return DraftworxData.BaseWorker.ExecuteScalar(connectionInformation, command, 0, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteUser(ConnectionInformation connectionInformation, int id)
        {
            StringBuilder sqlDelete = new StringBuilder();

            try
            {
                sqlDelete.Append("DELETE FROM Users ");
                sqlDelete.Append("WHERE User_ID=@User_ID;");
                sqlDelete.Append("SELECT IDENT_CURRENT('Users');");

                SqlDataAdapter da = null;
                using (SqlConnection cn = new SqlConnection())
                {
                    try
                    {
                        da = new SqlDataAdapter();
                        da.DeleteCommand = new SqlCommand(sqlDelete.ToString(), cn);

                        da.DeleteCommand.Parameters.Add("@User_ID", SqlDbType.Int, 0).Value = id;

                        DraftworxData.BaseWorker.ExecuteCommand(connectionInformation, da.DeleteCommand, false);
                        DisposeDatabaseConnection(cn, da);
                    }
                    catch
                    {
                        DisposeDatabaseConnection(cn, da);
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}