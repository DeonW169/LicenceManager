using DraftworxData.Classes;
using LicenseManagerAPI.Classes;
using LicenseManagerAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace LicenseManagerAPI.Workers
{
    public class FirmWorker : BaseWorker
    {
        private static string DBConnString = ConfigurationManager.ConnectionStrings["licenseManager"].ConnectionString;

        public static FirmCollection GetFirms(ConnectionInformation connectionInformation)
        {
            //• RETRIEVE THE PROPERTIES OF THE CLASS
            PropertyInfo[] propertyInfos = typeof(FirmClass).GetProperties();

            FirmCollection result = new FirmCollection();

            StringBuilder sql = new StringBuilder();
            try
            {
                sql = new StringBuilder();
                sql.Append("SELECT FirmGUID, FirmCode, Firm ");
                sql.Append("FROM Firms ");

                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = DBConnString;

                    connection.Open();

                    using (SqlCommand command = new SqlCommand(sql.ToString(), connection))
                    {
                        IDataReader reader = command.ExecuteReader();
                        FirmClass item = new FirmClass();
                        var itemObject = (object)item;
                        result.Firm.Add((FirmClass)itemObject);
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return result;
        }

        public static int AddFirm(ConnectionInformation connectionInformation, FirmModel firm)
        {
            try
            {
                IDbCommand command = DraftworxData.BaseWorker.CreateInsertCommand(connectionInformation, typeof(FirmModel).GetProperties(), firm, FirmModel.TableName, FirmModel.ColumnsDictionary());
                return DraftworxData.BaseWorker.ExecuteScalar(connectionInformation, command, 0, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int UpdateFirm(ConnectionInformation connectionInformation, FirmModel firm)
        {
            try
            {
                IDbCommand command = DraftworxData.BaseWorker.CreateUpdateCommand(connectionInformation, typeof(FirmModel).GetProperties(), firm, FirmModel.TableName, FirmModel.ColumnsDictionary());
                return DraftworxData.BaseWorker.ExecuteScalar(connectionInformation, command, 0, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteFirm(ConnectionInformation connectionInformation, string code)
        {
            StringBuilder sqlDelete = new StringBuilder();

            try
            {
                sqlDelete.Append("DELETE FROM Firms ");
                sqlDelete.Append("WHERE FirmCode=@FirmCode;");
                sqlDelete.Append("SELECT IDENT_CURRENT('Firms');");

                SqlDataAdapter da = null;
                using (SqlConnection cn = new SqlConnection())
                {
                    try
                    {
                        da = new SqlDataAdapter();
                        da.DeleteCommand = new SqlCommand(sqlDelete.ToString(), cn);

                        da.DeleteCommand.Parameters.Add("@FirmCode", SqlDbType.Int, 0).Value = code;

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