using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.ModelViews
{
    public class SpecDrawingView
    {
        public int pageIndex { get; set; }
        public int itemPerPage { get; set; }
        public int totalItem { get; set; }
        public string prod_code { get; set; }
        public string prod_name { get; set; }
        public string model_desc { get; set; }
        public string spring_type { get; set; }
        public string size_desc { get; set; }
        public string drawing_path { get; set; }
        public string drawing_name { get; set; }
        public List<RawMatListView> datas { get; set; }
    }

    public class RawMatListView
    {
        //public string pcs_barcode { get; set; }
        public int bom_seq { get; set; }
        public string bom_sub { get; set; }
        public string bom_name { get; set; }
        public int rm_seq { get; set; }
        public string rm_code { get; set; }
        //public string rm_name { get; set; }
        public string short_name { get; set; }
        public string uom_code { get; set; }
        public int unit_qty { get; set; }
        public string item_name { get; set; }
        
    }

    public class SpecDrawingSearchView
    {
        public int pageIndex { get; set; }
        public int itemPerPage { get; set; }
        public string entity { get; set; }
        public string req_date { get; set; }
        public string springtype_code { get; set; }
        public string pdsize_code { get; set; }
    }

    public class SpecDrawingSearchPcsView
    {
        public int pageIndex { get; set; }
        public int itemPerPage { get; set; }
        public string pcs_barcode { get; set; }
    }

    public class PcsBarcodeDataView
    {
        public string entity { get; set; }
        public string pcs_barcode { get; set; }
        public string springtype_code { get; set; }
        public string prod_code { get; set; }
        public string prod_name { get; set; }
        public string pdsize_desc { get; set; }
        public string pdmodel_desc { get; set; }
        public string wc_code { get; set; }

    }


}