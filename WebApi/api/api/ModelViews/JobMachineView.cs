using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.ModelViews
{
    public class ReqdateParam
    {
        public DateTime req_date { get; set; }
        
    }
   
    public class JobMachineView
    {

        public int pageIndex { get; set; }
        public int itemPerPage { get; set; }
        public int totalItem { get; set; }
        public List<JobMachineReqView> datas { get; set; }
        //public List<JobMachineReqView> jobMachineForward { get; set; }
        //public List<JobMachineReqView> jobMachinePending { get; set; }



    }

    public class JobMachineReqView
    {
        //public int pageIndex { get; set; }
        //public int itemPerPage { get; set; }
        //public int totalItem { get; set; }
        public DateTime req_date { get; set; }
        public string pdsize_desc { get; set; }
        public string springtype_code { get; set; }
        public int plan_qty { get; set; }
        public int actual_qty { get; set; }
        public int diff_qty { get; set; }
    }

    public class JobMachineSearchView
    {
        public int pageIndex { get; set; }
        public int itemPerPage { get; set; }
        public string entity { get; set; }
        public string user_id { get; set; }
        public string mc_code { get; set; }
        public string wc_code { get; set; }

    }
}