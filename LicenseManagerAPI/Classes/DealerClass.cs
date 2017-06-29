using DraftworxDatabase.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LicenseManagerAPI.Classes
{
    public class DealerClass
    {

        #region PROPERTIES
        public int Dealer_ID { get; set; }
        public string Dealer { get; set; }
        #endregion

        public static string TableName
        {
            get { return "Dealers"; }
        }

        #region COLUMNS
        public static Dictionary<string, TableColumnClass> ColumnsDictionary()
        {
            Dictionary<string, TableColumnClass> result = new Dictionary<string, TableColumnClass>();

            result.Add("dealerid", new TableColumnClass("Dealer_ID", "ID", true, true, System.Data.DbType.Int32, 0, false, false, null));
            result.Add("dealer", new TableColumnClass("Dealer", "Dealer", false, false, System.Data.DbType.String, 100, true, false, null));

            return result;
        }
        #endregion

        #region CONSTRUCTORS
        public DealerClass() { }
        #endregion
    }

    public class DealerCollection
    {
        #region PROPERTIES
        public List<DealerClass> Dealer { get; set; }
        #endregion

        #region CONSTRUCTORS
        public DealerCollection()
        {
            Dealer = new List<DealerClass>();
        }
        #endregion
    }
}