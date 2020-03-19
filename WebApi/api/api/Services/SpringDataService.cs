﻿using api.DataAccess;
using api.Interfaces;
using api.Models;
using api.ModelViews;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.Services
{
    public class SpringDataService : ISpringDataService
    {
        public SpringView SearchSpringData(SpringSearchView model)
        {
            using (var ctx = new ConXContext())
            {
                //define model view
                SpringView view = new ModelViews.SpringView()
                {
                    pageIndex = model.pageIndex - 1,
                    itemPerPage = model.itemPerPage,
                    totalItem = 0,


                    datas = new List<ModelViews.SpringReqView>()
                };

                string ventity = model.entity;
                string vwc_code = model.wc_code;
                string vmc_code = model.mc_code;
                int vmps_back_day;
                DateTime vreq_date;

             

                string sqlj = "select max(mps_back_day) from pd_jit_schedule_ctl where  pd_entity = :param1 and wc_code = :param2";

                int jit = ctx.Database.SqlQuery<int>(sqlj, new OracleParameter("param1", ventity), new OracleParameter("param2", vwc_code)).SingleOrDefault();

                if (jit == 0)
                {
                    vmps_back_day = 0;
                }
                else
                {
                    vmps_back_day = jit;
                }


                string sqlr = "select max(req_date) from mps_mast where entity = :param1 and req_date <= (SYSDATE + :p_mps_back_day)";

                DateTime mps = ctx.Database.SqlQuery<DateTime>(sqlr, new OracleParameter("param1", ventity), new OracleParameter("p_mps_back_day", vmps_back_day)).SingleOrDefault();


                if (mps == null)
                {
                    vreq_date = DateTime.Now;
                }
                else
                {
                    vreq_date = mps;
                }

                //DateTime vreq_date = DateTime.Now;

                //query data
                string sql = "SELECT REQ_DATE , MC_CODE , SPRINGTYPE_CODE ,PDSIZE_CODE ,PDSIZE_DESC ,sum(PLAN_QTY) PLAN_QTY , sum(ACTUAL_QTY) ACTUAL_QTY FROM (";
                sql += " (select  a.REQ_DATE , a.MC_CODE , a.SPRINGTYPE_CODE , a.PDSIZE_CODE , a.PDSIZE_DESC , count(*) PLAN_QTY , 0 ACTUAL_QTY from MPS_DET_IN_PROCESS a , MPS_DET_WC c";
                sql += " where a.entity = c.entity";
                sql += " and a.req_date = c.req_date";
                sql += " and a.wc_code = c.wc_code";
                sql += " and a.pcs_barcode = c.pcs_barcode";
                sql += " and a.entity = :p_entity";
                sql += " and a.req_date = trunc(:p_req_date)";
                sql += " and a.wc_code = :p_wc_code";
                sql += " and c.mps_st <> 'OCL'";
                sql += " and a.mc_code = :p_mc_code";
                sql += " group by a.req_date , a.mc_code , a.springtype_code , a.pdsize_code , a.pdsize_desc)";
                sql += " UNION ALL ";
                sql += " (select  a.REQ_DATE , a.MC_CODE , a.SPRINGTYPE_CODE  , a.PDSIZE_CODE , a.PDSIZE_DESC , 0 PLAN_QTY , count(*) ACTUAL_QTY from MPS_DET_IN_PROCESS a , MPS_DET_WC c";
                sql += " where a.entity = c.entity";
                sql += " and a.req_date = c.req_date";
                sql += " and a.wc_code = c.wc_code";
                sql += " and a.pcs_barcode = c.pcs_barcode";
                sql += " and a.entity = :p_entity";
                sql += " and a.req_date = trunc(:p_req_date)";
                sql += " and a.wc_code = :p_wc_code";
                sql += " and c.mps_st <> 'OCL'";
                sql += " and a.mc_code = :p_mc_code";
                sql += " and a.mps_st = 'Y'";
                sql += " group by a.req_date , a.mc_code , a.springtype_code , a.pdsize_code , a.pdsize_desc )";
                sql += " ) Group by REQ_DATE,  MC_CODE ,SPRINGTYPE_CODE , PDSIZE_CODE ,PDSIZE_DESC";
                sql += " Order  by req_date , mc_code , SPRINGTYPE_CODE , PDSIZE_CODE ,PDSIZE_DESC  ";

                List<SpringReqView> spring = ctx.Database.SqlQuery<SpringReqView>(sql, new OracleParameter("p_entity", ventity), new OracleParameter("p_req_date", vreq_date), new OracleParameter("p_wc_code", vwc_code), new OracleParameter("p_mc_code", vmc_code)).ToList();


                if (spring == null)
                {
                    throw new Exception("ไม่มีข้อมูล");
                }

                view.totalItem = spring.Count;
                spring = spring.Skip(view.pageIndex * view.itemPerPage)
                    .Take(view.itemPerPage)
                    .ToList();


                int tot_plan_qty = 0;
                int tot_actual_qty = 0;
                int tot_diff_qty = 0;

                ////prepare model to modelView
                foreach (var i in spring)
                {
                    tot_plan_qty += i.plan_qty;
                    tot_actual_qty += i.actual_qty;
                    tot_diff_qty += (i.plan_qty - i.actual_qty);

                    view.datas.Add(new ModelViews.SpringReqView()
                    {
                        req_date = i.req_date,
                        pdsize_code = i.pdsize_code,
                        pdsize_desc = i.pdsize_desc,
                        springtype_code = i.springtype_code,
                        plan_qty = i.plan_qty,
                        actual_qty = i.actual_qty,
                        diff_qty = i.plan_qty - i.actual_qty,
                        //pcs_barcode = i.pcs_barcode
                    });
                }

                view.total_plan_qty = tot_plan_qty;
                view.total_actual_qty = tot_actual_qty;
                view.total_diff_qty = tot_diff_qty;
                view.req_date = vreq_date;
               


                //return data to contoller
                return view;
            }
        }

        public SpringView SearchSpringDataDate(SpringDateSearchView model)
        {
            using (var ctx = new ConXContext())
            {
                //define model view
                SpringView view = new ModelViews.SpringView()
                {
                    pageIndex = model.pageIndex - 1,
                    itemPerPage = model.itemPerPage,
                    totalItem = 0,


                    datas = new List<ModelViews.SpringReqView>()
                };

                string ventity = model.entity;
                string vwc_code = model.wc_code;
                string vmc_code = model.mc_code;
                string vreq_date = model.req_date;
                

                //query data
                string sql = "SELECT REQ_DATE , MC_CODE , SPRINGTYPE_CODE ,PDSIZE_CODE ,PDSIZE_DESC ,sum(PLAN_QTY) PLAN_QTY , sum(ACTUAL_QTY) ACTUAL_QTY FROM (";
                sql += " (select  a.REQ_DATE , a.MC_CODE , a.SPRINGTYPE_CODE , a.PDSIZE_CODE , a.PDSIZE_DESC , count(*) PLAN_QTY , 0 ACTUAL_QTY from MPS_DET_IN_PROCESS a , MPS_DET_WC c";
                sql += " where a.entity = c.entity";
                sql += " and a.req_date = c.req_date";
                sql += " and a.wc_code = c.wc_code";
                sql += " and a.pcs_barcode = c.pcs_barcode";
                sql += " and a.entity = :p_entity";
                sql += " and a.req_date = to_date(:p_req_date,'dd/mm/yyyy')";
                sql += " and a.wc_code = :p_wc_code";
                sql += " and c.mps_st <> 'OCL'";
                sql += " and a.mc_code = :p_mc_code";
                sql += " group by a.req_date , a.mc_code , a.springtype_code , a.pdsize_code , a.pdsize_desc)";
                sql += " UNION ALL ";
                sql += " (select  a.REQ_DATE , a.MC_CODE , a.SPRINGTYPE_CODE  , a.PDSIZE_CODE , a.PDSIZE_DESC , 0 PLAN_QTY , count(*) ACTUAL_QTY from MPS_DET_IN_PROCESS a , MPS_DET_WC c";
                sql += " where a.entity = c.entity";
                sql += " and a.req_date = c.req_date";
                sql += " and a.wc_code = c.wc_code";
                sql += " and a.pcs_barcode = c.pcs_barcode";
                sql += " and a.entity = :p_entity";
                sql += " and a.req_date = to_date(:p_req_date,'dd/mm/yyyy')";
                sql += " and a.wc_code = :p_wc_code";
                sql += " and c.mps_st <> 'OCL'";
                sql += " and a.mc_code = :p_mc_code";
                sql += " and a.mps_st = 'Y'";
                sql += " group by a.req_date , a.mc_code , a.springtype_code , a.pdsize_code , a.pdsize_desc )";
                sql += " ) Group by REQ_DATE,  MC_CODE ,SPRINGTYPE_CODE , PDSIZE_CODE ,PDSIZE_DESC";
                sql += " Order  by req_date , mc_code , SPRINGTYPE_CODE , PDSIZE_CODE ,PDSIZE_DESC  ";

                List<SpringReqView> spring = ctx.Database.SqlQuery<SpringReqView>(sql, new OracleParameter("p_entity", ventity), new OracleParameter("p_req_date", vreq_date), new OracleParameter("p_wc_code", vwc_code), new OracleParameter("p_mc_code", vmc_code)).ToList();


                if (spring == null)
                {
                    throw new Exception("ไม่มีข้อมูล");
                }

                view.totalItem = spring.Count;
                spring = spring.Skip(view.pageIndex * view.itemPerPage)
                    .Take(view.itemPerPage)
                    .ToList();


                int tot_plan_qty = 0;
                int tot_actual_qty = 0;
                int tot_diff_qty = 0;

                ////prepare model to modelView
                foreach (var i in spring)
                {
                    tot_plan_qty += i.plan_qty;
                    tot_actual_qty += i.actual_qty;
                    tot_diff_qty += (i.plan_qty - i.actual_qty);

                    view.datas.Add(new ModelViews.SpringReqView()
                    {
                        req_date = i.req_date,
                        pdsize_code = i.pdsize_code,
                        pdsize_desc = i.pdsize_desc,
                        springtype_code = i.springtype_code,
                        plan_qty = i.plan_qty,
                        actual_qty = i.actual_qty,
                        diff_qty = i.plan_qty - i.actual_qty,
                        //pcs_barcode = i.pcs_barcode
                    });
                }

                view.total_plan_qty = tot_plan_qty;
                view.total_actual_qty = tot_actual_qty;
                view.total_diff_qty = tot_diff_qty;
                view.req_date = Convert.ToDateTime(vreq_date);

                //return data to contoller
                return view;
            }
        }
    
    }
}