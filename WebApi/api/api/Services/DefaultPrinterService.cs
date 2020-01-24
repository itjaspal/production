using api.DataAccess;
using api.Interfaces;
using api.Models;
using api.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Transactions;

namespace api.Services
{
    public class DefaultPrinterService : IDefaultPrinterService
    {

        public GetDefaultPrinterView GetInfo(string code)
        {
            using (var ctx = new ConXContext())
            {
                whmobileprnt_ctl model = ctx.mobileprnt_ctl
                   .Where(z => z.DEFAULT_NO == code).SingleOrDefault();

                if (model == null)
                {
                    return new GetDefaultPrinterView
                    {
                        serial_no = "",
                        grp_type = "",
                        prnt_point_name = "",
                        default_no = ""
                    };
                }
                else
                {
                    return new GetDefaultPrinterView
                    {
                        serial_no = model.SERIES_NO,
                        grp_type = model.GRP_TYPE,
                        prnt_point_name = model.PRNT_POINT_NAME,
                        default_no = model.DEFAULT_NO
                    };
                }
            }
        }

        public void SetDefault(DefaultPrinterView model)
        {
            using (var ctx = new ConXContext())
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    whmobileprnt_ctl updateObj = ctx.mobileprnt_ctl
                   .Where(z => z.SERIES_NO == model.serial_no && z.GRP_TYPE == "SPRING").SingleOrDefault();


                    updateObj.SERIES_NO = model.serial_no;
                    updateObj.GRP_TYPE = model.grp_type;
                    updateObj.DEFAULT_NO = model.default_no;
                    updateObj.PRNT_POINT_NAME = model.prnt_point_name;
                    updateObj.USER_CODE = model.user_code;
                    updateObj.SYS_DATE = DateTime.Now;

                    ctx.Configuration.AutoDetectChangesEnabled = true;
                    ctx.SaveChanges();
                    scope.Complete();
                }
            }
        }

        
    }
}