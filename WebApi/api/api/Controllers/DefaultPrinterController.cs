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
    public class DefaultPrinterController : ApiController
    {
        IDefaultPrinterService printerSvc;

        public DefaultPrinterController()
        {
            printerSvc = new DefaultPrinterService();
        }

        [Route("defprinter/getInfo/{code}")]
        public HttpResponseMessage getInfo(string code)
        {
            try
            {
                var result = printerSvc.GetInfo(code);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [Route("defprinter/postSetDefault")]
        public HttpResponseMessage postSetDefault(DefaultPrinterView model)
        {
            try
            {

                //check dupplicate Code
                //var isDupplicate = menuGroupSvc.CheckDupplicate(model.menuFunctionGroupId);
                //if (isDupplicate)
                //{
                //    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, string.Format("รหัสกลุ่มเมนู {0} มีอยู่ในระบบแล้ว", model.menuFunctionGroupId));
                //}

                printerSvc.SetDefault(model);

                return Request.CreateResponse(HttpStatusCode.OK, "บันทึกข้อมูลสำเร็จ");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }
    }
}
