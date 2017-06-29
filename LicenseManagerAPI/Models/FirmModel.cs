using DraftworxDatabase.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LicenseManagerAPI.Models
{
    public class FirmModel
    {
        public string FirmGUID { get; set; }
        public string FirmCode { get; set; }
        public string Firm { get; set; }
        public static string TableName { get; }

        public static Dictionary<string, TableColumnClass> ColumnsDictionary();
    }
}