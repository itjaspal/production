using api.DataAccess;
using api.Interfaces;
using api.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.Services
{
    public class DropdownlistService : IDropdownlistService
    {
         private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(DropdownlistService));

        public DropdownlistService()
        {

        }

        public List<Dropdownlist<string>> GetDdlBranchStatus()
        {
                //log.Info("Test Info Log");
                List<Dropdownlist<string>> ddl = new List<ModelViews.Dropdownlist<string>>();
                ddl.Add(new ModelViews.Dropdownlist<string>()
                {
                    key = "A",
                    value = "ใช้งาน"
                });
                ddl.Add(new ModelViews.Dropdownlist<string>()
                {
                    key = "I",
                    value = "ไม่ใช้งาน"
                });
                return ddl;
        }

        public List<Dropdownlist> GetDdlProductBrand()
        {
            using (var ctx = new ConXContext())
            {
                List<Dropdownlist> ddl = ctx.pdbrnd
                .Where(z => z.STATUS == "A")
                .OrderBy(z => z.PDBRND_CODE)
                .Select(x => new Dropdownlist()
                {

                    key = x.PDBRND_CODE,
                    value = x.PDBRND_CODE + " - " + x.PDBRND_TNAME,
                })
                .ToList();
                return ddl;
            }
        }

        public List<Dropdownlist> GetDdlProductColor()
        {
            using (var ctx = new ConXContext())
            {
                List<Dropdownlist> ddl = ctx.pdcolor
                .Where(z => z.STATUS == "A")
                .OrderBy(z => z.PDCOLOR_CODE)
                .Select(x => new Dropdownlist()
                {

                    key = x.PDCOLOR_CODE,
                    value = x.PDCOLOR_CODE + " - " + x.PDCOLOR_TNAME,
                })
                .ToList();
                return ddl;
            }
        }

        public List<Dropdownlist> GetDdlProductDesign()
        {
            using (var ctx = new ConXContext())
            {
                List<Dropdownlist> ddl = ctx.pddsgn
                .Where(z => z.STATUS == "A")
                .OrderBy(z => z.PDDSGN_CODE)
                .Select(x => new Dropdownlist()
                {

                    key = x.PDDSGN_CODE,
                    value = x.PDDSGN_CODE + " - " + x.PDDSGN_TNAME,
                })
                .ToList();
                return ddl;
            }
        }

        public List<Dropdownlist> GetDdlProductGroup()
        {
            using (var ctx = new ConXContext())
            {
                List<Dropdownlist> ddl = ctx.pdgroup
                .Where(z => z.STATUS == "A")
                .OrderBy(z => z.PDGRP_CODE)
                .Select(x => new Dropdownlist()
                {
                            
                    key = x.PDGRP_CODE,
                    value = x.PDGRP_CODE+" - "+x.PDGRP_TNAME,
                })
                .ToList();
                return ddl;
            }
        }

        public List<Dropdownlist> GetDdlProductModel()
        {
            using (var ctx = new ConXContext())
            {
                List<Dropdownlist> ddl = ctx.pdmodel
                .Where(z => z.STATUS == "A")
                .OrderBy(z => z.PDMODEL_CODE)
                .Select(x => new Dropdownlist()
                {

                    key = x.PDMODEL_CODE,
                    value = x.PDMODEL_CODE + " - " + x.PDMODEL_TNAME,
                })
                .ToList();
                return ddl;
            }
        }

        public List<Dropdownlist> GetDdlProductSize()
        {
            using (var ctx = new ConXContext())
            {
                List<Dropdownlist> ddl = ctx.pdsize
                .Where(z => z.STATUS == "A")
                .OrderBy(z => z.PDSIZE_CODE)
                .Select(x => new Dropdownlist()
                {

                    key = x.PDSIZE_CODE,
                    value = x.PDSIZE_CODE + " - " + x.PDSIZE_TNAME,
                })
                .ToList();
                return ddl;
            }
        }

        public List<Dropdownlist> GetDdlProductType()
        {
            using (var ctx = new ConXContext())
            {
                    List<Dropdownlist> ddl = ctx.pdtype
                        .Where(z => z.STATUS == "A")
                        .OrderBy(z => z.PDTYPE_CODE)
                        .Select(x => new Dropdownlist()
                        {

                            key = x.PDTYPE_CODE,
                            value = x.PDTYPE_CODE + " - " + x.PDTYPE_TNAME,
                        })
                        .ToList();
                    return ddl;
            }
        }

        public List<Dropdownlist> GetDdlMobilePrnt()
        {
            using (var ctx = new ConXContext())
            {
                List<Dropdownlist> ddl = ctx.mobileprnt_ctl
                    .Where(z => z.GRP_TYPE == "SPRING")
                    .OrderBy(z => z.PRNT_POINT_NAME)
                    .Select(x => new Dropdownlist()
                    {

                        key = x.SERIES_NO,
                        value = x.SERIES_NO + " - " + x.PRNT_POINT_NAME,
                    })
                    .ToList();
                return ddl;
            }
        }




        //public List<Dropdownlist> GetDdlYear()
        //{
        //    using (var ctx = new ConXContext())
        //    {

        //        List<Dropdownlist> ddl = new List<Dropdownlist>();
        //        int year = DateTime.Today.Year + 1;
        //        for (int i = 0; i < 6; i++)
        //        {
        //            Dropdownlist d = new Dropdownlist()
        //            {
        //                key = year - i,
        //                value = (year - i).ToString()
        //            };
        //            ddl.Add(d);
        //        }

        //        return ddl;
        //    }
        //}

        //public List<Dropdownlist> GetDdlMonth()
        //{
        //    using (var ctx = new ConXContext())
        //    {

        //        List<Dropdownlist> ddl = new List<Dropdownlist>();


        //        for (int i = 1; i <= 12; i++)
        //        {
        //            Dropdownlist d = new Dropdownlist()
        //            {
        //                key = i,
        //                value = Util.Util.GetMonthNameThai(i)
        //            };
        //            ddl.Add(d);
        //        }

        //        return ddl;
        //    }
        //}

        //public List<Dropdownlist> GetDdlStockLocation()
        //{
        //    using (var ctx = new ConXContext())
        //    {

        //        List<Dropdownlist> ddl = ctx.StockLocations.Where(z => z.isControlStock)
        //            .Select(x => new Dropdownlist()
        //            {
        //                key = x.stockLocationId,
        //                value = x.locationName
        //            })
        //            .ToList();

        //        return ddl;
        //    }
        //}



    }
}