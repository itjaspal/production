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
    public class JobMachineController : ApiController
    {
        IJobMachineService jobSvc;

        public JobMachineController()
        {
            jobSvc = new JobMachineService();
        }

        [Route("job/postSearchcurrent")]
        public HttpResponseMessage postSearchCurrent(JobMachineSearchView model)
        {
            try
            {
                var result = jobSvc.SearchCurrent(model);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [Route("job/postSearchpending")]
        public HttpResponseMessage postSearchPending(JobMachineSearchView model)
        {
            try
            {
                var result = jobSvc.SearchPending(model);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [Route("job/postSearchforward")]
        public HttpResponseMessage postSearchForward(JobMachineSearchView model)
        {
            try
            {
                var result = jobSvc.SearchForward(model);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }



    }
}
