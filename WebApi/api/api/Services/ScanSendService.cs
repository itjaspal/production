using api.DataAccess;
using api.Interfaces;
using api.Models;
using api.ModelViews;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Transactions;
using System.Web;

namespace api.Services
{
    public class ScanSendService : IScanSendService
    {
        public void CancelPcs(ScanSendProcView model)
        {
            using (var ctx = new ConXContext())
            {
                DateTime vreq_date = Convert.ToDateTime(model.req_date);

                //Check ส่งมอบหน่วยถัดไปแล้ว
                string sqlp = "select d.WC_NEXT from PD_WCCTL_SEQ d where d.pd_entity = :p_entity and d.wc_code = :p_wc_code";

                string vnext_wc = ctx.Database.SqlQuery<string>(sqlp, new OracleParameter("p_entity", model.entity), new OracleParameter("p_wc_code", model.wc_code))
                            .FirstOrDefault();

                string sql = "select a.pcs_barcode from mps_det a, pdmodel_mast b , mps_det_wc c";
                sql += " where a.entity = :p_entity";
                sql += " and a.req_date = to_date(:p_req_date,'dd/mm/yyyy')";
                sql += " and a.pdsize_code = :p_size_code";
                sql += " and substr(b.spring_type,1,2) = :p_spring_grp";
                sql += " and a.pddsgn_code= b.pdmodel_code";
                sql += " and a.entity= c.entity";
                sql += " and a.req_date = c.req_date";
                sql += " and a.pcs_no = c.pcs_no";
                sql += " and c.mps_st='N'";
                sql += " and c.wc_code  = :p_next_wc";
                sql += " and rownum =1";

                string pcs_barcode = ctx.Database.SqlQuery<string>(sql, new OracleParameter("p_entity", model.entity), new OracleParameter("p_req_date", model.req_date), new OracleParameter("p_size_code", model.size_code), new OracleParameter("p_spring_grp", model.spring_grp), new OracleParameter("p_next_wc", vnext_wc)).FirstOrDefault();




                if (pcs_barcode == null)
                {
                    throw new Exception("ยกเลิกไม่ได้");

                }

                using (TransactionScope scope = new TransactionScope())
                {
                    string strConn = ConfigurationManager.ConnectionStrings["OracleDbContext"].ConnectionString;
                    var dataConn = new OracleConnectionStringBuilder(strConn);
                    OracleConnection conn = new OracleConnection(dataConn.ToString());

                    conn.Open();

                    OracleCommand oraCommand = conn.CreateCommand();
                    OracleParameter[] param = new OracleParameter[]
                    {
                        new OracleParameter("p_entity", model.entity),
                        new OracleParameter("p_user_id", model.user_id),
                        new OracleParameter("p_pcs_barcode", pcs_barcode),
                        new OracleParameter("p_wc_code", model.wc_code)
                    };
                    oraCommand.BindByName = true;
                    oraCommand.Parameters.AddRange(param);
                    oraCommand.CommandText = "update MPS_DET_WC set mps_st='N' , fin_by =:p_user_id , fin_date = SYSDATE , upd_by =:p_user_id , upd_date = SYSDATE where entity = :p_entity and pcs_barcode = :p_pcs_barcode and wc_code =:p_wc_code";

                    //oraCommand.ExecuteReader(CommandBehavior.SingleRow);
                    oraCommand.ExecuteNonQuery();

                    conn.Close();


                    scope.Complete();
                }
            }
        }

        public void ScanPcs(ScanSendProcView model)
        {
            using (var ctx = new ConXContext())
            {
                DateTime vreq_date = Convert.ToDateTime(model.req_date);

                //Check QP QTY

                string sqlp = "select d.WC_PREV from PD_WCCTL_SEQ d where d.pd_entity = :p_entity and d.wc_code = :p_wc_code";

                string vprev_wc = ctx.Database.SqlQuery<string>(sqlp, new OracleParameter("p_entity", model.entity), new OracleParameter("p_wc_code", model.wc_code))
                            .FirstOrDefault();

                string sql = "select count(*) from MPS_DET_WC where entity = :p_entity and pcs_barcode = :p_pcs_barcode and wc_code = :p_prev_wc and mps_st = 'Y'";

                int qp_qty = ctx.Database.SqlQuery<int>(sql, new OracleParameter("p_entity", model.entity), new OracleParameter("p_entity", model.pcs_barcode), new OracleParameter("p_prev_wc", vprev_wc)).FirstOrDefault();




                if (qp_qty == 0)
                {
                    throw new Exception("Scan ส่งมอบเกิน Quit Panel ไม่ได้");

                }

               

                using (TransactionScope scope = new TransactionScope())
                {

                    string strConn = ConfigurationManager.ConnectionStrings["OracleDbContext"].ConnectionString;
                    var dataConn = new OracleConnectionStringBuilder(strConn);
                    OracleConnection conn = new OracleConnection(dataConn.ToString());

                    conn.Open();

                    OracleCommand oraCommand = conn.CreateCommand();
                    OracleParameter[] param = new OracleParameter[]
                    {
                        new OracleParameter("p_entity", model.entity),
                        new OracleParameter("p_user_id", model.user_id),
                        new OracleParameter("p_pcs_barcode", model.pcs_barcode),
                        new OracleParameter("p_wc_code", model.wc_code)
                    };
                    oraCommand.BindByName = true;
                    oraCommand.Parameters.AddRange(param);
                    oraCommand.CommandText = "update MPS_DET_WC set mps_st='Y' , fin_by =:p_user_id , fin_date = SYSDATE , upd_by =:p_user_id , upd_date = SYSDATE where entity = :p_entity and pcs_barcode = :p_pcs_barcode and wc_code =:p_wc_code";

                    //oraCommand.ExecuteReader(CommandBehavior.SingleRow);
                    oraCommand.ExecuteNonQuery();

                    conn.Close();

                    
                    scope.Complete();
                }
            }
        }

        

        public ScanPcsView SearchPcs(ScanPcsSearchView model)
        {
            using (var ctx = new ConXContext())
            {


                //DateTime vreq_date = Convert.ToDateTime(model.req_date);
                var ventity = model.entity;
                var vpcs_barcode = model.pcs_barcode;
                var vwc_code = model.wc_code;

               

                string sql = "select a.PCS_BARCODE , a.SIZE_NAME as SIZE_DESC , substr(b.SPRING_TYPE,1,2) as SPRING_GRP , 1 as QTY";
                sql += " from MPS_DET a , PDMODEL_MAST b , MPS_DET_WC c";
                sql += " where a.pcs_barcode = :p_pcs_barcode";
                sql += " and a.entity  = :p_entity";
                sql += " and a.pddsgn_code  = b.pdmodel_code";
                sql += " and a.entity  = c.entity";
                sql += " and a.req_date  = c.req_date";
                sql += " and a.pcs_no  = c.pcs_no";
                sql += " and c.mps_st  = 'N'";
                sql += " and c.wc_code  = :p_wc_code";

               
                List<ScanPcsView> mps_det = ctx.Database.SqlQuery<ScanPcsView>(sql, new OracleParameter("p_pcs_barcode", vpcs_barcode), new OracleParameter("p_entity", ventity),  new OracleParameter("p_wc_code", vwc_code)).ToList();
               

                if (mps_det.Count == 0)
                {

                    throw new Exception("PSC Barcodeไม่ถูกต้อง");

                }



                ////define model view

               

                ScanPcsView view = new ModelViews.ScanPcsView();
                    

                foreach (var i in mps_det)
                {
                    view.pcs_barcode = i.pcs_barcode;
                    view.spring_grp = i.spring_grp;
                    view.size_desc = i.size_desc;
                    view.qty = 1;
                }


                //return data to contoller
                return view;
            }
        }

        public ScanPcsView SearchScanPcs(ScanPcsSearchView model)
        {
            using (var ctx = new ConXContext())
            {


                String[] strlist = model.pcs_barcode.Split('|');
                string vspring_grp = strlist[0];
                string vsize_code = strlist[1];

                string sqlp = "select d.WC_PREV from PD_WCCTL_SEQ d where d.pd_entity = :p_entity and d.wc_code = :p_wc_code";

                string vprev_wc = ctx.Database.SqlQuery<string>(sqlp, new OracleParameter("p_entity", model.entity), new OracleParameter("p_wc_code", model.wc_code))
                            .FirstOrDefault();

                //if(strlist.Length < 1)
                //{
                //    throw new Exception("รูปแบบ QR ไม่ถูกต้อง");
                //}

                //DateTime vreq_date = Convert.ToDateTime(model.req_date);

                string sql = "select a.PCS_BARCODE from MPS_DET a , PDMODEL_MAST b , MPS_DET_WC c";
                sql += " where a.req_date = to_date(:p_req_date,'dd/mm/yyyy')";
                sql += " and a.entity  = :p_entity";
                sql += " and a.pdsize_code  = :p_size_code";
                sql += " and substr(b.spring_type,1,2)  = :p_spring_grp";
                sql += " and c.wc_code  = :p_wc_code";
                sql += " and a.pddsgn_code  = b.pdmodel_code";
                sql += " and a.entity  = c.entity";
                sql += " and a.req_date  = c.req_date";
                sql += " and a.pcs_no  = c.pcs_no";
                //sql += " and c.mps_st  <> 'OCL';
                sql += " and c.mps_st  = 'N'";
                //sql += " and rownum = 1";
                sql += " and a.pcs_barcode in (select d.pcs_barcode from  MPS_DET d, PDMODEL_MAST e , MPS_DET_WC f";
                sql += " where d.req_date = to_date(:p_req_date2,'dd/mm/yyyy')";
                sql += " and d.entity = :p_entity2";
                sql += " and d.pdsize_code = :p_size_code2";
                sql += " and substr(e.spring_type,1,2)  = :p_spring_grp2";
                sql += " and f.wc_code = :p_prev_wc";
                sql += " and d.pddsgn_code = e.pdmodel_code";
                sql += " and d.entity = f.entity";
                sql += " and d.req_date = f.req_date";
                sql += " and d.pcs_no = f.pcs_no";
                sql += " and f.mps_st = 'Y'";
                sql += " and rownum = 1)";



                string pcs_barcode = ctx.Database.SqlQuery<string>(sql, new OracleParameter("p_req_date", model.req_date), new OracleParameter("p_entity", model.entity), new OracleParameter("p_size_code", vsize_code), new OracleParameter("p_spring_grp", vspring_grp), new OracleParameter("p_wc_code", model.wc_code), new OracleParameter("p_req_date2", model.req_date), new OracleParameter("p_entity2", model.entity), new OracleParameter("p_size_code2", vsize_code), new OracleParameter("p_spring_grp2", vspring_grp), new OracleParameter("p_prev_wc", vprev_wc))
                            .FirstOrDefault();

                //string pcs_barcode = ctx.Database.SqlQuery<string>(sql).FirstOrDefault();

                //List<ScanPcsView> pcs = ctx.Database.SqlQuery<ScanPcsView>(sql, new OracleParameter("p_req_date", model.req_date), new OracleParameter("p_entity", model.entity), new OracleParameter("p_size_code", vsize_code), new OracleParameter("p_spring_grp", vspring_grp), new OracleParameter("p_wc_code", model.wc_code)).ToList();




                if (pcs_barcode == null)
                {
                    throw new Exception("PCS Barcodeไม่ถูกต้อง");
                }






                ////define model view
                ScanPcsView view = new ModelViews.ScanPcsView()
                {
                    pcs_barcode = pcs_barcode,
                    spring_grp = vspring_grp,
                    size_desc = vsize_code,
                    qty = 1,
                    //datas = new List<ModelViews.JobInProcessScanView>()
                };



                //return data to contoller
                return view;
            }
            
        }

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
                string vreq_date = model.req_date;
                string vmc_code = model.mc_code;


                if (vreq_date == "")
                {
                    pd_jit_schedule_ctl jit = ctx.jit_schedule_ctl.SqlQuery("select MAX(MPS_BACK_DAY) MPS_BACK_DAY  , max(PD_ENTITY) PD_ENTITY , max(SEQ_NO) SEQ_NO , max(PDGRP_CODE) PDGRP_CODE , max(WC_CODE) WC_CODE from PD_JIT_SCHEDULE_CTL where pd_entity = :param1 and wc_code = :param2", new OracleParameter("param1", ventity), new OracleParameter("param2", vwc_code)).SingleOrDefault();
                    var vmps_back_day = jit.MPS_BACK_DAY;
                    ////

                    mps_mast mps = ctx.mps_mast.SqlQuery("select max(REQ_DATE) REQ_DATE , max(ENTITY) ENTITY , max(BUILD_NO) BUILD_NO from MPS_MAST where entity = :param1 and req_date <= (SYSDATE + :p_mps_back_day)", new OracleParameter("param1", ventity), new OracleParameter("p_mps_back_day", vmps_back_day)).SingleOrDefault();
                    vreq_date = mps.REQ_DATE.ToString("dd/MM/yyyy");
                }

                //query data

                string sql = "select ENTITY, REQ_DATE, SPRINGTYPE_CODE, PDSIZE_CODE, PDSIZE_DESC, sum(PLAN_QTY)  PLAN_QTY ,sum(INACT_QTY) INACT_QTY ,sum(QP_QTY)  QP_QTY ,sum(ACT_QTY)  ACT_QTY FROM (";
                sql += " select c.ENTITY , c.REQ_DATE  , b.SPRING_TYPE as SPRINGTYPE_CODE, c.PDSIZE_CODE , c.PDSIZE_DESC  , sum(1) as PLAN_QTY , 0 as INACT_QTY , 0 as QP_QTY , 0 as ACT_QTY";
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
                sql += " UNION";
                sql += " select c.ENTITY , c.REQ_DATE , b.SPRING_TYPE as SPRINGTYPE_CODE, c.PDSIZE_CODE , c.PDSIZE_DESC , 0 as PLAN_QTY , sum(1) as INACT_QTY ,0 as QP_QTY ,0 as ACT_QTY";
                sql += " from MPS_DET a, PDMODEL_MAST b, MPS_DET_WC c";
                sql += " where trunc(a.req_date) = to_date(:p_req_date,'dd/mm/yyyy')";
                sql += " and a.entity = :p_entity";
                sql += " and c.wc_code =  :p_wc_code";
                sql += " and a.pddsgn_code = b.pdmodel_code";
                sql += " and a.entity = c.entity";
                sql += " and a.req_date = c.req_date";
                sql += " and a.pcs_no = c.pcs_no";
                sql += " and c.mps_st <> 'OCL'";
                sql += " and c.pcs_no  in (select PCS_NO from MPS_DET_IN_PROCESS where req_date = to_date(:p_req_date,'dd/mm/yyyy') and entity = :p_entity and mps_st = 'Y' and wc_code = :p_wc_code)";
                sql += " group by c.entity , c.req_date , b.spring_type , c.pdsize_code , c.pdsize_desc";
                sql += " UNION";
                sql += " select c.ENTITY , c.REQ_DATE , b.SPRING_TYPE as SPRINGTYPE_CODE , c.PDSIZE_CODE , c.PDSIZE_DESC ,0 as PLAN_QTY  ,0 as INACT_QTY  ,sum(1) as QP_QTY ,0 as ACT_QTY";
                sql += " FROM MPS_DET a,PDMODEL_MAST b,MPS_DET_WC c";
                sql += " where trunc(a.req_date) = to_date(:p_req_date,'dd/mm/yyyy')";
                sql += " and a.entity = :p_entity";
                sql += " and a.pddsgn_code = b.pdmodel_code";
                sql += " and a.entity = c.entity";
                sql += " and a.req_date = c.req_date";
                sql += " and a.pcs_no = c.pcs_no";
                sql += " and c.mps_st = 'Y'";
                sql += " and c.wc_code = (select  WC_PREV from PD_WCCTL_SEQ where pd_entity = :p_entity  and wc_code = :p_wc_code)";
                sql += " group by  c.entity , c.req_date , b.spring_type , c.pdsize_code , c.pdsize_desc";
                sql += " UNION";
                sql += " select c.ENTITY , c.REQ_DATE , b.SPRING_TYPE as SPRINGTYPE_CODE , c.PDSIZE_CODE , c.PDSIZE_DESC , 0 as PLAN_QTY  ,0 as INACT_QTY ,0 as QP_QTY ,sum(1) as  ACT_QTY";
                sql += " from MPS_DET a,PDMODEL_MAST b,MPS_DET_WC c";
                sql += " where trunc(a.req_date) = to_date(:p_req_date,'dd/mm/yyyy')";
                sql += " and a.entity = :p_entity";
                sql += " and a.pddsgn_code = b.pdmodel_code";
                sql += " and a.entity = c.entity";
                sql += " and a.req_date = c.req_date";
                sql += " and a.pcs_no = c.pcs_no";
                sql += " and c.mps_st = 'Y'";
                sql += " and c.wc_code = :p_wc_code";
                sql += " group by  c.entity , c.req_date , b.spring_type,c.pdsize_code , c.pdsize_desc";
                sql += " ) group by entity , req_date , springtype_code , pdsize_code , pdsize_desc";

                List<SpringDataView> spring = ctx.Database.SqlQuery<SpringDataView>(sql, new OracleParameter("p_entity", ventity), new OracleParameter("p_req_date" , vreq_date), new OracleParameter("p_wc_code", vwc_code)).ToList();
            
                //List<SpringDataView> spring = ctx.Database.SqlQuery<SpringDataView>("BEGIN TEST_PKG.GetSpringMpsSummList('B10' ,'PB1',to_date('07/02/2020','dd/mm/yyyy'),'IT'); END;").ToList();



                view.totalItem = spring.Count;
                spring = spring.Skip(view.pageIndex * view.itemPerPage)
                    .Take(view.itemPerPage)
                    .ToList();


                int tot_plan_qty = 0;
                int tot_inact_qty = 0;
                int tot_qp_qty = 0;
                int tot_act_qty = 0;
                int tot_diff_qty = 0;

                ////prepare model to modelView
                foreach (var i in spring)
                {
                    tot_plan_qty += i.plan_qty;
                    tot_inact_qty += i.inact_qty;
                    tot_qp_qty += i.qp_qty;
                    tot_act_qty += i.act_qty;
                    tot_diff_qty += (i.act_qty - i.plan_qty);

                    view.datas.Add(new ModelViews.SpringDataView()
                    {
                        //entity = i.entity,
                        req_date = i.req_date,
                        pdsize_code = i.pdsize_code,
                        pdsize_desc = i.pdsize_desc,
                        springtype_code = i.springtype_code,
                        plan_qty = i.plan_qty,
                        inact_qty = i.inact_qty,
                        qp_qty = i.qp_qty,
                        act_qty = i.act_qty,
                        diff_qty = i.act_qty - i.plan_qty
                    });
                }

                view.req_date = vreq_date;
                view.mc_code = vmc_code;
                view.spring_grp = vmc_code.Substring(0,2);
                view.total_plan_qty = tot_plan_qty;
                view.total_inact_qty = tot_inact_qty;
                view.total_qp_qty = tot_qp_qty;
                view.total_act_qty = tot_act_qty;
                view.total_diff_qty = tot_diff_qty;



                //return data to contoller
                return view;
            }
        }

        public ScanSendFinView SerachFinPcs(ScanSendFinSearchView model)
        {
            using (var ctx = new ConXContext())
            {
                //define model view
                ScanSendFinView view = new ModelViews.ScanSendFinView()
                {
                    pageIndex = model.pageIndex - 1,
                    itemPerPage = model.itemPerPage,
                    totalItem = 0,


                    datas = new List<ModelViews.ScanSendDataView>()
                };


                string sql = "select a.PCS_BARCODE , a.PROD_CODE , b.PROD_TNAME PROD_NAME , b.PDMODEL_DESC MODEL_DESC";
                sql += " from MPS_DET_WC a , PRODUCT b";
                sql += " where a.prod_code = b.prod_code";
                sql += " and a.mps_st = 'Y'";
                sql += " and a.fin_by = :p_user_id";
                sql += " and trunc(a.fin_date) = trunc(SYSDATE)";
                sql += " and a.entity = :p_entity";
                sql += " and a.wc_code = :p_wc_code";


                List<ScanSendDataView> scan = ctx.Database.SqlQuery<ScanSendDataView>(sql, new OracleParameter("p_user_id", model.user_id), new OracleParameter("p_entity", model.entity),  new OracleParameter("p_wc_code", model.wc_code)).ToList();



                view.totalItem = scan.Count;
                scan = scan.Skip(view.pageIndex * view.itemPerPage)
                    .Take(view.itemPerPage)
                    .ToList();

                ////prepare model to modelView
                foreach (var i in scan)
                {

                    view.datas.Add(new ModelViews.ScanSendDataView()
                    {
                        pcs_barcode = i.pcs_barcode,
                        model_desc = i.model_desc,
                        prod_code = i.prod_code,
                        prod_name = i.prod_name

                    });
                }

                //return data to contoller
                return view;
            }
        }

        public ScanSendFinView SerachCanPcs(ScanSendFinSearchView model)
        {
            using (var ctx = new ConXContext())
            {
                //define model view
                ScanSendFinView view = new ModelViews.ScanSendFinView()
                {
                    pageIndex = model.pageIndex - 1,
                    itemPerPage = model.itemPerPage,
                    totalItem = 0,


                    datas = new List<ModelViews.ScanSendDataView>()
                };


                string sql = "select a.PCS_BARCODE , a.PROD_CODE , b.PROD_TNAME PROD_NAME , b.PDMODEL_DESC MODEL_DESC";
                sql += " from MPS_DET_WC a , PRODUCT b";
                sql += " where a.prod_code = b.prod_code";
                sql += " and a.mps_st = 'N'";
                sql += " and a.fin_by = :p_user_id";
                sql += " and trunc(a.fin_date) = trunc(SYSDATE)";
                sql += " and a.entity = :p_entity";
                sql += " and a.wc_code = :p_wc_code";


                List<ScanSendDataView> scan = ctx.Database.SqlQuery<ScanSendDataView>(sql, new OracleParameter("p_user_id", model.user_id), new OracleParameter("p_entity", model.entity), new OracleParameter("p_wc_code", model.wc_code)).ToList();



                view.totalItem = scan.Count;
                scan = scan.Skip(view.pageIndex * view.itemPerPage)
                    .Take(view.itemPerPage)
                    .ToList();

                ////prepare model to modelView
                foreach (var i in scan)
                {

                    view.datas.Add(new ModelViews.ScanSendDataView()
                    {
                        pcs_barcode = i.pcs_barcode,
                        model_desc = i.model_desc,
                        prod_code = i.prod_code,
                        prod_name = i.prod_name

                    });
                }

                //return data to contoller
                return view;
            }
        }

        
    }
}