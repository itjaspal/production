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
    public class PrintTagController : ApiController
    {
        IPrintTagService tagSvc;

        public PrintTagController()
        {
            tagSvc = new PrintTagService();
        }

        [Route("print-tag/postSearchPrintTag")]
        public HttpResponseMessage postSearchPrintTag(PrintTagSearchView model)
        {
            try
            {
                var result = tagSvc.searchPrintData(model);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [Route("print-tag/postSearchRawData")]
        public HttpResponseMessage postsearchRawData(RawMatSearchView model)
        {
            try
            {
                var result = tagSvc.searchRawData(model);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [Route("print-tag/postPrintTag")]
        public HttpResponseMessage postPrintTag(PrintTagView model)
        {
            try
            {
                tagSvc.PringTag(model);

                return Request.CreateResponse(HttpStatusCode.OK, "ส่งข้อมูลไปยังเครื่องพิมพ์เรียบร้อยแล้ว");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }

        [Route("print-tag/postDeleteTag")]
        public HttpResponseMessage postDeleteTag(PrintTagProcView model)
        {
            try
            {
                tagSvc.DeleteTag(model);

                return Request.CreateResponse(HttpStatusCode.OK, "ลบข้อมูลเรียบร้อยแล้ว");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }


        }

        [Route("print-tag/postAddTag")]
        public HttpResponseMessage postAddTag(PrintTagSearchView model)
        {
            try
            {
                var result = tagSvc.AddTag(model);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }

        [Route("print-tag/postSearchTagNoList")]
        public HttpResponseMessage searchProcessTagNoList(ProcessTagSearchView model)
        {
            try
            {
                var result = tagSvc.searchProcessTagNoList(model);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [Route("print-tag/postSearchTagNo")]
        public HttpResponseMessage searchProcessTagNo(ProcessTagNoSearch model)
        {
            try
            {
                var result = tagSvc.searchProcessTagNo(model);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [Route("print-tag/postSearchRawScan")]
        public HttpResponseMessage searchRawScan(RawMatScanSerchView model)
        {
            try
            {
                var result = tagSvc.searchRawScan(model);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }
    }
}
