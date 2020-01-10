using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.ModelViews
{
    public class StockView
    {
        //public string ic_entity { get; set; }
        public string prod_code { get; set; }
        public string prod_name { get; set; }
        public string bar_code { get; set; }
        //public string pcs_barcode { get; set; }
    }

    public class StockSearchView
    {
        public int pageIndex { get; set; }
        public int itemPerPage { get; set; }
        //public string ic_entity { get; set; }
        public string prod_code { get; set; }
        public string prod_name { get; set; }
        public string bar_code { get; set; }
        //public string pcs_barcode { get; set; }
        //public decimal qty_oh { get; set; }
        //public decimal qty_all { get; set; }
        //public decimal qty_ship { get; set; }
        //public decimal qty_build { get; set; }
        //public decimal qty_avai { get; set; }


    }
}