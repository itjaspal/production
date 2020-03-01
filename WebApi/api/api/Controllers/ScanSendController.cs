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

        [Route("scan-send/postUpdatePcs")]
        public HttpResponseMessage postScanPcs(DataEntrySearchView model)
        {
            try
            {
                

                sendSvc.UpdatePcs(model);

                return Request.CreateResponse(HttpStatusCode.OK, "บันทึกข้อมูลสำเร็จ");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }

        [Route("scan-send/postCancelPcs")]
        public HttpResponseMessage postCancelPcs(DataEntrySearchView model)
        {
            try
            {
                sendSvc.CancelPcs(model);

                return Request.CreateResponse(HttpStatusCode.OK, "ยกเลิกข้อมูลสำเร็จ");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }

    
        [Route("scan-send/postSearchScanPcs")]
        public HttpResponseMessage postSearchScanPcs(ScanPcsSearchView model)
        {
            try
            {
                var result = sendSvc.SearchScanPcs(model);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }

        [Route("scan-send/postSearchScanCancelPcs")]
        public HttpResponseMessage postSearchScanCancelPcs(ScanPcsSearchView model)
        {
            try
            {
                var result = sendSvc.SearchScanCanclePcs(model);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }


        [Route("scan-send/postSerachFinPcs")]
        public HttpResponseMessage postSerachFinPcs(ScanSendFinSearchView model)
        {
            try
            {
                var result = sendSvc.SerachFinPcs(model);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }


        [Route("scan-send/postSerachCanPcs")]
        public HttpResponseMessage postSerachCanPcs(ScanSendFinSearchView model)
        {
            try
            {
                var result = sendSvc.SerachCanPcs(model);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }


    }
}
