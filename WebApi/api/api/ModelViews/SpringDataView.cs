using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.ModelViews
{
    public class SpringSearchView
    {
        public int pageIndex { get; set; }
        public int itemPerPage { get; set; }
        public string entity { get; set; }
        public string user_id { get; set; }
        public string mc_code { get; set; }
        public string wc_code { get; set; }

    }

    public class SpringView
    {
        public int pageIndex { get; set; }
        public int itemPerPage { get; set; }
        public int totalItem { get; set; }
        public DateTime req_date { get; set; }
        public string spring_grp { get; set; }
        public int total_plan_qty { get; set; }
        public int total_actual_qty { get; set; }
        public int total_diff_qty { get; set; }
        public List<SpringReqView> datas { get; set; }

    }

    public class SpringReqView
    {
        
        public DateTime req_date { get; set; }
        public string pdsize_code { get; set; }
        public string pdsize_desc { get; set; }
        public string springtype_code { get; set; }
        public int plan_qty { get; set; }
        public int actual_qty { get; set; }
        public int diff_qty { get; set; }
    }

    public class SpringDateSearchView
    {
        public int pageIndex { get; set; }
        public int itemPerPage { get; set; }
        public string entity { get; set; }
        public string user_id { get; set; }
        public string mc_code { get; set; }
        public string wc_code { get; set; }
        public string req_date { get; set; }

    }
}