using DraftworxDatabase.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LicenseManagerAPI.Classes
{
    public class FirmClass
    {
        #region PROPERTIES
        public string FirmGUID { get; set; }
        public string FirmCode { get; set; }
        public string Firm { get; set; }
        #endregion

        public static string TableName
        {
            get { return "Firms"; }
        }

        #region COLUMNS
        public static Dictionary<string, TableColumnClass> ColumnsDictionary()
        {
            Dictionary<string, TableColumnClass> result = new Dictionary<string, TableColumnClass>();

            result.Add("firmguid", new TableColumnClass("FirmGUID", "FirmGUID", false, false, System.Data.DbType.String, 100, true, false, null));
            result.Add("firmcode", new TableColumnClass("FirmCode", "FirmCode", false, false, System.Data.DbType.String, 100, true, false, null));
            result.Add("firm", new TableColumnClass("Firm", "Firm", false, false, System.Data.DbType.String, 100, true, false, null));

            return result;
        }
        #endregion

        #region CONSTRUCTORS
        public FirmClass() { }
        #endregion
    }

    public class FirmCollection
    {
        #region PROPERTIES
        public List<FirmClass> Firm { get; set; }
        #endregion

        #region CONSTRUCTORS
        public FirmCollection()
        {
            Firm = new List<FirmClass>();
        }
        #endregion
    }
}