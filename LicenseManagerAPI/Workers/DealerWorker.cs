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
    public class DealerWorker : BaseWorker
    {
        private static string DBConnString = ConfigurationManager.ConnectionStrings["licenseManager"].ConnectionString;

        public static DealerCollection GetDealers(ConnectionInformation connectionInformation)
        {
            //• RETRIEVE THE PROPERTIES OF THE CLASS
            PropertyInfo[] propertyInfos = typeof(DealerClass).GetProperties();

            DealerCollection result = new DealerCollection();

            StringBuilder sql = new StringBuilder();
            try
            {
                sql = new StringBuilder();
                sql.Append("SELECT Dealer_ID, Dealer ");
                sql.Append("FROM Dealers ");

                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = DBConnString;

                    connection.Open();

                    using (SqlCommand command = new SqlCommand(sql.ToString(), connection))
                    {
                        IDataReader reader = command.ExecuteReader();
                        DealerClass item = new DealerClass();
                        var itemObject = (object)item;
                        result.Dealer.Add((DealerClass)itemObject);
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return result;
        }

        public static int AddDealer(ConnectionInformation connectionInformation, DealerModel dealerModel)
        {
            try
            {
                IDbCommand command = DraftworxData.BaseWorker.CreateInsertCommand(connectionInformation, typeof(DealerModel).GetProperties(), dealerModel, DealerModel.TableName, DealerModel.ColumnsDictionary());
                return DraftworxData.BaseWorker.ExecuteScalar(connectionInformation, command, 0, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int UpdateDealer(ConnectionInformation connectionInformation, DealerModel dealerModel)
        {
            try
            {

                IDbCommand command = DraftworxData.BaseWorker.CreateUpdateCommand(connectionInformation, typeof(DealerModel).GetProperties(), dealerModel, DealerModel.TableName, DealerModel.ColumnsDictionary());
                return DraftworxData.BaseWorker.ExecuteScalar(connectionInformation, command, 0, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteDealer(ConnectionInformation connectionInformation, int id)
        {
            StringBuilder sqlDelete = new StringBuilder();

            try
            {
                sqlDelete.Append("DELETE FROM Dealers ");
                sqlDelete.Append("WHERE Dealer_ID=@Dealer_ID;");
                sqlDelete.Append("SELECT IDENT_CURRENT('Dealers');");

                SqlDataAdapter da = null;
                using (SqlConnection cn = new SqlConnection())
                {
                    try
                    {
                        da = new SqlDataAdapter();
                        da.DeleteCommand = new SqlCommand(sqlDelete.ToString(), cn);

                        da.DeleteCommand.Parameters.Add("@Dealer_ID", SqlDbType.Int, 0).Value = id;

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