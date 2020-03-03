using api.DataAccess;
using api.Interfaces;
using api.Models;
using api.ModelViews;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Transactions;
using System.Web;

namespace api.Services
{
    public class JobInProcessService : IJobInProcessService
    {
        

        public JobInProcessView SearchScanCancelPcs(JobInProcessSearchView model)
        {
            using (var ctx = new ConXContext())
            {


                String[] strlist = model.pcs_barcode.Split('|');
                string vspring_grp = strlist[0];
                string vsize_code = strlist[1];

                //DateTime vreq_date = Convert.ToDateTime(model.req_date);

                string sqlp = "select pcs_barcode , spring_grp springtype_code , pdsize_desc ,prod_code";
                sqlp += " from mps_det_in_process";
                sqlp += " where spring_grp = :p_spring_grp";
                sqlp += " and pdsize_code =  :p_size_code";
                sqlp += " and req_date = to_date(:p_req_date,'dd/mm/yyyy')";
                sqlp += " and entity = :p_entity";
                sqlp += " and wc_code =:p_wc_code";
                sqlp += " and mc_code =:p_mc_code";
                sqlp += " and mps_st =  'Y'";
                sqlp += " and rownum = 1";

                JobInProcessView mps_in_process = ctx.Database.SqlQuery<JobInProcessView>(sqlp, new OracleParameter("p_spring_grp", vspring_grp), new OracleParameter("p_size_code", vsize_code), new OracleParameter("p_req_date", model.req_date), new OracleParameter("p_entity", model.entity), new OracleParameter("p_wc_code", model.wc_code), new OracleParameter("p_mc_code", model.mc_code)).FirstOrDefault(); ;



                if (mps_in_process == null)
                {
                    throw new Exception("PSC Barcodeไม่ถูกต้อง");
                }


                //define model view
                JobInProcessView view = new ModelViews.JobInProcessView()
                {
                    pcs_barcode = mps_in_process.pcs_barcode,
                    springtype_code = mps_in_process.springtype_code,
                    pdsize_desc = mps_in_process.pdsize_desc,
                    prod_code = mps_in_process.prod_code,
                    qty = 1,
                    //datas = new List<ModelViews.JobInProcessScanView>()
                };

                // Udpate
                using (TransactionScope scope = new TransactionScope())
                {
                    mps_det_in_process updateObj = ctx.mps_in_process
                   .Where(z => z.PCS_BARCODE == mps_in_process.pcs_barcode
                        && z.ENTITY == model.entity
                        && z.WC_CODE == model.wc_code).SingleOrDefault();


                    updateObj.MPS_ST = "N";
                    updateObj.UPD_BY = model.user_id;
                    updateObj.UPD_DATE = DateTime.Now;
                    updateObj.PROCESS_TAG_QR = model.pcs_barcode;

                    ctx.Configuration.AutoDetectChangesEnabled = true;
                    ctx.SaveChanges();
                    scope.Complete();
                }

                ////////////////////////////////////




                //DateTime vreq_date = DateTime.Now;

                //query data
                //string sql = "select a.PCS_BARCODE , a.PROD_CODE , b.PROD_TNAME , b.PDMODEL_DESC";
                //sql += " from MPS_DET_IN_PROCESS a , PRODUCT b";
                //sql += " where a.prod_code = b.prod_code";
                //sql += " and a.mps_st = 'Y'";
                //sql += " and a.fin_by = :p_user_id";
                //sql += " and trunc(a.fin_date) = trunc(SYSDATE)";
                //sql += " and a.entity = :p_entity";
                //sql += " and a.wc_code = :p_wc_code";
                //sql += " and a.mc_code = :p_mc_code";


                //List<JobInProcessScanView> scan = ctx.Database.SqlQuery<JobInProcessScanView>(sql, new OracleParameter("p_entity", model.entity), new OracleParameter("p_user_id", model.user_id), new OracleParameter("p_wc_code", model.wc_code), new OracleParameter("p_mc_code", model.mc_code)).ToList();



                //view.totalItem = scan.Count;
                //scan = scan.Skip(view.pageIndex * view.itemPerPage)
                //    .Take(view.itemPerPage)
                //    .ToList();

                //////prepare model to modelView
                //foreach (var i in scan)
                //{

                //    view.datas.Add(new ModelViews.JobInProcessScanView()
                //    {
                //        pcs_barcode = i.pcs_barcode,
                //        pdmodel_code = i.pdmodel_code,
                //        prod_code = i.prod_code,
                //        prod_name = i.prod_name

                //    });
                //}

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

                string sqlp = "select pcs_barcode , spring_grp springtype_code , pdsize_desc ,prod_code";
                sqlp += " from mps_det_in_process";
                sqlp += " where spring_grp = :p_spring_grp";
                sqlp += " and pdsize_code =  :p_size_code";
                sqlp += " and req_date = to_date(:p_req_date,'dd/mm/yyyy')";
                sqlp += " and entity = :p_entity";
                sqlp += " and wc_code =:p_wc_code";
                sqlp += " and mc_code =:p_mc_code";
                sqlp += " and mps_st =  'N'";
                sqlp += " and rownum = 1";

                JobInProcessView mps_in_process = ctx.Database.SqlQuery<JobInProcessView>(sqlp, new OracleParameter("p_spring_grp", vspring_grp) ,new OracleParameter("p_size_code", vsize_code), new OracleParameter("p_req_date", model.req_date), new OracleParameter("p_entity", model.entity), new OracleParameter("p_wc_code", model.wc_code), new OracleParameter("p_mc_code", model.mc_code)).FirstOrDefault();

                if (mps_in_process == null)
                {
                    throw new Exception("PSC Barcodeไม่ถูกต้อง");
                }


                //define model view
                JobInProcessView view = new ModelViews.JobInProcessView()
                {
                    pcs_barcode = mps_in_process.pcs_barcode,
                    springtype_code = mps_in_process.springtype_code,
                    pdsize_desc = mps_in_process.pdsize_desc,
                    prod_code = mps_in_process.prod_code,
                    qty = 1,
                    //datas = new List<ModelViews.JobInProcessScanView>()
                };


                // Update PCS Barcode

                using (TransactionScope scope = new TransactionScope())
                {
                    mps_det_in_process updateObj = ctx.mps_in_process
                   .Where(z => z.PCS_BARCODE == mps_in_process.pcs_barcode
                        && z.ENTITY == model.entity
                        && z.WC_CODE == model.wc_code).SingleOrDefault();


                    updateObj.MPS_ST = "Y";
                    updateObj.FIN_BY = model.user_id;
                    updateObj.FIN_DATE = DateTime.Now;
                    updateObj.UPD_BY = model.user_id;
                    updateObj.UPD_DATE = DateTime.Now;
                    updateObj.PROCESS_TAG_QR = model.pcs_barcode;

                    ctx.Configuration.AutoDetectChangesEnabled = true;
                    ctx.SaveChanges();
                    scope.Complete();
                }


                ////////////////////////////////////////////////

                //DateTime vreq_date = DateTime.Now;

                //query data
                //string sql = "select a.PCS_BARCODE , a.PROD_CODE , b.PROD_TNAME , b.PDMODEL_DESC";
                //sql += " from MPS_DET_IN_PROCESS a , PRODUCT b";
                //sql += " where a.prod_code = b.prod_code";
                //sql += " and a.mps_st = 'Y'";
                //sql += " and a.fin_by = :p_user_id";
                //sql += " and trunc(a.fin_date) = trunc(SYSDATE)";
                //sql += " and a.entity = :p_entity";
                //sql += " and a.wc_code = :p_wc_code";
                //sql += " and a.mc_code = :p_mc_code";


                //List<JobInProcessScanView> scan = ctx.Database.SqlQuery<JobInProcessScanView>(sql, new OracleParameter("p_entity", model.entity), new OracleParameter("p_user_id", model.user_id), new OracleParameter("p_wc_code", model.wc_code), new OracleParameter("p_mc_code", model.mc_code)).ToList();



                //view.totalItem = scan.Count;
                //scan = scan.Skip(view.pageIndex * view.itemPerPage)
                //    .Take(view.itemPerPage)
                //    .ToList();

                //////prepare model to modelView
                //foreach (var i in scan)
                //{

                //    view.datas.Add(new ModelViews.JobInProcessScanView()
                //    {
                //        pcs_barcode = i.pcs_barcode,
                //        pdmodel_code = i.pdmodel_code,
                //        prod_code = i.prod_code,
                //        prod_name = i.prod_name

                //    });
                //}

                //return data to contoller
                return view;
            }
        }

        public JobInProcessScanFinView SerachCancelPcs(JobInProcessSearchView model)
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


                string sql = "select a.PCS_BARCODE , a.PROD_CODE , b.PROD_TNAME PROD_NAME, b.PDMODEL_DESC PDMODEL_CODE";
                sql += " from MPS_DET_IN_PROCESS a , PRODUCT b";
                sql += " where a.prod_code = b.prod_code";
                sql += " and a.mps_st = 'N'";
                sql += " and a.entity = :p_entity";
                sql += " and a.fin_by = :p_user_id";
                sql += " and trunc(a.fin_date) = trunc(SYSDATE)";
                sql += " and a.wc_code = :p_wc_code";
                sql += " and a.mc_code = :p_mc_code";
                sql += " and a.req_date = to_date(:p_req_date,'dd/mm/yyyy')";
                sql += " and a.spring_grp = :p_spring_grp";
                sql += " and a.pdsize_code = :p_pdsize_code";


                List<JobInProcessScanView> scan = ctx.Database.SqlQuery<JobInProcessScanView>(sql, new OracleParameter("p_entity", model.entity), new OracleParameter("p_user_id", model.user_id), new OracleParameter("p_wc_code", model.wc_code), new OracleParameter("p_mc_code", model.mc_code), new OracleParameter("p_req_date", model.req_date), new OracleParameter("p_spring_grp", model.spring_grp), new OracleParameter("p_pdsize_code", model.size_code)).ToList();



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
                    pageIndex = model.pageIndex -1,
                    itemPerPage = model.itemPerPage,
                    totalItem = 0,


                    datas = new List<ModelViews.JobInProcessScanView>()
                }; 


                string sql = "select a.PCS_BARCODE , a.PROD_CODE , b.PROD_TNAME PROD_NAME, b.PDMODEL_DESC PDMODEL_CODE";
                sql += " from MPS_DET_IN_PROCESS a , PRODUCT b";
                sql += " where a.prod_code = b.prod_code";
                sql += " and a.mps_st = 'Y'";
                sql += " and a.entity = :p_entity";
                sql += " and a.fin_by = :p_user_id";
                sql += " and trunc(a.fin_date) = trunc(SYSDATE)";
                sql += " and a.wc_code = :p_wc_code";
                sql += " and a.mc_code = :p_mc_code";
                sql += " and a.req_date = to_date(:p_req_date,'dd/mm/yyyy')";
                sql += " and a.spring_grp = :p_spring_grp";
                sql += " and a.pdsize_code = :p_pdsize_code";


                List<JobInProcessScanView> scan = ctx.Database.SqlQuery<JobInProcessScanView>(sql, new OracleParameter("p_entity", model.entity), new OracleParameter("p_user_id", model.user_id), new OracleParameter("p_wc_code", model.wc_code), new OracleParameter("p_mc_code", model.mc_code),new OracleParameter("p_req_date", model.req_date), new OracleParameter("p_spring_grp", model.spring_grp), new OracleParameter("p_pdsize_code", model.size_code)).ToList();



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

        //public void UpdatePcs(DataEntrySearchView model)
        //{
        //    using (var ctx = new ConXContext())
        //    {
        //        var ventity = model.entity;
        //        var vreq_date = model.req_date;
        //        var vwc_code = model.wc_code;
        //        var vmc_code = model.mc_code;
        //        var vuser_id = model.user_id;
        //        var vspring_grp = model.spring_grp;
        //        var vsize_code = model.size_code;
        //        var vqty = model.qty;

        //        string sqlc = "select count(*) from mps_det_in_process";
        //        sqlc += " where entity = :p_entity";
        //        sqlc += " and req_date=to_date(:p_req_date,'dd/mm/yyyy')";
        //        sqlc += " and wc_code = :p_wc_code";
        //        sqlc += " and mc_code = :p_mc_code";
        //        sqlc += " and pdsize_code = :p_size_code";
        //        sqlc += " and spring_grp = :p_spring_grp";
        //        sqlc += " and mps_st='N'";

        //        int cnt = ctx.Database.SqlQuery<int>(sqlc, new OracleParameter("p_entity", ventity),  new OracleParameter("p_req_date", vreq_date), new OracleParameter("p_wc_code", vwc_code), new OracleParameter("p_mc_code", vmc_code), new OracleParameter("p_size_code", vsize_code), new OracleParameter("p_spring_grp", vspring_grp)).FirstOrDefault(); ;


        //        if(vqty > cnt)
        //        {
        //            throw new Exception("บันทึกผลผลิตเกินจำนวน");
        //        }





        //        using (TransactionScope scope = new TransactionScope())
        //        {
        //            string strConn = ConfigurationManager.ConnectionStrings["OracleDbContext"].ConnectionString;
        //            var dataConn = new OracleConnectionStringBuilder(strConn);
        //            OracleConnection conn = new OracleConnection(dataConn.ToString());

        //            conn.Open();

        //            OracleCommand oraCommand = conn.CreateCommand();
        //            OracleParameter[] param = new OracleParameter[]
        //            {
        //                new OracleParameter("p_entity", ventity),
        //                new OracleParameter("p_req_date", vreq_date),
        //                new OracleParameter("p_wc_code", vwc_code),
        //                new OracleParameter("p_mc_code", vmc_code),
        //                new OracleParameter("p_size_code", vsize_code),
        //                new OracleParameter("p_spring_grp", vspring_grp),
        //                new OracleParameter("p_user_id", vuser_id),   
        //                new OracleParameter("p_qty", vqty)
        //            };
        //            oraCommand.BindByName = true;
        //            oraCommand.Parameters.AddRange(param);
        //            oraCommand.CommandText = "update MPS_DET_IN_PROCESS set mps_st='Y' , fin_by =:p_user_id , fin_date = SYSDATE , upd_by =:p_user_id , upd_date = SYSDATE where entity = :p_entity and req_date = to_date(:p_req_date,'dd/mm/yyyy') and wc_code =:p_wc_code and mc_code = :p_mc_code and pdsize_code = :p_size_code and spring_grp = :p_spring_grp and mps_st='N' and rownum <= :p_qty ";

        //            //oraCommand.ExecuteReader(CommandBehavior.SingleRow);
        //            oraCommand.ExecuteNonQuery();

        //            conn.Close();


        //            scope.Complete();
        //        }
        //    }


        //}

        //public JobInProcessView UpdatePcs(DataEntrySearchView model)
        public JobInProcessScanFinView UpdatePcs(DataEntrySearchView model)
        {
            using (var ctx = new ConXContext())
            {
                var ventity = model.entity;
                var vreq_date = model.req_date;
                var vwc_code = model.wc_code;
                var vmc_code = model.mc_code;
                var vuser_id = model.user_id;
                var vspring_grp = model.spring_grp;
                var vsize_code = model.size_code;
                var vqty = model.qty;

                JobInProcessScanFinView view = new ModelViews.JobInProcessScanFinView()
                {
                    pageIndex = 0,
                    itemPerPage = 10,
                    totalItem = 0,


                    datas = new List<ModelViews.JobInProcessScanView>()
                };

                string sqlc = "select count(*) from mps_det_in_process";
                sqlc += " where entity = :p_entity";
                sqlc += " and req_date=to_date(:p_req_date,'dd/mm/yyyy')";
                sqlc += " and wc_code = :p_wc_code";
                sqlc += " and mc_code = :p_mc_code";
                sqlc += " and pdsize_code = :p_size_code";
                sqlc += " and spring_grp = :p_spring_grp";
                sqlc += " and mps_st='N'";

                int cnt = ctx.Database.SqlQuery<int>(sqlc, new OracleParameter("p_entity", ventity), new OracleParameter("p_req_date", vreq_date), new OracleParameter("p_wc_code", vwc_code), new OracleParameter("p_mc_code", vmc_code), new OracleParameter("p_size_code", vsize_code), new OracleParameter("p_spring_grp", vspring_grp)).FirstOrDefault(); ;


                if (vqty > cnt)
                {
                    throw new Exception("บันทึกผลผลิตเกินจำนวน");
                }


                string sqlp = "select pcs_barcode , spring_grp springtype_code , pdsize_desc ,prod_code";
                sqlp += " from mps_det_in_process";
                sqlp += " where spring_grp = :p_spring_grp";
                sqlp += " and pdsize_code =  :p_size_code";
                sqlp += " and req_date = to_date(:p_req_date,'dd/mm/yyyy')";
                sqlp += " and entity = :p_entity";
                sqlp += " and wc_code =:p_wc_code";
                sqlp += " and mc_code =:p_mc_code";
                sqlp += " and mps_st =  'N'";
                sqlp += " and rownum <= :p_qty";

                List<JobInProcessScanView> mps_in_process = ctx.Database.SqlQuery<JobInProcessScanView>(sqlp, new OracleParameter("p_spring_grp", vspring_grp), new OracleParameter("p_size_code", vsize_code), new OracleParameter("p_req_date", model.req_date), new OracleParameter("p_entity", model.entity), new OracleParameter("p_wc_code", model.wc_code), new OracleParameter("p_mc_code", model.mc_code), new OracleParameter("p_qty", vqty)).ToList() ;


               


                using (TransactionScope scope = new TransactionScope())
                {
                    string strConn = ConfigurationManager.ConnectionStrings["OracleDbContext"].ConnectionString;
                    var dataConn = new OracleConnectionStringBuilder(strConn);
                    OracleConnection conn = new OracleConnection(dataConn.ToString());

                    conn.Open();

                    OracleCommand oraCommand = conn.CreateCommand();
                    OracleParameter[] param = new OracleParameter[]
                    {
                        new OracleParameter("p_entity", ventity),
                        new OracleParameter("p_req_date", vreq_date),
                        new OracleParameter("p_wc_code", vwc_code),
                        new OracleParameter("p_mc_code", vmc_code),
                        new OracleParameter("p_size_code", vsize_code),
                        new OracleParameter("p_spring_grp", vspring_grp),
                        new OracleParameter("p_user_id", vuser_id),
                        new OracleParameter("p_qty", vqty)
                    };
                    oraCommand.BindByName = true;
                    oraCommand.Parameters.AddRange(param);
                    oraCommand.CommandText = "update MPS_DET_IN_PROCESS set mps_st='Y' , fin_by =:p_user_id , fin_date = SYSDATE , upd_by =:p_user_id , upd_date = SYSDATE where entity = :p_entity and req_date = to_date(:p_req_date,'dd/mm/yyyy') and wc_code =:p_wc_code and mc_code = :p_mc_code and pdsize_code = :p_size_code and spring_grp = :p_spring_grp and mps_st='N' and rownum <= :p_qty ";

                    //oraCommand.ExecuteReader(CommandBehavior.SingleRow);
                    oraCommand.ExecuteNonQuery();

                    conn.Close();


                    scope.Complete();

                    foreach (var i in mps_in_process)
                    {

                        view.datas.Add(new ModelViews.JobInProcessScanView()
                        {
                            pcs_barcode = i.pcs_barcode,
                            //pdmodel_code = i.pdmodel_code,
                            prod_code = i.prod_code
                            //prod_name = i.prod_name

                        });
                    }
                }

                

                return view;
            }
            

        }

        public void CancelPcs(DataEntrySearchView model)
        {
            using (var ctx = new ConXContext())
            {
                var ventity = model.entity;
                var vreq_date = model.req_date;
                var vwc_code = model.wc_code;
                var vmc_code = model.mc_code;
                var vuser_id = model.user_id;
                var vspring_grp = model.spring_grp;
                var vsize_code = model.size_code;
                var vqty = model.qty;

                string sqlc = "select count(*) from mps_det_in_process";
                sqlc += " where entity = :p_entity";
                sqlc += " and req_date=to_date(:p_req_date,'dd/mm/yyyy')";
                sqlc += " and wc_code = :p_wc_code";
                sqlc += " and mc_code = :p_mc_code";
                sqlc += " and pdsize_code = :p_size_code";
                sqlc += " and spring_grp = :p_spring_grp";
                sqlc += " and mps_st='Y'";

                int cnt = ctx.Database.SqlQuery<int>(sqlc, new OracleParameter("p_entity", ventity), new OracleParameter("p_req_date", vreq_date), new OracleParameter("p_wc_code", vwc_code), new OracleParameter("p_mc_code", vmc_code), new OracleParameter("p_size_code", vsize_code), new OracleParameter("p_spring_grp", vspring_grp)).FirstOrDefault(); ;


                if (vqty > cnt)
                {
                    throw new Exception("ยกเลิกเกินจำนวน");
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
                        new OracleParameter("p_entity", ventity),
                        new OracleParameter("p_req_date", vreq_date),
                        new OracleParameter("p_wc_code", vwc_code),
                        new OracleParameter("p_mc_code", vmc_code),
                        new OracleParameter("p_size_code", vsize_code),
                        new OracleParameter("p_spring_grp", vspring_grp),
                        new OracleParameter("p_user_id", vuser_id),
                        new OracleParameter("p_qty", vqty)
                    };
                    oraCommand.BindByName = true;
                    oraCommand.Parameters.AddRange(param);
                    oraCommand.CommandText = "update MPS_DET_IN_PROCESS set mps_st='N' , fin_by =:p_user_id , fin_date = SYSDATE , upd_by =:p_user_id , upd_date = SYSDATE where entity = :p_entity and req_date = to_date(:p_req_date,'dd/mm/yyyy') and wc_code =:p_wc_code and mc_code = :p_mc_code and pdsize_code = :p_size_code and spring_grp = :p_spring_grp and mps_st='Y' and rownum <= :p_qty ";

                    //oraCommand.ExecuteReader(CommandBehavior.SingleRow);
                    oraCommand.ExecuteNonQuery();

                    conn.Close();


                    scope.Complete();
                }
            }
        }


    }
}