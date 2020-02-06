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
    public class JobMachineService : IJobMachineService
    {
        public JobMachineView SearchCurrent(JobMachineSearchView model)
        {
            using (var ctx = new ConXContext())
            {
                //define model view
                JobMachineView view = new ModelViews.JobMachineView()
                {
                    pageIndex = model.pageIndex - 1,
                    itemPerPage = model.itemPerPage,
                    totalItem = 0,


                    datas = new List<ModelViews.JobMachineReqView>()
                };

                string ventity = model.entity;
                string vwc_code = model.wc_code;
                string vmc_code = model.mc_code;


                pd_jit_schedule_ctl jit = ctx.jit_schedule_ctl.SqlQuery("select MAX(MPS_BACK_DAY) MPS_BACK_DAY  , max(PD_ENTITY) PD_ENTITY , max(SEQ_NO) SEQ_NO , max(PDGRP_CODE) PDGRP_CODE , max(WC_CODE) WC_CODE from PD_JIT_SCHEDULE_CTL where pd_entity = :param1 and wc_code = :param2", new OracleParameter("param1", ventity), new OracleParameter("param2", vwc_code)).SingleOrDefault();
                var vmps_back_day = jit.MPS_BACK_DAY;
                
                mps_mast mps = ctx.mps_mast.SqlQuery("select max(REQ_DATE) REQ_DATE , max(ENTITY) ENTITY , max(BUILD_NO) BUILD_NO from MPS_MAST where entity = :param1 and req_date <= (SYSDATE + :p_mps_back_day)", new OracleParameter("param1", ventity), new OracleParameter("p_mps_back_day", vmps_back_day)).SingleOrDefault();
                var vreq_date = mps.REQ_DATE;

                //DateTime vreq_date = DateTime.Now;

                //query data
                string sql = "SELECT PCS_BARCODE , REQ_DATE , MC_CODE , SPRINGTYPE_CODE  ,PDSIZE_DESC ,sum(PLAN_QTY) PLAN_QTY , sum(ACTUAL_QTY) ACTUAL_QTY FROM ("; 
                sql += " (select max(a.PCS_BARCODE) PCS_BARCODE , a.REQ_DATE , a.MC_CODE , a.SPRINGTYPE_CODE  , a.PDSIZE_DESC , count(*) PLAN_QTY , 0 ACTUAL_QTY from MPS_DET_IN_PROCESS a , MPS_DET_WC c";
                sql += " where a.entity = c.entity";
                sql += " and a.req_date = c.req_date";
                sql += " and a.wc_code = c.wc_code";
                sql += " and a.pcs_barcode = c.pcs_barcode";
                sql += " and a.entity = :p_entity";
                sql += " and a.req_date = trunc(:p_req_date)";
                sql += " and a.wc_code = :p_wc_code";
                sql += " and c.mps_st <> 'OCL'";
                sql += " and a.mc_code = :p_mc_code";
                sql += " group by a.req_date , a.mc_code , a.springtype_code  , a.pdsize_desc)";
                sql += " UNION ALL ";
                sql += " (select max(a.PCS_BARCODE) PCS_BARCODE , a.REQ_DATE , a.MC_CODE , a.SPRINGTYPE_CODE  , a.PDSIZE_DESC , 0 PLAN_QTY , count(*) ACTUAL_QTY from MPS_DET_IN_PROCESS a , MPS_DET_WC c";
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
                sql += " group by a.req_date , a.mc_code , a.springtype_code  , a.pdsize_desc )";
                sql += " ) Group by REQ_DATE,  MC_CODE ,SPRINGTYPE_CODE  ,PDSIZE_DESC , pcs_barcode";
                sql += " Order  by req_date , mc_code , springType_Code ,PDSIZE_DESC  ";

                List <JobMachineReqView> jobcurrentView = ctx.Database.SqlQuery<JobMachineReqView>(sql, new OracleParameter("p_entity", ventity), new OracleParameter("p_req_date", vreq_date), new OracleParameter("p_wc_code", vwc_code), new OracleParameter("p_mc_code", vmc_code)).ToList();



                view.totalItem = jobcurrentView.Count;
                jobcurrentView = jobcurrentView.Skip(view.pageIndex * view.itemPerPage)
                    .Take(view.itemPerPage)
                    .ToList();

                ////prepare model to modelView
                foreach (var i in jobcurrentView)
                {

                    view.datas.Add(new ModelViews.JobMachineReqView()
                    {
                        req_date  = i.req_date,
                        pdsize_desc = i.pdsize_desc,
                        springtype_code = i.springtype_code,
                        plan_qty = i.plan_qty,
                        actual_qty = i.actual_qty,
                        diff_qty = i.plan_qty - i.actual_qty,
                        pcs_barcode = i.pcs_barcode
                    });
                }

                //return data to contoller
                return view;
            }
        }

        public JobMachineView SearchPending(JobMachineSearchView model)
        {
            using (var ctx = new ConXContext())
            {
                //define model view
                JobMachineView view = new ModelViews.JobMachineView()
                {
                    pageIndex = model.pageIndex - 1,
                    itemPerPage = model.itemPerPage,
                    totalItem = 0,


                    datas = new List<ModelViews.JobMachineReqView>()
                };

                string ventity = model.entity;
                string vwc_code = model.wc_code;
                string vmc_code = model.mc_code;


                pd_jit_schedule_ctl jit = ctx.jit_schedule_ctl.SqlQuery("select MAX(MPS_BACK_DAY) MPS_BACK_DAY  , max(PD_ENTITY) PD_ENTITY , max(SEQ_NO) SEQ_NO , max(PDGRP_CODE) PDGRP_CODE , max(WC_CODE) WC_CODE from PD_JIT_SCHEDULE_CTL where pd_entity = :param1 and wc_code = :param2", new OracleParameter("param1", ventity), new OracleParameter("param2", vwc_code)).SingleOrDefault();
                var vmps_back_day = jit.MPS_BACK_DAY;

                mps_mast mps = ctx.mps_mast.SqlQuery("select max(REQ_DATE) REQ_DATE , max(ENTITY) ENTITY , max(BUILD_NO) BUILD_NO from MPS_MAST where entity = :param1 and req_date <= (SYSDATE + :p_mps_back_day)", new OracleParameter("param1", ventity), new OracleParameter("p_mps_back_day", vmps_back_day)).SingleOrDefault();
                var vreq_date = mps.REQ_DATE;

                //DateTime vreq_date = DateTime.Now;

                //query data
                string sql = "SELECT PCS_BARCODE , REQ_DATE ,MC_CODE , SPRINGTYPE_CODE  ,PDSIZE_DESC ,sum(PLAN_QTY) PLAN_QTY , sum(ACTUAL_QTY) ACTUAL_QTY FROM (";
                sql += " (select max(a.PCS_BARCODE) PCS_BARCODE , a.REQ_DATE , a.MC_CODE , a.SPRINGTYPE_CODE  , a.PDSIZE_DESC , count(*) PLAN_QTY , 0 ACTUAL_QTY from MPS_DET_IN_PROCESS a , MPS_DET_WC c";
                sql += " where a.entity = c.entity";
                sql += " and a.req_date = c.req_date";
                sql += " and a.wc_code = c.wc_code";
                sql += " and a.pcs_barcode = c.pcs_barcode";
                sql += " and a.entity = :p_entity";
                sql += " and a.req_date < trunc(:p_req_date)";
                sql += " and a.wc_code = :p_wc_code";
                sql += " and c.mps_st <> 'OCL'";
                sql += " and a.mc_code = :p_mc_code";
                sql += " group by a.req_date , a.mc_code , a.springtype_code  , a.pdsize_desc)";
                sql += " UNION ALL ";
                sql += " (select max(a.PCS_BARCODE) PCS_BARCODE ,  a.REQ_DATE , a.MC_CODE , a.SPRINGTYPE_CODE  , a.PDSIZE_DESC , 0 PLAN_QTY , count(*) ACTUAL_QTY from MPS_DET_IN_PROCESS a , MPS_DET_WC c";
                sql += " where a.entity = c.entity";
                sql += " and a.req_date = c.req_date";
                sql += " and a.wc_code = c.wc_code";
                sql += " and a.pcs_barcode = c.pcs_barcode";
                sql += " and a.entity = :p_entity";
                sql += " and a.req_date < trunc(:p_req_date)";
                sql += " and a.wc_code = :p_wc_code";
                sql += " and c.mps_st <> 'OCL'";
                sql += " and a.mc_code = :p_mc_code";
                sql += " and a.mps_st = 'Y'";
                sql += " group by a.REQ_DATE , a.mc_code , a.springtype_code  , a.pdsize_desc)";
                sql += " ) Group by REQ_DATE , MC_CODE ,SPRINGTYPE_CODE  ,PDSIZE_DESC , pcs_barcode";
                sql += " Order  by req_date , mc_code , springType_Code ,PDSIZE_DESC ";

                List<JobMachineReqView> jobcurrentView = ctx.Database.SqlQuery<JobMachineReqView>(sql, new OracleParameter("p_entity", ventity), new OracleParameter("p_req_date", vreq_date), new OracleParameter("p_wc_code", vwc_code), new OracleParameter("p_mc_code", vmc_code)).ToList();



                view.totalItem = jobcurrentView.Count;
                jobcurrentView = jobcurrentView.Skip(view.pageIndex * view.itemPerPage)
                    .Take(view.itemPerPage)
                    .ToList();

                ////prepare model to modelView
                foreach (var i in jobcurrentView)
                {

                    view.datas.Add(new ModelViews.JobMachineReqView()
                    {
                        req_date = i.req_date,
                        pdsize_desc = i.pdsize_desc,
                        springtype_code = i.springtype_code,
                        plan_qty = i.plan_qty,
                        actual_qty = i.actual_qty,
                        diff_qty = i.plan_qty - i.actual_qty,
                        pcs_barcode = i.pcs_barcode
                    });
                }

                //return data to contoller
                return view;
            }
        }

        public JobMachineView SearchForward(JobMachineSearchView model)
        {
            using (var ctx = new ConXContext())
            {
                //define model view
                JobMachineView view = new ModelViews.JobMachineView()
                {
                    pageIndex = model.pageIndex - 1,
                    itemPerPage = model.itemPerPage,
                    totalItem = 0,


                    datas = new List<ModelViews.JobMachineReqView>()
                };

                string ventity = model.entity;
                string vwc_code = model.wc_code;
                string vmc_code = model.mc_code;


                pd_jit_schedule_ctl jit = ctx.jit_schedule_ctl.SqlQuery("select MAX(MPS_BACK_DAY) MPS_BACK_DAY  , max(PD_ENTITY) PD_ENTITY , max(SEQ_NO) SEQ_NO , max(PDGRP_CODE) PDGRP_CODE , max(WC_CODE) WC_CODE from PD_JIT_SCHEDULE_CTL where pd_entity = :param1 and wc_code = :param2", new OracleParameter("param1", ventity), new OracleParameter("param2", vwc_code)).SingleOrDefault();
                var vmps_back_day = jit.MPS_BACK_DAY;

                mps_mast mps = ctx.mps_mast.SqlQuery("select max(REQ_DATE) REQ_DATE , max(ENTITY) ENTITY , max(BUILD_NO) BUILD_NO from MPS_MAST where entity = :param1 and req_date <= (SYSDATE + :p_mps_back_day)", new OracleParameter("param1", ventity), new OracleParameter("p_mps_back_day", vmps_back_day)).SingleOrDefault();
                var vreq_date = mps.REQ_DATE;

                //DateTime vreq_date = DateTime.Now;

                //query data
                string sql = "SELECT PCS_BARCODE , REQ_DATE ,MC_CODE , SPRINGTYPE_CODE  ,PDSIZE_DESC ,sum(PLAN_QTY) PLAN_QTY , sum(ACTUAL_QTY) ACTUAL_QTY FROM (";
                sql += " (select max(a.PCS_BARCODE) PCS_BARCODE , a.REQ_DATE , a.MC_CODE , a.SPRINGTYPE_CODE  , a.PDSIZE_DESC , count(*) PLAN_QTY , 0 ACTUAL_QTY from MPS_DET_IN_PROCESS a , MPS_DET_WC c";
                sql += " where a.entity = c.entity";
                sql += " and a.req_date = c.req_date";
                sql += " and a.wc_code = c.wc_code";
                sql += " and a.pcs_barcode = c.pcs_barcode";
                sql += " and a.entity = :p_entity";
                sql += " and a.req_date > trunc(:p_req_date)";
                sql += " and a.wc_code = :p_wc_code";
                sql += " and c.mps_st <> 'OCL'";
                sql += " and a.mc_code = :p_mc_code";
                sql += " group by a.req_date , a.mc_code , a.springtype_code  , a.pdsize_desc)";
                sql += " UNION ALL ";
                sql += " (select max(a.PCS_BARCODE) PCS_BARCODE , a.REQ_DATE , a.MC_CODE , a.SPRINGTYPE_CODE  , a.PDSIZE_DESC , 0 PLAN_QTY , count(*) ACTUAL_QTY from MPS_DET_IN_PROCESS a , MPS_DET_WC c";
                sql += " where a.entity = c.entity";
                sql += " and a.req_date = c.req_date";
                sql += " and a.wc_code = c.wc_code";
                sql += " and a.pcs_barcode = c.pcs_barcode";
                sql += " and a.entity = :p_entity";
                sql += " and a.req_date > trunc(:p_req_date)";
                sql += " and a.wc_code = :p_wc_code";
                sql += " and c.mps_st <> 'OCL'";
                sql += " and a.mc_code = :p_mc_code";
                sql += " and a.mps_st = 'Y'";
                sql += " group by a.REQ_DATE , a.mc_code , a.springtype_code  , a.pdsize_desc)";
                sql += " ) Group by REQ_DATE , MC_CODE ,SPRINGTYPE_CODE  ,PDSIZE_DESC , pcs_barcode";
                sql += " Order  by req_date , mc_code , springType_Code ,PDSIZE_DESC ";

                List<JobMachineReqView> jobcurrentView = ctx.Database.SqlQuery<JobMachineReqView>(sql, new OracleParameter("p_entity", ventity), new OracleParameter("p_req_date", vreq_date), new OracleParameter("p_wc_code", vwc_code), new OracleParameter("p_mc_code", vmc_code)).ToList();



                view.totalItem = jobcurrentView.Count;
                jobcurrentView = jobcurrentView.Skip(view.pageIndex * view.itemPerPage)
                    .Take(view.itemPerPage)
                    .ToList();

                ////prepare model to modelView
                foreach (var i in jobcurrentView)
                {

                    view.datas.Add(new ModelViews.JobMachineReqView()
                    {
                        req_date = i.req_date,
                        pdsize_desc = i.pdsize_desc,
                        springtype_code = i.springtype_code,
                        plan_qty = i.plan_qty,
                        actual_qty = i.actual_qty,
                        diff_qty = i.plan_qty - i.actual_qty,
                        pcs_barcode = i.pcs_barcode
                    });
                }

                //return data to contoller
                return view;
            }
        }
    }
}