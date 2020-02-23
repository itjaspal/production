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
    public class SpringDataController : ApiController
    {
        ISpringDataService springSvc;

        public SpringDataController()
        {
            springSvc = new SpringDataService();
        }

        [Route("spring/postSearchspring")]
        public HttpResponseMessage postSearchSpring(SpringSearchView model)
        {
            try
            {
                var result = springSvc.SearchSpringData(model);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [Route("spring/postSearchspringdate")]
        public HttpResponseMessage postSearchDate(SpringDateSearchView model)
        {
            try
            {
                var result = springSvc.SearchSpringDataDate(model);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }
    }
}
