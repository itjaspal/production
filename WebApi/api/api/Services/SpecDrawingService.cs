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
    public class SpecDrawingService : ISpecDrawingService
    {
        public SpecDrawingView GetInfo(SpecDrawingSearchView model)
        {
            using (var ctx = new ConXContext())
            {
                var ventity = model.entity;
                var vreq_date = model.req_date;
                var vspringtype_code = model.springtype_code;
                var vpdsize_code = model.pdsize_code;


                string sql = "select a.entity , a.pcs_barcode , a.springtype_code , a.pdsize_desc  , a.wc_code, a.prod_code , b.prod_tname prod_name , b.pdmodel_desc ";
                sql += " from mps_det_in_process a , product b";
                sql += " where a.prod_code = b.prod_code";
                sql += " and a.entity = :p_entity";
                sql += " and a.req_date = to_date(:p_req_date,'dd/mm/yyyy')";
                sql += " and a.springtype_code = :p_springtype_code";
                sql += " and a.pdsize_code = :p_pdsize_code";
                sql += " and rownum = 1";


                PcsBarcodeDataView datas = ctx.Database.SqlQuery<PcsBarcodeDataView>(sql, new OracleParameter("p_entity", ventity), new OracleParameter("p_req_date", vreq_date), new OracleParameter("p_springtype_code", vspringtype_code), new OracleParameter("p_pdsize_code", vpdsize_code)).SingleOrDefault();



                if (datas == null)
                {
                    throw new Exception("ไม่พบข้อมูล");
                }


                string sqls = "select spring_pic_file from pdspring_mast where springtype_code = :p_springtype_code";
                string spring_file  = ctx.Database.SqlQuery<string>(sqls, new OracleParameter("p_springtype_code", datas.springtype_code)).SingleOrDefault();

                if(spring_file is null)
                {
                    throw new Exception("ไม่มีการกำหนดรูปให้กับ Spring Type นี้");
                }

                //System.Diagnostics.Process.Start("net.exe", @"use Z: \\128.1.1.23\prog").WaitForExit();

                string sqlp = "select spring_path from bm_basic_mast";
                string spring_path = ctx.Database.SqlQuery<string>(sqlp).SingleOrDefault();

                string imagePath = @spring_path+spring_file;
                //string imagePath = @"\\128.1.1.23\prog\Picture\Spring\" + spring_file;

                //string imgBase64String = GetBase64StringForImage(imagePath);
                string imgBase64String = "";




                //define model view
                SpecDrawingView view = new ModelViews.SpecDrawingView()
                {
                    pageIndex = model.pageIndex - 1,
                    itemPerPage = model.itemPerPage,
                    totalItem = 0,

                    prod_code = datas.prod_code,
                    prod_name = datas.prod_name,
                    model_desc = datas.pdmodel_desc,
                    spring_type = datas.springtype_code,
                    size_desc = datas.pdsize_desc,
                    drawing_path = imagePath,
                    //drawing_name = spring_file,
                    drawing_name = imgBase64String,

                    datas = new List<ModelViews.RawMatListView>()
                };

                string sqlr = "select bom_seq , bom_sub , bom_name , rm_seq , rm_code , rm_name , short_name , uom_code , unit_qty , item_name from mps_mr_pcs where entity = :p_entity and pcs_barcode = :p_pcs_barcode and dept_bom = :p_wc_code order by bom_seq , rm_seq";

                List<RawMatListView> rawmat = ctx.Database.SqlQuery<RawMatListView>(sqlr , new OracleParameter("p_entity", datas.entity), new OracleParameter("p_pcs_barcode", datas.pcs_barcode), new OracleParameter("p_wc_code", datas.wc_code))
                                           .Select(x => new RawMatListView()
                                           {
                                               bom_seq = x.bom_seq,
                                               bom_sub = x.bom_sub,
                                               bom_name = x.bom_name,
                                               rm_seq = x.rm_seq,
                                               rm_code = x.rm_code,
                                               rm_name = x.rm_name,
                                               short_name = x.short_name,
                                               uom_code = x.uom_code,
                                               unit_qty = x.unit_qty,
                                               item_name = x.item_name
                                           })
                                           .ToList();

                //List<RawMatListView> rawmat = ctx.mr_pcs
                //.Where(z => z.ENTITY == datas.entity && z.PCS_BARCODE == datas.pcs_barcode && z.DEPT_BOM == datas.wc_code)
                //.OrderBy(z => z.BOM_SEQ).ThenBy(z => z.RM_SEQ)
                //.Select(x => new RawMatListView()
                //{

                //    bom_seq = x.BOM_SEQ,
                //    bom_sub = x.BOM_SUB,
                //    bom_name = x.BOM_NAME,
                //    rm_seq = x.RM_SEQ,
                //    rm_code = x.RM_CODE,
                //    rm_name = x.RM_NAME,
                //    short_name = x.SHORT_NAME,
                //    uom_code = x.UOM_CODE,
                //    unit_qty = x.UNIT_QTY,
                //    item_name = x.ITEM_NAME
                //})
                //.ToList();


                view.totalItem = rawmat.Count;
                rawmat = rawmat.Skip(view.pageIndex * view.itemPerPage)
                    .Take(view.itemPerPage)
                    .ToList();


                foreach (var i in rawmat)
                {

                    view.datas.Add(new ModelViews.RawMatListView()
                    {
                        bom_seq = i.bom_seq,
                        bom_sub = i.bom_sub,
                        bom_name = i.bom_name,
                        rm_seq = i.rm_seq,
                        rm_code = i.rm_code,
                        rm_name = i.rm_name,
                        short_name = i.short_name,
                        uom_code = i.uom_code,
                        unit_qty = i.unit_qty,
                        item_name = i.item_name
                    });
                }


                //return data to contoller
                return view;

            }
        }

        

        public SpecDrawingView GetPcsInfo(SpecDrawingSearchPcsView model)
        {
            using (var ctx = new ConXContext())
            {
                var vpcs_barcode = model.pcs_barcode;
                
                string sql = "select a.entity , a.pcs_barcode , a.springtype_code , a.pdsize_desc  , a.wc_code, a.prod_code , b.prod_tname prod_name , b.pdmodel_desc";
                sql += " from mps_det_in_process a , product b";
                sql += " where a.prod_code = b.prod_code";
                sql += " and a.entity = 'B10'";
                sql += " and a.pcs_barcode = :p_pcs_barcode";
                //sql += " and a.springtype_code = :p_springtype_code";
                //sql += " and a.pdsize_code = :p_pdsize_code";
                sql += " and rownum = 1";


                PcsBarcodeDataView datas = ctx.Database.SqlQuery<PcsBarcodeDataView>(sql, new OracleParameter("p_pcs_barcode", vpcs_barcode)).SingleOrDefault();



                if (datas == null)
                {
                    throw new Exception("ไม่พบข้อมูล");
                }


                string sqls = "select spring_pic_file from pdspring_mast where springtype_code = :p_springtype_code";
                string spring_file = ctx.Database.SqlQuery<string>(sqls, new OracleParameter("p_springtype_code", datas.springtype_code)).SingleOrDefault();


                string sqlp = "select spring_path from bm_basic_mast";
                string spring_path = ctx.Database.SqlQuery<string>(sqlp).SingleOrDefault();

                string imagePath = @spring_path + spring_file;
                string imgBase64String = GetBase64StringForImage(imagePath);



                //define model view
                SpecDrawingView view = new ModelViews.SpecDrawingView()
                {
                    pageIndex = model.pageIndex - 1,
                    itemPerPage = model.itemPerPage,
                    totalItem = 0,

                    prod_code = datas.prod_code,
                    prod_name = datas.prod_name,
                    model_desc = datas.pdmodel_desc,
                    spring_type = datas.springtype_code,
                    size_desc = datas.pdsize_desc,
                    drawing_path = imagePath,
                    //drawing_name = spring_file,
                    drawing_name = imgBase64String,

                    datas = new List<ModelViews.RawMatListView>()
                };



                //List<RawMatListView> rawmat = ctx.mr_pcs
                //.Where(z => z.ENTITY == datas.entity && z.PCS_BARCODE == datas.pcs_barcode && z.DEPT_BOM == datas.wc_code)
                //.OrderBy(z => z.BOM_SEQ).ThenBy(z => z.RM_SEQ)
                //.Select(x => new RawMatListView()
                //{

                //    bom_seq = x.BOM_SEQ,
                //    bom_sub = x.BOM_SUB,
                //    bom_name = x.BOM_NAME,
                //    rm_seq = x.RM_SEQ,
                //    rm_code = x.RM_CODE,
                //    rm_name = x.RM_NAME,
                //    short_name = x.SHORT_NAME,
                //    uom_code = x.UOM_CODE,
                //    unit_qty = x.UNIT_QTY,
                //    item_name = x.ITEM_NAME
                //})
                //.ToList();

                string sqlr = "select bom_seq , bom_sub , bom_name , rm_seq , rm_code , rm_name , short_name , uom_code , unit_qty , item_name from mps_mr_pcs where entity = :p_entity and pcs_barcode = :p_pcs_barcode and dept_bom = :p_wc_code order by bom_seq , rm_seq";

                List<RawMatListView> rawmat = ctx.Database.SqlQuery<RawMatListView>(sqlr, new OracleParameter("p_entity", datas.entity), new OracleParameter("p_pcs_barcode", datas.pcs_barcode), new OracleParameter("p_wc_code", datas.wc_code))
                                           .Select(x => new RawMatListView()
                                           {
                                               bom_seq = x.bom_seq,
                                               bom_sub = x.bom_sub,
                                               bom_name = x.bom_name,
                                               rm_seq = x.rm_seq,
                                               rm_code = x.rm_code,
                                               rm_name = x.rm_name,
                                               short_name = x.short_name,
                                               uom_code = x.uom_code,
                                               unit_qty = x.unit_qty,
                                               item_name = x.item_name
                                           })
                                           .ToList();


                view.totalItem = rawmat.Count;
                rawmat = rawmat.Skip(view.pageIndex * view.itemPerPage)
                    .Take(view.itemPerPage)
                    .ToList();


                foreach (var i in rawmat)
                {

                    view.datas.Add(new ModelViews.RawMatListView()
                    {
                        bom_seq = i.bom_seq,
                        bom_sub = i.bom_sub,
                        bom_name = i.bom_name,
                        rm_seq = i.rm_seq,
                        rm_code = i.rm_code,
                        rm_name = i.rm_name,
                        short_name = i.short_name,
                        uom_code = i.uom_code,
                        unit_qty = i.unit_qty,
                        item_name = i.item_name
                    });
                }


                //return data to contoller
                return view;

            }
        }

        protected static string GetBase64StringForImage(string imgPath)
        {
            byte[] imageBytes = System.IO.File.ReadAllBytes(imgPath);
            string base64String = Convert.ToBase64String(imageBytes);
            return base64String;
        }
    }
}