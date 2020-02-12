﻿using api.Interfaces;
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
    public class JobInProcessController : ApiController
    {
        IJobInProcessService inprocSvc;

        public JobInProcessController()
        {
            inprocSvc = new JobInProcessService();
        }

        [Route("job-inprocess/postSearchPcs")]
        public HttpResponseMessage postSearchPcs(JobInProcessSearchView model)
        {
            try
            {
                var result = inprocSvc.SearchPcs(model);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [Route("job-inprocess/postSearchScanPcs")]
        public HttpResponseMessage postSearchScanPcs(JobInProcessSearchView model)
        {
            try
            {
                var result = inprocSvc.SearchScanPcs(model);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [Route("job-inprocess/postSerachFinPcs")]
        public HttpResponseMessage postSerachFinPcs(JobInProcessSearchView model)
        {
            try
            {
                var result = inprocSvc.SerachFinPcs(model);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [Route("job-inprocess/postScanPcs")]
        public HttpResponseMessage postScanPcs(JobInProcessSearchView model)
        {
            try
            {
                inprocSvc.UpdatePcs(model);

                return Request.CreateResponse(HttpStatusCode.OK, "บันทึกข้อมูลสำเร็จ");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [Route("job-inprocess/postCancelPcs")]
        public HttpResponseMessage postCancelPcs(JobInProcessSearchView model)
        {
            try
            {
                inprocSvc.CancelPcs(model);

                return Request.CreateResponse(HttpStatusCode.OK, "ยกเลิกข้อมูลสำเร็จ");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }
    }
}