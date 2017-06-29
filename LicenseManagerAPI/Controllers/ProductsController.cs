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
    public class ProductsController : System.Web.Http.ApiController
    {
        [HttpGet]
        [Route("products")]
        public ActionResult Products()
        {
            ConnectionInformation connectionInformation = LicenseManagerAPI.Classes.ConnectionClass;

            ProductCollection productsList = LicenseManagerAPI.Workers.ProductWorker.GetProducts(connectionInformation);
            return ViewPage(new ProductModel(productsList));
        }

        [HttpPost]
        [Route("addProduct")]
        public int addProduct([System.Web.Http.FromBody] ProductModel product)
        {
            ConnectionInformation connectionInformation = LicenseManagerAPI.Classes.ConnectionClass;
            if (product != null)
            {
                return LicenseManagerAPI.Workers.ProductWorker.AddProduct(connectionInformation, product);
            }
            else
                throw new Exception();
        }

        [HttpPut]
        [Route("updateProduct")]
        public int updateProduct([System.Web.Http.FromBody] ProductModel product)
        {
            ConnectionInformation connectionInformation = LicenseManagerAPI.Classes.ConnectionClass;
            if (product != null)
            {
                return LicenseManagerAPI.Workers.ProductWorker.UpdateProduct(connectionInformation, product);
            }
            else
                throw new Exception();
        }

        [HttpDelete]
        [Route("deleteProduct")]
        public ActionResult deleteProduct([System.Web.Http.FromBody] ProductModel product)
        {
            if (product.Product_Id != 0)
            {
                ConnectionInformation connectionInformation = LicenseManagerAPI.Classes.ConnectionClass;
                LicenseManagerAPI.Workers.ProductWorker.DeleteProduct(connectionInformation, product.Product_Id);
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            else
                throw new Exception();
        }
    }
}
