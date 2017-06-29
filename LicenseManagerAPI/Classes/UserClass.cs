using DraftworxDatabase.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LicenseManagerAPI.Classes
{
    public class UserClass
    {

        #region PROPERTIES
        public int User_Id { get; set; }
        public int Dealer_Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
        #endregion

        public static string TableName
        {
            get { return "Users"; }
        }

        #region COLUMNS
        public static Dictionary<string, TableColumnClass> ColumnsDictionary()
        {
            Dictionary<string, TableColumnClass> result = new Dictionary<string, TableColumnClass>();

            result.Add("userid", new TableColumnClass("User_ID", "userID", true, true, System.Data.DbType.Int32, 0, false, false, null));
            result.Add("dealerid", new TableColumnClass("Dealer_ID", "dealerID", true, true, System.Data.DbType.Int32, 0, false, false, null));
            result.Add("name", new TableColumnClass("Name", "Name", false, false, System.Data.DbType.String, 100, true, false, null));
            result.Add("email", new TableColumnClass("Email", "Email", false, false, System.Data.DbType.String, 100, true, false, null));
            result.Add("password", new TableColumnClass("Password", "Password", false, false, System.Data.DbType.String, 100, true, false, null));
            result.Add("active", new TableColumnClass("Active", "Active", false, false, System.Data.DbType.Boolean, 1, true, false, null));

            return result;
        }
        #endregion

        #region CONSTRUCTORS
        public UserClass() { }
        #endregion
    }

    public class UserCollection
    {
        #region PROPERTIES
        public List<UserClass> User { get; set; }
        #endregion

        #region CONSTRUCTORS
        public UserCollection()
        {
            User = new List<UserClass>();
        }
        #endregion
    }
}