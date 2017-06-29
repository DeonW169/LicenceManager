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
    public class ProductWorker : BaseWorker
    {
        private static string DBConnString = ConfigurationManager.ConnectionStrings["licenseManager"].ConnectionString;

        public static ProductCollection GetProducts(ConnectionInformation connectionInformation)
        {
            //• RETRIEVE THE PROPERTIES OF THE CLASS
            PropertyInfo[] propertyInfos = typeof(ProductClass).GetProperties();

            ProductCollection result = new ProductCollection();

            StringBuilder sql = new StringBuilder();
            try
            {
                sql = new StringBuilder();
                sql.Append("SELECT Product_ID, Product ");
                sql.Append("FROM Products ");

                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = DBConnString;

                    connection.Open();

                    using (SqlCommand command = new SqlCommand(sql.ToString(), connection))
                    {
                        IDataReader reader = command.ExecuteReader();
                        ProductClass item = new ProductClass();
                        var itemObject = (object)item;
                        result.Product.Add((ProductClass)itemObject);
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return result;
        }

        public static int AddProduct(ConnectionInformation connectionInformation, ProductModel product)
        {
            try
            {
                IDbCommand command = DraftworxData.BaseWorker.CreateInsertCommand(connectionInformation, typeof(ProductModel).GetProperties(), product, ProductModel.TableName, ProductModel.ColumnsDictionary());
                return DraftworxData.BaseWorker.ExecuteScalar(connectionInformation, command, 0, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int UpdateProduct(ConnectionInformation connectionInformation, ProductModel product)
        {
            try
            {

                IDbCommand command = DraftworxData.BaseWorker.CreateUpdateCommand(connectionInformation, typeof(ProductModel).GetProperties(), product, ProductModel.TableName, ProductModel.ColumnsDictionary());
                return DraftworxData.BaseWorker.ExecuteScalar(connectionInformation, command, 0, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteProduct(ConnectionInformation connectionInformation, int id)
        {
            StringBuilder sqlDelete = new StringBuilder();

            try
            {
                sqlDelete.Append("DELETE FROM Products ");
                sqlDelete.Append("WHERE Product_ID=@Product_ID;");
                sqlDelete.Append("SELECT IDENT_CURRENT('Products');");

                SqlDataAdapter da = null;
                using (SqlConnection cn = new SqlConnection())
                {
                    try
                    {
                        da = new SqlDataAdapter();
                        da.DeleteCommand = new SqlCommand(sqlDelete.ToString(), cn);

                        da.DeleteCommand.Parameters.Add("@Product_ID", SqlDbType.Int, 0).Value = id;

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