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
                whmobileprnt_default prnt_def = ctx.mobileprnt_def
                    .Where(z=>z.MC_CODE == code).SingleOrDefault();

                if (prnt_def == null)
                {
                    return new GetDefaultPrinterView
                    {
                        serial_no = "",
                        grp_type = "",
                        prnt_point_name = "",
                        default_no = "",
                        filepath_data = "",
                        filepath_btw = "",
                        filepath_txt = ""
                    };
                }

                else
                {
                    whmobileprnt_ctl model = ctx.mobileprnt_ctl
                   .Where(z => z.SERIES_NO == prnt_def.SERIES_NO).SingleOrDefault();

                
                
                    return new GetDefaultPrinterView
                    {
                        serial_no = model.SERIES_NO,
                        grp_type = model.GRP_TYPE,
                        prnt_point_name = model.PRNT_POINT_NAME,
                        default_no = prnt_def.MC_CODE,
                        filepath_data = model.FILEPATH_DATA,
                        filepath_btw = model.FILEPATH_BTW,
                        filepath_txt = model.FILEPATH_TXT
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

                    whmobileprnt_default prnt_def = ctx.mobileprnt_def
                   .Where(z => z.MC_CODE == model.mc_code).SingleOrDefault();

                    if (prnt_def == null)
                    {
                        whmobileprnt_default newObj = new whmobileprnt_default()
                        {
                            MC_CODE = model.mc_code,
                            SERIES_NO = model.serial_no,
                            UPD_BY = model.user_id,
                            UPD_DATE = DateTime.Now

                        };

                        ctx.mobileprnt_def.Add(newObj);
                        ctx.SaveChanges();
                        scope.Complete();
                    }
                    else
                    {
                        whmobileprnt_default updateObj = ctx.mobileprnt_def
                            .Where(z => z.MC_CODE == model.mc_code).SingleOrDefault();

                        updateObj.MC_CODE = model.mc_code;
                        updateObj.SERIES_NO = model.serial_no;
                        updateObj.UPD_BY = model.user_id;
                        updateObj.UPD_DATE = DateTime.Now;

                        ctx.Configuration.AutoDetectChangesEnabled = true;
                        ctx.SaveChanges();
                        scope.Complete();
                    }

                   
                }
            }
        }

        
    }
}