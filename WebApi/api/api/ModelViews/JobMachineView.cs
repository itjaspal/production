using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.ModelViews
{
    public class JobMachineView
    {
        public string pdsize_desc{ get; set; }
        public string springtype_code { get; set; }
        public int plan_qty { get; set; }
        public int actual_qty { get; set; }
        public int diff_qty { get; set; }
    }

    public class JobMachineSearchView
    {
        public int pageIndex { get; set; }
        public int itemPerPage { get; set; }
        public string user_id { get; set; }
        public string mc_code { get; set; }
        
    }
}