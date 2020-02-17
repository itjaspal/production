using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.ModelViews
{
    public class PrintTagView
    {
    }

    public class SpringTagSearchView
    {
        public int pageIndex { get; set; }
        public int itemPerPage { get; set; }
        public string entity { get; set; }
        public string user_id { get; set; }
        public string mc_code { get; set; }
        public string wc_code { get; set; }

    }

    public class SpringTagView
    {
        public int totalItem { get; set; }
        public DateTime req_date { get; set; }
        public int total_plan_qty { get; set; }
        public int total_actual_qty { get; set; }
        public int total_diff_qty { get; set; }
        public List<SpringTagDataView> datas { get; set; }

    }

    public class SpringTagDataView
    {
        public int pageIndex { get; set; }
        public int itemPerPage { get; set; }
        public int totalItem { get; set; }
        public DateTime req_date { get; set; }
        public string pdsize_code { get; set; }
        public string pdsize_desc { get; set; }
        public string springtype_code { get; set; }
        public int plan_qty { get; set; }
        public int actual_qty { get; set; }
        public int diff_qty { get; set; }
        public string pcs_barcode { get; set; }
    }
}