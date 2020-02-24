using api.DataAccess;
using api.Interfaces;
using api.Models;
using api.ModelViews;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Transactions;
using System.Web;

namespace api.Services
{
    public class PrintTagService : IPrintTagService
    {
        public PrintTagView AddTag(PrintTagSearchView model)
        {
            using (var ctx = new ConXContext())
            {

                var ventity = model.entity;
                var vreq_date = model.req_date;
                var vwc_code = model.wc_code;
                var vmc_code = model.mc_code;
                var vspring_grp = model.spring_grp;
                var vsize_desc = model.size_desc;
                var vqty = model.qty;
                var vuser_id = model.user_id;
                var vprinter = model.printer;
                //int vprocess_tag_no = 0;

                PrintTagView view = new ModelViews.PrintTagView()
                {
                    datas = new List<ModelViews.RawMatitemView>()
                };

                string sqlp = "select prnt_point_name from whmobileprnt_ctl where grp_type='SPRING' and series_no=:p_printer";
                string vprinter_name = ctx.Database.SqlQuery<string>(sqlp, new OracleParameter("p_printer", vprinter)).FirstOrDefault();


                string sqls = "select pdsize_tname from pdsize_mast where pdsize_code=:p_size_code";
                string vsize_name = ctx.Database.SqlQuery<string>(sqls, new OracleParameter("p_size_code", vsize_desc)).FirstOrDefault();


                string sql = "select max(process_tag_no) process_tag_no , max(to_char(req_date,'dd/mm/yyyy')) req_date , max(mc_code) , max(to_char(fin_date,'dd/mm/yyyy')) fin_date";
                sql += " from mps_det_in_process_tag";
                sql += " where entity = :p_entity";
                sql += " and req_date = to_date(:p_req_date,'dd/mm/yyyy')";
                sql += " and mc_code = :p_mc_code";
               // sql += " and rownum = 1";
                sql += " order by process_tag_no";


                PrintTagView tag = ctx.Database.SqlQuery<PrintTagView>(sql, new OracleParameter("p_entity", ventity), new OracleParameter("p_req_date", vreq_date), new OracleParameter("p_mc_code", vmc_code)).SingleOrDefault();


                if (tag == null)
                {

                    view.entity = ventity;
                    view.process_tag_no = 1;
                    view.req_date = vreq_date;
                    view.wc_code = vwc_code;
                    view.mc_code = vmc_code;
                    view.spring_grp = vspring_grp;
                    view.size_desc = vsize_name;
                    view.qty = vqty;
                    view.fin_date = DateTime.Now.ToShortDateString();
                    view.printer = vprinter_name;

                }
                else
                {


                    view.entity = ventity;
                    view.process_tag_no = tag.process_tag_no + 1;
                    view.req_date = tag.req_date;
                    view.wc_code = tag.wc_code;
                    view.mc_code = vmc_code;
                    view.spring_grp = vspring_grp;
                    view.size_desc = vsize_name;
                    view.qty = vqty;
                    view.fin_date = tag.fin_date;
                    view.printer = vprinter_name;
                }



                //return data to contoller
                return view;
            }
        }

        public void DeleteTag(PrintTagProcView model)
        {
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
                        new OracleParameter("p_req_date", model.req_date),
                        new OracleParameter("p_mc_code", model.mc_code),
                        new OracleParameter("p_process_tag_no", model.process_tag_no)
                };
                oraCommand.BindByName = true;
                oraCommand.Parameters.AddRange(param);
                oraCommand.CommandText = "delete MPS_DET_IN_PROCESS_TAG where entity = :p_entity and req_date = to_date(:p_req_date,'dd/mm/yyyy') and mc_code =:p_mc_code and process_tag_no  = :p_process_tag_no";

                //oraCommand.ExecuteReader(CommandBehavior.SingleRow);
                oraCommand.ExecuteNonQuery();

                conn.Close();


                scope.Complete();
            }
        }

        public void PringTag(PrintTagView model)
        {
            using (var ctx = new ConXContext())
            {

                var ventity = model.entity;
                var vprocess_tag_no = model.process_tag_no;
                //var vreq_date = model.req_date;
                var vmc_code = model.mc_code;
                var vqty = model.qty;
                var vprinter = model.printer;
                var vfin_date = DateTime.Now.ToShortDateString();
                var vspring_grp = model.spring_grp;
                var vsize_desc = model.size_desc;
                var vuser_id = model.user_id;
                DateTime vreq_date = Convert.ToDateTime(model.req_date);
                //int vitem = 1;


                string sqli = "select count(seq_no)+1 from mps_det_in_process_tag where entity=:p_entity and req_date = to_date(:p_req_date,'dd/mm/yyyy') and mc_code = :p_mc_code";
                int item = ctx.Database.SqlQuery<int>(sqli, new OracleParameter("p_entity", ventity), new OracleParameter("p_req_date", model.req_date), new OracleParameter("p_mc_code", vmc_code)).SingleOrDefault();

                string sql = "select prod_code from mps_det_in_process_tag where entity=:p_entity and req_date = to_date(:p_req_date,'dd/mm/yyyy') and mc_code = :p_mc_code and process_tag_no = :p_process_tag_no and rownum = 1";
                string chk_tag = ctx.Database.SqlQuery<string>(sql, new OracleParameter("p_entity", ventity), new OracleParameter("p_req_date", model.req_date), new OracleParameter("p_mc_code", vmc_code), new OracleParameter("p_process_tag_no", vprocess_tag_no)).SingleOrDefault();

                if(chk_tag == null)
                {
                    using (TransactionScope scope = new TransactionScope())
                    {

                        mps_det_in_process_tag tag = ctx.mps_tag
                        .Where(z => z.ENTITY == ventity
                            && System.Data.Entity.DbFunctions.TruncateTime(z.REQ_DATE) == System.Data.Entity.DbFunctions.TruncateTime(vreq_date)
                            && z.MC_CODE == vmc_code && z.PROCESS_TAG_NO == vprocess_tag_no).SingleOrDefault();

                


                        if (tag == null)
                        {

                            foreach (var i in model.datas)
                            {
                                if (i.process_tag_no == vprocess_tag_no)
                                {
                                    mps_det_in_process_tag newObj = new mps_det_in_process_tag()
                                    {
                                        ENTITY = ventity,
                                        REQ_DATE = vreq_date,
                                        FIN_DATE = DateTime.Now,
                                        MC_CODE = vmc_code,
                                        PROCESS_TAG_NO = vprocess_tag_no,
                                        PROCESS_TAG_QR = "",
                                        SEQ_NO = item,
                                        REF_DOC_NO = i.doc_no,
                                        PROD_CODE = i.prod_code,
                                        PROD_TNAME = i.prod_name,
                                        UPD_BY = vuser_id,
                                        UPD_DATE = DateTime.Now

                                    };

                                    item++;

                                    ctx.mps_tag.Add(newObj);
                                    ctx.SaveChanges();
                                    //scope.Complete();
                                }

                            }
                            scope.Complete();

                        }

                    }
                }

                

                var vdoc_no = "";
                var vprod_code = "";
                string[] tag_sticker;
                tag_sticker = new string[vqty];

                string sqlp = "select filepath_txt from whmobileprnt_ctl where series_no =:p_printer";
                //string docPath = ctx.Database.SqlQuery<string>(sqlp, new OracleParameter("p_printer", vprinter)).SingleOrDefault();
                //string docPath = "d:\\data\\trigger.txt";
                string docPath = "L:\\PRINT_POINT7\\trigger.txt";



                foreach (var k in model.datas)
                {
                    if (k.process_tag_no == vprocess_tag_no)
                    {
                        vdoc_no = vdoc_no + "," + k.doc_no;
                        vprod_code = vprod_code + "," + k.prod_code;
                    }
                }

                System.Diagnostics.Process.Start("net.exe", @"use L: \\192.168.8.14\Data");
                string all_txt = "";
                string txt = vspring_grp + "|" + vsize_desc + "|" + vdoc_no.Trim(',') + "|" + vprod_code.Trim(',') + "|" + model.req_date + "|" + vfin_date;


                for (int j = 0; j < vqty; j++)
                {
                    //tag_sticker[j] = txt;
                    if (j == 0)
                    {
                        all_txt = txt + Environment.NewLine; ; 
                    }
                    else
                    {
                        all_txt = all_txt + txt + Environment.NewLine;
                    }

                }
                
                File.WriteAllText(Path.Combine(docPath), all_txt);

                System.Diagnostics.Process.Start("net.exe", @"use L: / delete");


            }
        }

        public PrintTagView searchPrintData(PrintTagSearchView model)
        {
            using (var ctx = new ConXContext())
            {

                var ventity = model.entity;
                var vreq_date = model.req_date;
                var vwc_code = model.wc_code;
                var vmc_code = model.mc_code;
                var vspring_grp = model.spring_grp;
                var vsize_desc = model.size_desc;
                var vqty = model.qty;
                var vuser_id = model.user_id;
                var vprinter = model.printer;
                int vprocess_tag_no = 0;

                PrintTagView view = new ModelViews.PrintTagView()
                {
                    datas = new List<ModelViews.RawMatitemView>()
                };

                string sqlp = "select prnt_point_name from whmobileprnt_ctl where grp_type='SPRING' and series_no=:p_printer";
                string vprinter_name = ctx.Database.SqlQuery<string>(sqlp, new OracleParameter("p_printer", vprinter)).FirstOrDefault();


                string sqls = "select pdsize_tname from pdsize_mast where pdsize_code=:p_size_code";
                string vsize_name = ctx.Database.SqlQuery<string>(sqls, new OracleParameter("p_size_code", vsize_desc)).FirstOrDefault();


                //string sql = "select max(process_tag_no) process_tag_no , max(to_char(req_date,'dd/mm/yyyy')) req_date , max(mc_code) , max(to_char(fin_date,'dd/mm/yyyy')) fin_date";
                string sql = "select process_tag_no , to_char(req_date,'dd/mm/yyyy') req_date , mc_code , to_char(fin_date,'dd/mm/yyyy') fin_date";
                sql += " from mps_det_in_process_tag";
                sql += " where entity = :p_entity";
                sql += " and req_date = to_date(:p_req_date,'dd/mm/yyyy')";
                sql += " and mc_code = :p_mc_code";
                sql += " and rownum = 1";
                sql += " order by process_tag_no desc";


                PrintTagView tag = ctx.Database.SqlQuery<PrintTagView>(sql, new OracleParameter("p_entity", ventity), new OracleParameter("p_req_date", vreq_date), new OracleParameter("p_mc_code", vmc_code)).SingleOrDefault();


                if (tag == null)
                {
                    vprocess_tag_no = 1;

                    view.entity = ventity;
                    view.process_tag_no = vprocess_tag_no;
                    view.req_date = vreq_date;
                    view.wc_code = vwc_code;
                    view.mc_code = vmc_code;
                    view.spring_grp = vspring_grp;
                    view.size_desc = vsize_name;
                    view.qty = vqty;
                    view.fin_date = DateTime.Now.ToShortDateString();
                    view.printer = vprinter_name;

                }
                else
                {
                    //vprocess_tag_no = tag.process_tag_no;

                    string sqlt = "select max(process_tag_no)";
                    sqlt += " from mps_det_in_process_tag";
                    sqlt += " where entity = :p_entity";
                    sqlt += " and req_date = to_date(:p_req_date,'dd/mm/yyyy')";
                    sqlt += " and mc_code = :p_mc_code";
                    sqlt += " order by process_tag_no desc";


                    vprocess_tag_no = ctx.Database.SqlQuery<int>(sqlt, new OracleParameter("p_entity", ventity), new OracleParameter("p_req_date", vreq_date), new OracleParameter("p_mc_code", vmc_code)).SingleOrDefault();


                    view.entity = ventity;
                    view.process_tag_no = vprocess_tag_no;
                    view.req_date = tag.req_date;
                    view.wc_code = tag.wc_code;
                    view.mc_code = vmc_code;
                    view.spring_grp = vspring_grp;
                    view.size_desc = vsize_name;
                    view.qty = vqty;
                    view.fin_date = tag.fin_date;
                    view.printer = vprinter_name;
                }



                string sqlr = "select process_tag_no , ref_doc_no doc_no , prod_code , prod_tname prod_name";
                sqlr += " from mps_det_in_process_tag";
                sqlr += " where entity = :p_entity";
                sqlr += " and req_date = to_date(:p_req_date,'dd/mm/yyyy')";
                sqlr += " and mc_code = :p_mc_code";
                sqlr += " and process_tag_no = :p_process_tag_no";
                sqlr += " order by seq_no";

                List<RawMatitemView> scan = ctx.Database.SqlQuery<RawMatitemView>(sqlr, new OracleParameter("p_entity", ventity), new OracleParameter("p_req_date", vreq_date), new OracleParameter("p_mc_code", vmc_code), new OracleParameter("p_process_tag_no", vprocess_tag_no)).ToList();

                foreach (var i in scan)
                {

                    view.datas.Add(new ModelViews.RawMatitemView()
                    {
                        process_tag_no = i.process_tag_no,
                        doc_no = i.doc_no,
                        prod_code = i.prod_code,
                        prod_name = i.prod_name

                    });
                }


                //return data to contoller
                return view;
            }
        }

        public PrintTagView searchProcessTagNo(ProcessTagNoSearch model)
        {
            using (var ctx = new ConXContext())
            {

                var ventity = model.entity;
                var vreq_date = model.req_date;
                var vwc_code = model.wc_code;
                var vmc_code = model.mc_code;
                var vspring_grp = model.spring_grp;
                var vsize_desc = model.size_desc;
                var vqty = model.qty;
                //var vuser_id = model.user_id;
                var vprinter = model.printer;
                int vprocess_tag_no = model.process_tag_no;

                PrintTagView view = new ModelViews.PrintTagView()
                {
                    datas = new List<ModelViews.RawMatitemView>()
                };

                string sqlp = "select prnt_point_name from whmobileprnt_ctl where grp_type='SPRING' and series_no=:p_printer";
                string vprinter_name = ctx.Database.SqlQuery<string>(sqlp, new OracleParameter("p_printer", vprinter)).FirstOrDefault();


                string sqls = "select pdsize_tname from pdsize_mast where pdsize_code=:p_size_code";
                string vsize_name = ctx.Database.SqlQuery<string>(sqls, new OracleParameter("p_size_code", vsize_desc)).FirstOrDefault();


                //string sql = "select max(process_tag_no) process_tag_no , max(to_char(req_date,'dd/mm/yyyy')) req_date , max(mc_code) , max(to_char(fin_date,'dd/mm/yyyy')) fin_date";
                string sql = "select process_tag_no , to_char(req_date,'dd/mm/yyyy') req_date , mc_code , to_char(fin_date,'dd/mm/yyyy') fin_date";
                sql += " from mps_det_in_process_tag";
                sql += " where entity = :p_entity";
                sql += " and req_date = to_date(:p_req_date,'dd/mm/yyyy')";
                sql += " and mc_code = :p_mc_code";
                sql += " and process_tag_no = :p_process_tag_no";
                sql += " order by process_tag_no desc";


                PrintTagView tag = ctx.Database.SqlQuery<PrintTagView>(sql, new OracleParameter("p_entity", ventity), new OracleParameter("p_req_date", vreq_date), new OracleParameter("p_mc_code", vmc_code), new OracleParameter("p_process_tag_no", vprocess_tag_no)).SingleOrDefault();


                if (tag == null)
                {
                    vprocess_tag_no = 1;

                    view.entity = ventity;
                    view.process_tag_no = vprocess_tag_no;
                    view.req_date = vreq_date;
                    view.wc_code = vwc_code;
                    view.mc_code = vmc_code;
                    view.spring_grp = vspring_grp;
                    view.size_desc = vsize_name;
                    view.qty = vqty;
                    view.fin_date = DateTime.Now.ToShortDateString();
                    view.printer = vprinter_name;

                }
                else
                {
                    //vprocess_tag_no = tag.process_tag_no;

                    

                    view.entity = ventity;
                    view.process_tag_no = tag.process_tag_no;
                    view.req_date = tag.req_date;
                    view.wc_code = tag.wc_code;
                    view.mc_code = vmc_code;
                    view.spring_grp = vspring_grp;
                    view.size_desc = vsize_name;
                    view.qty = vqty;
                    view.fin_date = tag.fin_date;
                    view.printer = vprinter_name;
                }



                string sqlr = "select process_tag_no , ref_doc_no doc_no , prod_code , prod_tname prod_name";
                sqlr += " from mps_det_in_process_tag";
                sqlr += " where entity = :p_entity";
                sqlr += " and req_date = to_date(:p_req_date,'dd/mm/yyyy')";
                sqlr += " and mc_code = :p_mc_code";
                sqlr += " and process_tag_no = :p_process_tag_no";
                sqlr += " order by seq_no";

                List<RawMatitemView> scan = ctx.Database.SqlQuery<RawMatitemView>(sqlr, new OracleParameter("p_entity", ventity), new OracleParameter("p_req_date", vreq_date), new OracleParameter("p_mc_code", vmc_code), new OracleParameter("p_process_tag_no", vprocess_tag_no)).ToList();

                foreach (var i in scan)
                {

                    view.datas.Add(new ModelViews.RawMatitemView()
                    {
                        process_tag_no = i.process_tag_no,
                        doc_no = i.doc_no,
                        prod_code = i.prod_code,
                        prod_name = i.prod_name

                    });
                }


                //return data to contoller
                return view;
            }
        }

        public CommonSearchView<ProcessTagView>  searchProcessTagNoList(ProcessTagSearchView model)
        {
            using (var ctx = new ConXContext())
            {
                var ventity = model.entity;
                var vreq_date = model.req_date;
                var vmc_code = model.mc_code;

                CommonSearchView<ProcessTagView> view = new ModelViews.CommonSearchView<ModelViews.ProcessTagView>()
                {
                    


                    datas = new List<ModelViews.ProcessTagView>()
                };


                string sql = "select distinct process_tag_no";
                sql += " from mps_det_in_process_tag";
                sql += " where entity = :p_entity";
                sql += " and req_date = to_date(:p_req_date,'dd/mm/yyyy')";
                sql += " and mc_code = :p_mc_code";
                

                List<ProcessTagView> tag_no = ctx.Database.SqlQuery<ProcessTagView>(sql, new OracleParameter("p_entity", ventity), new OracleParameter("p_req_date", vreq_date), new OracleParameter("p_mc_code", vmc_code)).ToList();


                foreach (var i in tag_no)
                {

                    view.datas.Add(new ModelViews.ProcessTagView()
                    {

                        //ic_entity = i.IC_ENTITY,
                        process_tag_no = i.process_tag_no,
                        //wh_code = i.WH_CODE,
                        //qty_oh = i.QTY_OH,
                        //qty_all = i.QTY_ALL,
                        //qty_ship = i.QTY_SHIP,
                        //qty_build = i.QTY_BUILD_DISPLAY,
                        //qty_avai = i.QTY_AVAI

                    });
                }

                //ProcessTagView view = new ModelViews.ProcessTagView();


                //foreach (var i in tag_no)
                //{
                //    view.process_tag_no = i.process_tag_no;

                //}


                //return data to contoller
                return view;
            }
        }



       

        public RawMatView searchRawData(RawMatSearchView model)
        {
            using (var ctx = new ConXContext())
            {
                RawMatView view = new ModelViews.RawMatView()
                {
                    pageIndex = model.pageIndex - 1,
                    itemPerPage = model.itemPerPage,
                    totalItem = 0,
                    //entity = model.entity,
                    //req_date = model.req_date,
                    //mc_code = model.mc_code,
                    //process_tag_no = model.process_tag_no,

                    datas = new List<ModelViews.RawProductView>()
                };

                var vdoc_date = model.doc_date;

                string sql = "select distinct a.entity , a.doc_code , a.doc_no , a.doc_date , b.prod_code , c.prod_tname prod_name ";
                sql += " from sr_mast a , sr_det b , product c";
                sql += " where a.entity = b.entity";
                sql += " and a.doc_code = b.doc_code";
                sql += " and a.doc_no = b.doc_no";
                sql += " and b.prod_code = c.prod_code";
                sql += " and a.entity='B01'";
                sql += " and a.doc_code ='RC'";
                sql += " and a.type='RVD'";
                sql += " and a.doc_date = to_date(:p_doc_date,'dd/mm/yyyy')";
                sql += " and c.pdtype_code='AB'";
                sql += " and a.doc_status = 'APV'";
                sql += " order by a.doc_date desc , a.doc_no , b.prod_code";

                List<RawProductView> raw = ctx.Database.SqlQuery<RawProductView>(sql, new OracleParameter("p_doc_date", vdoc_date)).ToList();


                //RawMatView view = new ModelViews.RawMatView();


                //foreach (var i in raw)
                //{
                //    view.doc_no = i.doc_no;
                //    view.prod_code = i.prod_code;
                //    view.prod_name = i.prod_name;
                //}


                view.totalItem = raw.Count;
                raw = raw.Skip(view.pageIndex * view.itemPerPage)
                    .Take(view.itemPerPage)
                    .ToList();

                ////prepare model to modelView
                foreach (var i in raw)
                {

                    view.datas.Add(new ModelViews.RawProductView()
                    {
                        doc_no = i.doc_no,
                        prod_code = i.prod_code,
                        prod_name = i.prod_name

                    });
                }



                return view;
            }
        }
    }

       
    }