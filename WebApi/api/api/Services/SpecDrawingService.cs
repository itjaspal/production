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
                
                
                
                //define model view
                SpecDrawingView view = new ModelViews.SpecDrawingView()
                {
                    pageIndex = model.pageIndex - 1,
                    itemPerPage = model.itemPerPage,
                    totalItem = 0,


                    datas = new List<ModelViews.RawMatListView>()
                };

                string vpcs_barcode = model.pcs_barcode;



                

                //return data to contoller
                return view;

            }
        }
    }
}