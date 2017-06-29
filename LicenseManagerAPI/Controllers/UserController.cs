using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using LicenseManagerAPI.Models;
using LicenseManagerAPI.Controllers;
using DraftworxData.Classes;
using System.Web.Mvc;
using LicenseManagerAPI.Classes;
namespace LicenseManagerAPI.Controllers
{
    public class UserController : System.Web.Http.ApiController
    {
        [HttpGet]
        [Route("users")]
        public ActionResult Users()
        {
            ConnectionInformation connectionInformation = LicenseManagerAPI.Classes.ConnectionClass;

            UserCollection usersList = LicenseManagerAPI.Workers.UserWorker.GetUsers(connectionInformation);
            return ViewPage(new UserModel(usersList));
        }

        [HttpPost]
        [Route("addUser")]
        public int addUser([System.Web.Http.FromBody] UserModel user)
        {
            ConnectionInformation connectionInformation = LicenseManagerAPI.Classes.ConnectionClass;
            if (user != null)
            {
                return LicenseManagerAPI.Workers.UserWorker.AddUser(connectionInformation, user);
            }
            else
                throw new Exception();
        }

        [HttpPut]
        [Route("updateUser")]
        public int updateUser([System.Web.Http.FromBody] UserModel user)
        {
            ConnectionInformation connectionInformation = LicenseManagerAPI.Classes.ConnectionClass;
            if (user != null)
            {
                return LicenseManagerAPI.Workers.UserWorker.UpdateUser(connectionInformation, user);
            }
            else
                throw new Exception();
        }

        [HttpDelete]
        [Route("deleteUser")]
        public ActionResult deleteUser([System.Web.Http.FromBody] UserModel user)
        {
            if (user.User_Id != 0)
            {
                ConnectionInformation connectionInformation = LicenseManagerAPI.Classes.ConnectionClass;
                LicenseManagerAPI.Workers.UserWorker.DeleteUser(connectionInformation, user.User_Id);
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            else
                throw new Exception();
        }
    }
}
