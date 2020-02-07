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
}