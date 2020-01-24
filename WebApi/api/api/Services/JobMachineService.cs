using api.DataAccess;
using api.Interfaces;
using api.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.Services
{
    public class JobMachineService : IJobMachineService
    {
        public CommonSearchView<JobMachineView> Search(JobMachineSearchView model)
        {
            using (var ctx = new ConXContext())
            {
                //define model view
                CommonSearchView<JobMachineView> view = new ModelViews.CommonSearchView<ModelViews.JobMachineView>()
                {
                    pageIndex = model.pageIndex - 1,
                    itemPerPage = model.itemPerPage,
                    totalItem = 0,


                    datas = new List<ModelViews.JobMachineView>()
                };



                //query data
                //List<ic_mast> ic_masts = ctx.ic_masts
                //    .Where(x => (x.PROD_CODE.Contains(model.prod_code) || model.prod_code == "")
                //    //&& (x.WH_CODE.Contains(model.wh_code) || model.wh_code == "")
                //    )
                //    .OrderBy(o => o.IC_ENTITY).ThenBy(o => o.PROD_CODE).ThenBy(o => o.WH_CODE)
                //    .ToList();

                ////count , select data from pageIndex, itemPerPage
                //view.totalItem = ic_masts.Count;
                //ic_masts = ic_masts.Skip(view.pageIndex * view.itemPerPage)
                //    .Take(view.itemPerPage)
                //    .ToList();

                ////prepare model to modelView
                //foreach (var i in ic_masts)
                //{

                //    view.datas.Add(new ModelViews.StockView()
                //    {

                //        //ic_entity = i.IC_ENTITY,
                //        prod_code = i.PROD_CODE,
                //        //wh_code = i.WH_CODE,
                //        //qty_oh = i.QTY_OH,
                //        //qty_all = i.QTY_ALL,
                //        //qty_ship = i.QTY_SHIP,
                //        //qty_build = i.QTY_BUILD_DISPLAY,
                //        //qty_avai = i.QTY_AVAI

                //    });
                //}

                //return data to contoller
                return view;
            }
        }
    }
}