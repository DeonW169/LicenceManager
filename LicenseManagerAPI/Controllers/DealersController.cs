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
    public class DealersController : System.Web.Http.ApiController
    {
        [HttpGet]
        [Route("dealers")]
        public ActionResult Dealers()
        {
            ConnectionInformation connectionInformation = LicenseManagerAPI.Classes.ConnectionClass;

            DealerCollection dealersList = LicenseManagerAPI.Workers.DealerWorker.GetDealers(connectionInformation);
            return ViewPage(new DealerModel(dealersList));
        }

        [HttpPost]
        [Route("addDealer")]
        public int addDealer([System.Web.Http.FromBody] DealerModel dealerModel)
        {
            ConnectionInformation connectionInformation = LicenseManagerAPI.Classes.ConnectionClass;
            if (dealerModel != null)
            {
                return LicenseManagerAPI.Workers.DealerWorker.AddDealer(connectionInformation, dealerModel);
            }
            else
                throw new Exception();
        }

        [HttpPut]
        [Route("updateDealer")]
        public int updateDealer([System.Web.Http.FromBody] DealerModel dealerModel)
        {
            ConnectionInformation connectionInformation = LicenseManagerAPI.Classes.ConnectionClass;
            if (dealerModel != null)
            {
                return LicenseManagerAPI.Workers.DealerWorker.UpdateDealer(connectionInformation, dealerModel);
            }
            else
                throw new Exception();
        }

        [HttpDelete]
        [Route("deleteDealer")]
        public ActionResult deleteDealer([System.Web.Http.FromBody] DealerModel dealerModel)
        {
            if (dealerModel.Dealer_Id != 0)
            {
                ConnectionInformation connectionInformation = LicenseManagerAPI.Classes.ConnectionClass;
                LicenseManagerAPI.Workers.DealerWorker.DeleteDealer(connectionInformation, dealerModel.Dealer_Id);
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            else
                throw new Exception();
        }
    }
}
