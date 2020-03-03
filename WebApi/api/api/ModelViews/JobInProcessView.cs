using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.ModelViews
{
    public class JobInProcessView
    {
        public int pageIndex { get; set; }
        public int itemPerPage { get; set; }
        public int totalItem { get; set; }
        public string pcs_barcode { get; set; }
        public string pdsize_desc { get; set; }
        public string springtype_code { get; set; }
        public int qty { get; set; }
        public string prod_code { get; set; }
        //public List<JobInProcessScanView> datas { get; set; }


    }

    public class JobInProcessScanFinView
    {
        public int pageIndex { get; set; }
        public int itemPerPage { get; set; }
        public int totalItem { get; set; }
        public List<JobInProcessScanView> datas { get; set; }
    }

    public class JobInProcessScanView
    {
        public string pcs_barcode { get; set; }
        public string pdmodel_code { get; set; }
        public string prod_code { get; set; }
        public string prod_name { get; set; }
        public int qty { get; set; }
    }
    public class JobInProcessSearchView
    {
        public int pageIndex { get; set; }
        public int itemPerPage { get; set; }
        public string entity { get; set; }
        public string req_date { get; set; }
        public string pcs_barcode { get; set; }
        public string wc_code { get; set; }
        public string mc_code { get; set; }
        public string user_id { get; set; }
        public string spring_grp { get; set; }
        public string size_code { get; set; }
        //public int qty { get; set; }

    }

    public class DataEntrySearchView
    {
        public string entity { get; set; }
        public string req_date { get; set; }
        public string wc_code { get; set; }
        public string mc_code { get; set; }  
        public string user_id { get; set; } 
        public string spring_grp { get; set; }  
        public string size_code { get; set; }  
        public int qty { get; set; }
    }
}