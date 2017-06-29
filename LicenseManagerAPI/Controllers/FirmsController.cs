using DraftworxData.Classes;
using LicenseManagerAPI.Classes;
using LicenseManagerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;

namespace LicenseManagerAPI.Controllers
{
    public class FirmsController : System.Web.Http.ApiController
    {
        [HttpGet]
        [Route("firms")]
        public ActionResult Firms()
        {
            ConnectionInformation connectionInformation = LicenseManagerAPI.Classes.ConnectionClass;

            FirmCollection firmsList = LicenseManagerAPI.Workers.FirmWorker.GetFirms(connectionInformation);
            return View(new FirmModel(firmsList));
        }

        [HttpPost]
        [Route("addFirm")]
        public int addFirm([System.Web.Http.FromBody] FirmModel firm)
        {
            ConnectionInformation connectionInformation = LicenseManagerAPI.Classes.ConnectionClass;
            if (firm != null)
            {
                return LicenseManagerAPI.Workers.FirmWorker.AddFirm(connectionInformation, firm);
            }
            else
                throw new Exception();
        }

        [HttpPut]
        [Route("updateFirm")]
        public int updateFirm([System.Web.Http.FromBody] FirmModel firm)
        {
            ConnectionInformation connectionInformation = LicenseManagerAPI.Classes.ConnectionClass;
            if (firm != null)
            {
                return LicenseManagerAPI.Workers.FirmWorker.UpdateFirm(connectionInformation, firm);
            }
            else
                throw new Exception();
        }

        [HttpDelete]
        [Route("deleteFirm")]
        public ActionResult deleteFirm([System.Web.Http.FromBody] FirmModel firm)
        {
            if (firm.FirmCode != null)
            {
                ConnectionInformation connectionInformation = LicenseManagerAPI.Classes.ConnectionClass;
                LicenseManagerAPI.Workers.FirmWorker.DeleteFirm(connectionInformation, firm.FirmCode);
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            else
                throw new Exception();
        }
    }
}
