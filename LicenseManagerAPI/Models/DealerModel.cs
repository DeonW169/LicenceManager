using DraftworxDatabase.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LicenseManagerAPI.Models
{
    public class DealerModel
    {
        public int Dealer_Id { get; set; }
        public string Dealer { get; set; }
        public static string TableName { get; }

        public static Dictionary<string, TableColumnClass> ColumnsDictionary();
    }
}