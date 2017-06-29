using DraftworxDatabase.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LicenseManagerAPI.Models
{
    public class ProductModel
    {
        public int Product_Id { get; set; }
        public string Product { get; set; }
        public static string TableName { get; }

        public static Dictionary<string, TableColumnClass> ColumnsDictionary();
    }
}