using DraftworxDatabase.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LicenseManagerAPI.Models
{
    public class UserModel
    {
        public int User_Id { get; set; }
        public int Dealer_Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
        public static string TableName { get; }

        public static Dictionary<string, TableColumnClass> ColumnsDictionary();
    }
}