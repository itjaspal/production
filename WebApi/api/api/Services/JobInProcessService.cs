using api.DataAccess;
using api.Interfaces;
using api.Models;
using api.ModelViews;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Transactions;
using System.Web;

namespace api.Services
{
    public class JobInProcessService : IJobInProcessService
    {
        public void CancelPcs(JobInProcessSearchView model)
        {
            using (var ctx = new ConXContext())
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    mps_det_in_process updateObj = ctx.mps_in_process
                   .Where(z => z.PCS_BARCODE == model.pcs_barcode
                        && z.ENTITY == model.entity
                        && z.WC_CODE == model.wc_code).SingleOrDefault();


                    updateObj.MPS_ST = "N";
                    updateObj.UPD_BY = model.user_id;
                    updateObj.UPD_DATE = DateTime.Now;

                    ctx.Configuration.AutoDetectChangesEnabled = true;
                    ctx.SaveChanges();
                    scope.Complete();
                }
            }
        }

        public JobInProcessView SearchPcs(JobInProcessSearchView model)
        {
            using (var ctx = new ConXContext())
            {

                mps_det_in_process mps_in_process = ctx.mps_in_process
                    .Where(z => z.PCS_BARCODE == model.pcs_barcode  && z.WC_CODE == model.wc_code  && z.ENTITY == model.entity && z.MPS_ST == "N")
                    .SingleOrDefault();


                if (mps_in_process == null)
                {
                    throw new Exception("PSC Barcodeไม่ถูกต้อง");
                }


                //define model view
                JobInProcessView view = new ModelViews.JobInProcessView()
                {
                    pcs_barcode = mps_in_process.PCS_BARCODE,
                    springtype_code = mps_in_process.SPRINGTYPE_CODE,
                    pdsize_desc = mps_in_process.PDSIZE_DESC,
                    qty = 1,
                    datas = new List<ModelViews.JobInProcessScanView>()
                };

               

                //DateTime vreq_date = DateTime.Now;

                //query data
                string sql = "select a.PCS_BARCODE , a.PROD_CODE , b.PROD_TNAME , b.PDMODEL_DESC";
                sql += " from MPS_DET_IN_PROCESS a , PRODUCT b";
                sql += " where a.prod_code = b.prod_code";
                sql += " and a.mps_st = 'Y'";
                sql += " and a.fin_by = :p_user_id";
                sql += " and trunc(a.fin_date) = trunc(SYSDATE)";
                sql += " and a.entity = :p_entity";
                sql += " and a.wc_code = :p_wc_code";


                List<JobInProcessScanView> scan = ctx.Database.SqlQuery<JobInProcessScanView>(sql, new OracleParameter("p_entity", model.entity), new OracleParameter("p_user_id", model.user_id), new OracleParameter("p_wc_code", model.wc_code)).ToList();



                view.totalItem = scan.Count;
                scan = scan.Skip(view.pageIndex * view.itemPerPage)
                    .Take(view.itemPerPage)
                    .ToList();

                ////prepare model to modelView
                foreach (var i in scan)
                {

                    view.datas.Add(new ModelViews.JobInProcessScanView()
                    {
                        pcs_barcode = i.pcs_barcode,
                        pdmodel_code = i.pdmodel_code,
                        prod_code = i.prod_code,
                        prod_name = i.prod_name
                        
                    });
                }

                //return data to contoller
                return view;
            }
        }

        public JobInProcessView SearchScanPcs(JobInProcessSearchView model)
        {
            using (var ctx = new ConXContext())
            {
                
               
                String[] strlist = model.pcs_barcode.Split('|');
                string vspring_grp = strlist[0];
                string vsize_code = strlist[1];

                DateTime vreq_date = Convert.ToDateTime(model.req_date);

                mps_det_in_process mps_in_process = ctx.mps_in_process
                    .Where(z => z.SPRING_GRP == vspring_grp && z.PDSIZE_CODE == vsize_code && System.Data.Entity.DbFunctions.TruncateTime(z.REQ_DATE) == System.Data.Entity.DbFunctions.TruncateTime(vreq_date) && z.WC_CODE == model.wc_code && z.ENTITY == model.entity && z.MPS_ST == "N")
                    .OrderBy(z => z.PCS_BARCODE)
                    .FirstOrDefault();


                if (mps_in_process == null)
                {
                    throw new Exception("PSC Barcodeไม่ถูกต้อง");
                }


                //define model view
                JobInProcessView view = new ModelViews.JobInProcessView()
                {
                    pcs_barcode = mps_in_process.PCS_BARCODE,
                    springtype_code = mps_in_process.SPRINGTYPE_CODE,
                    pdsize_desc = mps_in_process.PDSIZE_DESC,
                    qty = 1,
                    datas = new List<ModelViews.JobInProcessScanView>()
                };



                //DateTime vreq_date = DateTime.Now;

                //query data
                string sql = "select a.PCS_BARCODE , a.PROD_CODE , b.PROD_TNAME , b.PDMODEL_DESC";
                sql += " from MPS_DET_IN_PROCESS a , PRODUCT b";
                sql += " where a.prod_code = b.prod_code";
                sql += " and a.mps_st = 'Y'";
                sql += " and a.fin_by = :p_user_id";
                sql += " and trunc(a.fin_date) = trunc(SYSDATE)";
                sql += " and a.entity = :p_entity";
                sql += " and a.wc_code = :p_wc_code";


                List<JobInProcessScanView> scan = ctx.Database.SqlQuery<JobInProcessScanView>(sql, new OracleParameter("p_entity", model.entity), new OracleParameter("p_user_id", model.user_id), new OracleParameter("p_wc_code", model.wc_code)).ToList();



                view.totalItem = scan.Count;
                scan = scan.Skip(view.pageIndex * view.itemPerPage)
                    .Take(view.itemPerPage)
                    .ToList();

                ////prepare model to modelView
                foreach (var i in scan)
                {

                    view.datas.Add(new ModelViews.JobInProcessScanView()
                    {
                        pcs_barcode = i.pcs_barcode,
                        pdmodel_code = i.pdmodel_code,
                        prod_code = i.prod_code,
                        prod_name = i.prod_name

                    });
                }

                //return data to contoller
                return view;
            }
        }

        public JobInProcessScanFinView SerachFinPcs(JobInProcessSearchView model)
        {
            using (var ctx = new ConXContext())
            {
                //define model view
                JobInProcessScanFinView view = new ModelViews.JobInProcessScanFinView()
                {
                    pageIndex = model.pageIndex - 1,
                    itemPerPage = model.itemPerPage,
                    totalItem = 0,


                    datas = new List<ModelViews.JobInProcessScanView>()
                };


                string sql = "select a.PCS_BARCODE , a.PROD_CODE , b.PROD_TNAME , b.PDMODEL_DESC";
                sql += " from MPS_DET_IN_PROCESS a , PRODUCT b";
                sql += " where a.prod_code = b.prod_code";
                sql += " and a.mps_st = 'Y'";
                sql += " and a.fin_by = :p_user_id";
                sql += " and trunc(a.fin_date) = trunc(SYSDATE)";
                sql += " and a.entity = :p_entity";
                sql += " and a.wc_code = :p_wc_code";


                List<JobInProcessScanView> scan = ctx.Database.SqlQuery<JobInProcessScanView>(sql, new OracleParameter("p_entity", model.entity), new OracleParameter("p_user_id", model.user_id), new OracleParameter("p_wc_code", model.wc_code)).ToList();



                view.totalItem = scan.Count;
                scan = scan.Skip(view.pageIndex * view.itemPerPage)
                    .Take(view.itemPerPage)
                    .ToList();

                ////prepare model to modelView
                foreach (var i in scan)
                {

                    view.datas.Add(new ModelViews.JobInProcessScanView()
                    {
                        pcs_barcode = i.pcs_barcode,
                        pdmodel_code = i.pdmodel_code,
                        prod_code = i.prod_code,
                        prod_name = i.prod_name

                    });
                }

                //return data to contoller
                return view;
            }
        }

        public void UpdatePcs(JobInProcessSearchView model)
        {
            using (var ctx = new ConXContext())
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    mps_det_in_process updateObj = ctx.mps_in_process
                   .Where(z => z.PCS_BARCODE == model.pcs_barcode 
                        && z.ENTITY == model.entity
                        && z.WC_CODE == model.wc_code).SingleOrDefault();


                    updateObj.MPS_ST = "Y";
                    updateObj.FIN_BY = model.user_id;
                    updateObj.FIN_DATE = DateTime.Now;
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