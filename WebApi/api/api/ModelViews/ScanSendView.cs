using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.ModelViews
{
    public class ScanSendView
    {
        public int pageIndex { get; set; }
        public int itemPerPage { get; set; }
        public int totalItem { get; set; }
        public int total_plan_qty { get; set; }
        public int total_inact_qty { get; set; }
        public int total_qp_qty { get; set; }
        public int total_act_qty { get; set; }
        public List<SpringDataView> datas { get; set; }
    }

    public class SpringDataView
    {
        //public string entity { get; set; }
        public DateTime req_date { get; set; }
        public string springtype_code { get; set; }
        public string pdsize_code { get; set; }
        public string pdsize_desc { get; set; }
        public int plan_qty { get; set; }
        public int inact_qty { get; set; }
        public int qp_qty { get; set; }
        public int act_qty { get; set; }
    }

    public class ScanSendSearchView
    {
        public int pageIndex { get; set; }
        public int itemPerPage { get; set; }
        public string entity { get; set; }
        public string wc_code { get; set; }
        public string req_date { get; set; }

    }

    public class ScanPcsSearchView
    {
        public int pageIndex { get; set; }
        public int itemPerPage { get; set; }
        public string entity { get; set; }
        public string req_date { get; set; }
        public string wc_code { get; set; }
        public string pcs_barcode { get; set; }
    
    }

    public class ScanPcsView
    {
        public string pcs_barcode {get ;set;}
        public string spring_grp { get; set; }
        public string size_desc { get; set; }
        public int qty { get; set; }
    }

    public class ScanSendFinSearchView
    {
        public int pageIndex { get; set; }
        public int itemPerPage { get; set; }
        public string entity { get; set; }
        public string wc_code { get; set; }
        public string req_date { get; set; }
        public string user_id { get; set; }

    }

    public class ScanSendFinView
    {
        public int pageIndex { get; set; }
        public int itemPerPage { get; set; }
        public int totalItem { get; set; }
        public List<ScanSendDataView> datas { get; set; }
    }

    public class ScanSendDataView
    {
        public string pcs_barcode { get; set; }
        public string model_desc { get; set; }
        public string prod_code { get; set; }
        public string prod_name { get; set; }
    }

    public class ScanSendProcView
    {
        public string entity { get; set; }
        public string wc_code { get; set; }
        public string req_date { get; set; }
        public string pcs_barcode { get; set; }
        public string user_id { get; set; }
    }
}