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

                mps_det_in_process mps_in_process = ctx.mps_in_process
                    .Where(z => z.PCS_BARCODE == model.pcs_barcode)
                    .SingleOrDefault();


                if (mps_in_process == null)
                {
                    throw new Exception("PSC Barcodeไม่ถูกต้อง");
                }

               

                product prod = ctx.product
                    .Where(z => z.PROD_CODE == mps_in_process.PROD_CODE)
                    .SingleOrDefault();


                pdspring_mast spring_file = ctx.pdspring
                    .Where(z => z.SPRINGTYPE_CODE == mps_in_process.SPRINGTYPE_CODE)
                    .SingleOrDefault();


                bm_basic_mast spring_path = ctx.bm_basic
                    .SingleOrDefault();




                //define model view
                SpecDrawingView view = new ModelViews.SpecDrawingView()
                {
                    pageIndex = model.pageIndex - 1,
                    itemPerPage = model.itemPerPage,
                    totalItem = 0,

                    prod_code = mps_in_process.PROD_CODE,
                    prod_name = prod.PROD_TNAME,
                    model_desc = prod.PDMODEL_DESC,
                    spring_type = mps_in_process.SPRINGTYPE_CODE,
                    size_desc = mps_in_process.PDSIZE_DESC,
                    drawing_path = spring_path.SPRING_PATH,
                    drawing_name = spring_file.SPRING_PIC_FILE,

                    datas = new List<ModelViews.RawMatListView>()
                };

               

                List<RawMatListView> rawmat = ctx.mr_pcs
                .Where(z => z.ENTITY == mps_in_process.ENTITY && z.PCS_BARCODE == mps_in_process.PCS_BARCODE && z.DEPT_BOM == mps_in_process.WC_CODE)
                .OrderBy(z => z.BOM_SEQ).ThenBy(z => z.RM_SEQ)
                .Select(x => new RawMatListView()
                {

                    bom_seq = x.BOM_SEQ,
                    bom_sub = x.BOM_SUB,
                    bom_name = x.BOM_NAME,
                    rm_seq = x.RM_SEQ,
                    rm_code = x.RM_CODE,
                    short_name = x.SHORT_NAME,
                    uom_code = x.UOM_CODE,
                    unit_qty = x.UNIT_QTY,
                    item_name = x.ITEM_NAME
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
    }
}