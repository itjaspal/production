using api.Interfaces;
using api.ModelViews;
using api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace api.Controllers
{
    public class StockController : ApiController
    {
        IStockService stockSvc;

        public StockController()
        {
            stockSvc = new StockService();
        }


        [Route("stock/postSearch")]

        public HttpResponseMessage postSearch(StockSearchView model)
        {
            try
            {
                var result = stockSvc.Search(model);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }


        }

        [Route("stock/postSearchpcs")]

        public HttpResponseMessage postSearchpcs(StockSearchView model)
        {
            try
            {
                var result = stockSvc.Search(model);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }


        }
    }
}
