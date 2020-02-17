using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.ModelViews
{
    public class DefaultPrinterView
    {
        public string grp_type { get; set; }
        public string prnt_point_name { get; set; }
        //public string filepath_data { get; set; }
        //public string filepath_btw { get; set; }
        //public string filepath_txt { get; set; }
        public string serial_no { get; set; }
        public string user_code { get; set; }
        //public DateTime sys_date { get; set; }
        public string default_no { get; set; }

    }

    public class GetDefaultPrinterView
    {
        public string grp_type { get; set; }
        public string serial_no { get; set; }
        public string prnt_point_name { get; set; }
        public string default_no { get; set; }
        public string filepath_data { get; set; }
        public string filepath_btw { get; set; }
        public string filepath_txt { get; set; }
    }


}