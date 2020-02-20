using api.DataAccess;
using api.Interfaces;
using api.ModelViews;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace api.Services
{
    public class PrintTagService : IPrintTagService
    {
        //public void AddTag(PrintTagAddView model)
        //{
        //    throw new NotImplementedException();
        //}

        public void DeleteTag(PrintTagProcView model)
        {
            throw new NotImplementedException();
        }

        public void PringTag(PrintTagView model)
        {
            using (var ctx = new ConXContext())
            {

                var ventity = model.entity;
                var vprocess_tag_no = model.process_tag_no;
                var vreq_date = model.req_date;
                var vmc_code = model.mc_code;
                var vqty = model.qty;
                var vprinter = model.printer;
                var vfin_date = DateTime.Now.ToShortDateString();
                var vspring_grp = model.spring_grp;
                var vsize_desc = model.size_desc;


                foreach (var i in model.datas)
                {

                    var vdoc_no = i.doc_no;

                }   
                    

                    string[] lines = { "New line 1", "New line 2" };

                //string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string docPath = "d:\\data";

                // Append new lines of text to the file
                File.AppendAllLines(Path.Combine(docPath, "WriteFile.txt"), lines);
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
                    vprocess_tag_no = tag.process_tag_no + 1;

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