using api.DataAccess;
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
    public class ScanSendService : IScanSendService
    {
        public ScanSendView SearchSpringData(ScanSendSearchView model)
        {
            using (var ctx = new ConXContext())
            {
                //define model view
                ScanSendView view = new ModelViews.ScanSendView()
                {
                    pageIndex = model.pageIndex - 1,
                    itemPerPage = model.itemPerPage,
                    totalItem = 0,


                    datas = new List<ModelViews.SpringDataView>()
                };

                string ventity = model.entity;
                string vwc_code = model.wc_code;




                pd_jit_schedule_ctl jit = ctx.jit_schedule_ctl.SqlQuery("select MAX(MPS_BACK_DAY) MPS_BACK_DAY  , max(PD_ENTITY) PD_ENTITY , max(SEQ_NO) SEQ_NO , max(PDGRP_CODE) PDGRP_CODE , max(WC_CODE) WC_CODE from PD_JIT_SCHEDULE_CTL where pd_entity = :param1 and wc_code = :param2", new OracleParameter("param1", ventity), new OracleParameter("param2", vwc_code)).SingleOrDefault();
                var vmps_back_day = jit.MPS_BACK_DAY;

                mps_mast mps = ctx.mps_mast.SqlQuery("select max(REQ_DATE) REQ_DATE , max(ENTITY) ENTITY , max(BUILD_NO) BUILD_NO from MPS_MAST where entity = :param1 and req_date <= (SYSDATE + :p_mps_back_day)", new OracleParameter("param1", ventity), new OracleParameter("p_mps_back_day", vmps_back_day)).SingleOrDefault();
                var vreq_date = mps.REQ_DATE.ToString("dd/MM/yyyy");

                //Console.WriteLine(vreq_date);





                //DateTime vreq_date = DateTime.Now;

                //query data

                //string sql = "select ENTITY, REQ_DATE, SPRINGTYPE_CODE, PDSIZE_CODE, PDSIZE_DESC, sum(PLAN_QTY)  PLAN_QTY ,sum(INACT_QTY) INACT_QTY ,sum(QP_QTY)  QP_QTY ,sum(ACT_QTY)  ACT_QTY FROM (";
                string sql = "select c.ENTITY , c.REQ_DATE , b.SPRING_TYPE as SPRINGTYPE_CODE, c.PDSIZE_CODE , c.PDSIZE_DESC  , sum(1) as PLAN_QTY , 0 as INACT_QTY , 0 as QP_QTY   , 0 as ACT_QTY";
                sql += " from MPS_DET a ,PDMODEL_MAST b ,MPS_DET_WC c";
                sql += " where a.entity =  :p_entity";
                sql += " and trunc(a.req_date) = to_date(:p_req_date,'dd/mm/yyyy')";
                sql += " and c.wc_code =  :p_wc_code";
                sql += " and a.pddsgn_code = b.pdmodel_code";
                sql += " and a.entity = c.entity";
                sql += " and a.req_date = c.req_date";
                sql += " and a.pcs_no = c.pcs_no";
                sql += " and c.mps_st <> 'OCL'";
                sql += " group by c.entity , c.req_date   ,b.spring_type, c.pdsize_code , c.pdsize_desc";
                ////sql += " UNION ALL";
                //sql += " select MPS_DET_WC.ENTITY   , MPS_DET_WC.REQ_DATE ,PDMODEL_MAST.SPRING_TYPE SPRINGTYPE_CODE, MPS_DET_WC.PDSIZE_CODE , MPS_DET_WC.PDSIZE_DESC ,0      PLAN_QTY ,sum(1) as INACT_QTY ,0 as QP_QTY ,0 as ACT_QTY";
                //sql += " FROM MPS_DET, PDMODEL_MAST, MPS_DET_WC";
                //sql += " WHERE MPS_DET.REQ_DATE = p_req_date";
                //sql += " AND MPS_DET.ENTITY = :p_entity";
                //sql += " AND MPS_DET_WC.wc_code =  :p_wc_code";
                //sql += " AND MPS_DET.PDDSGN_CODE = PDMODEL_MAST.PDMODEL_CODE";
                //sql += " AND MPS_DET.ENTITY = MPS_DET_WC.ENTITY";
                //sql += " AND MPS_DET.REQ_DATE = MPS_DET_WC.REQ_DATE";
                //sql += " AND MPS_DET.PCS_NO = MPS_DET_WC.PCS_NO";
                //sql += " AND MPS_DET_WC.mps_st <> 'OCL'";
                //sql += " AND MPS_DET_WC.PCS_NO  in (select PCS_NO from MPS_DET_IN_PROCESS where REQ_DATE = :p_req_date and ENTITY = :p_entity and mps_st = 'Y' and wc_code = :p_wc_code)";
                //sql += " Group by MPS_DET_WC.ENTITY , MPS_DET_WC.REQ_DATE , PDMODEL_MAST.SPRING_TYPE,MPS_DET_WC.PDSIZE_CODE , MPS_DET_WC.PDSIZE_DESC";
                //sql += " UNION ALL";
                //sql += " select MPS_DET_WC.ENTITY   , MPS_DET_WC.REQ_DATE , PDMODEL_MAST.SPRING_TYPE as SPRINGTYPE_CODE ,MPS_DET_WC.PDSIZE_CODE ,MPS_DET_WC.PDSIZE_DESC ,0 PLAN_QTY  ,0 INACT_QTY  ,sum(1) QP_QTY ,0  ACT_QTY";
                //sql += " FROM MPS_DET,PDMODEL_MAST,MPS_DET_WC";
                //sql += " WHERE MPS_DET.REQ_DATE = p_req_date";
                //sql += " AND MPS_DET.ENTITY = p_entity";
                //sql += " AND MPS_DET.PDDSGN_CODE = PDMODEL_MAST.PDMODEL_CODE";
                //sql += " AND MPS_DET.ENTITY = MPS_DET_WC.ENTITY";
                //sql += " AND MPS_DET.REQ_DATE = MPS_DET_WC.REQ_DATE";
                //sql += " AND MPS_DET.PCS_NO = MPS_DET_WC.PCS_NO";
                //sql += " AND MPS_DET_WC.mps_st = 'Y'";
                //sql += " AND MPS_DET_WC.wc_code = (select  WC_PREV from PD_WCCTL_SEQ where PD_ENTITY = p_entity  and WC_CODE = :p_wc_code)";
                //sql += " Group by   MPS_DET_WC.ENTITY ,MPS_DET_WC.REQ_DATE ,PDMODEL_MAST.SPRING_TYPE,MPS_DET_WC.PDSIZE_CODE ,MPS_DET_WC.PDSIZE_DESC";
                //sql += " UNION ALL";
                //sql += " select MPS_DET_WC.ENTITY  , MPS_DET_WC.REQ_DATE ,PDMODEL_MAST.SPRING_TYPE as SPRINGTYPE_CODE ,MPS_DET_WC.PDSIZE_CODE  ,MPS_DET_WC.PDSIZE_DESC     , 0 PLAN_QTY  ,0 INACT_QTY ,0 QP_QTY ,sum(1)  ACT_QTY";
                //sql += " FROM MPS_DET,PDMODEL_MAST,MPS_DET_WC";
                //sql += " WHERE MPS_DET.REQ_DATE = p_req_date";
                //sql += " AND MPS_DET.ENTITY = p_entity";
                //sql += " AND MPS_DET.PDDSGN_CODE = PDMODEL_MAST.PDMODEL_CODE";
                //sql += " AND MPS_DET.ENTITY = MPS_DET_WC.ENTITY";
                //sql += " AND MPS_DET.REQ_DATE = MPS_DET_WC.REQ_DATE";
                //sql += " AND MPS_DET.PCS_NO = MPS_DET_WC.PCS_NO";
                //sql += " AND MPS_DET_WC.mps_st = 'Y'";
                //sql += " AND MPS_DET_WC.wc_code = p_wc_code";
                //sql += " Group by  MPS_DET_WC.ENTITY , MPS_DET_WC.REQ_DATE , PDMODEL_MAST.SPRING_TYPE,MPS_DET_WC.PDSIZE_CODE , MPS_DET_WC.PDSIZE_DESC)";
                //sql += " ) GROUP BY ENTITY ,REQ_DATE ,SPRINGTYPE_CODE ,PDSIZE_CODE ,PDSIZE_DESC";

               List<SpringDataView> spring = ctx.Database.SqlQuery<SpringDataView>(sql, new OracleParameter("p_entity", ventity), new OracleParameter("p_req_date" , vreq_date), new OracleParameter("p_wc_code", vwc_code)).ToList();
                //List<SpringDataView> spring = ctx.Database.SqlQuery<SpringDataView>(sql, new OracleParameter("p_entity", ventity),  new OracleParameter("p_wc_code", vwc_code)).ToList();

                //List<SpringDataView> spring = ctx.Database.SqlQuery<SpringDataView>("BEGIN TEST_PKG.GetSpringMpsSummList('B10' ,'PB1',to_date('07/02/2020','dd/mm/yyyy'),'IT'); END;").ToList();



                view.totalItem = spring.Count;
                spring = spring.Skip(view.pageIndex * view.itemPerPage)
                    .Take(view.itemPerPage)
                    .ToList();

                ////prepare model to modelView
                foreach (var i in spring)
                {

                    view.datas.Add(new ModelViews.SpringDataView()
                    {
                        //entity = i.entity,
                        req_date = i.req_date,
                        pdsize_code = i.pdsize_code,
                        pdsize_desc = vreq_date,
                        springtype_code = i.pdsize_desc,
                        plan_qty = i.plan_qty,
                        inact_qty = i.inact_qty,
                        qp_qty = i.qp_qty,
                        act_qty = i.act_qty
                    });
                }

                //return data to contoller
                return view;
            }
        }
    }
}