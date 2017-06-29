using DraftworxDatabase.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LicenseManagerAPI.Classes
{
    public class ProductClass
    {

        #region PROPERTIES
        public int Product_ID { get; set; }
        public string Product { get; set; }
        #endregion

        public static string TableName
        {
            get { return "Products"; }
        }

        #region COLUMNS
        public static Dictionary<string, TableColumnClass> ColumnsDictionary()
        {
            Dictionary<string, TableColumnClass> result = new Dictionary<string, TableColumnClass>();

            result.Add("productid", new TableColumnClass("Product_ID", "ID", true, true, System.Data.DbType.Int32, 0, false, false, null));
            result.Add("product", new TableColumnClass("Product", "Product", false, false, System.Data.DbType.String, 100, true, false, null));

            return result;
        }
        #endregion

        #region CONSTRUCTORS
        public ProductClass() { }
        #endregion
    }

    public class ProductCollection
    {
        #region PROPERTIES
        public List<ProductClass> Product { get; set; }
        #endregion

        #region CONSTRUCTORS
        public ProductCollection()
        {
            Product = new List<ProductClass>();
        }
        #endregion
    }
}