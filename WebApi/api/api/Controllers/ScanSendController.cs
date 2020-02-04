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
    public class ScanSendController : ApiController
    {
        IScanSendService sendSvc;

        public ScanSendController()
        {
            sendSvc = new ScanSendService();
        }

        [Route("scan-send/postSearchSpring")]
        public HttpResponseMessage postSearchSpring(ScanSendSearchView model)
        {
            try
            {
                var result = sendSvc.SearchSpringData(model);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }
    }
}
