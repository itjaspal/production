﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.ModelViews
{
    public class PrintTagSearchView
    {
        public string entity { get; set; }
        public string req_date { get; set; }
        public string wc_code { get; set; }
        public string mc_code { get; set; }
        public string spring_grp { get; set; }
        public string size_desc { get; set; }
        public int qty { get; set; }
        public string user_id { get; set; }
        public string printer { get; set; }
    }

    public class PrintTagView
    {
        public string entity { get; set; }
        public int process_tag_no { get; set; }
        public string req_date { get; set; }
        public string wc_code { get; set; }
        public string mc_code { get; set; }
        public string spring_grp { get; set; }
        public string size_desc { get; set; }
        public int qty { get; set; }
        public string fin_date { get; set; }
        public string printer { get; set; }
        public string user_id { get; set; }
        public List<RawMatitemView> datas { get; set; }
    }

    public class RawMatitemView
    {
        public int process_tag_no { get; set; }
        public string doc_no { get; set; }
        public string prod_code { get; set; }
        public string prod_name { get; set; }
    }


    public class RawMatSearchView
    {
        //public bool isEdit { get; set; }
        //public int itemPerPage { get; set; }
        public int pageIndex { get; set; }
        public int itemPerPage { get; set; }
        public string doc_date { get; set; }
        //public string entity { get; set; }
        //public string req_date { get; set; }
        //public string mc_code { get; set; }
        //public int process_tag_no { get; set; }
        //public List<RawMatitemView> editItem { get; set; }
    }

    public class RawMatView
    {
        public int pageIndex { get; set; }
        public int itemPerPage { get; set; }
        public int totalItem { get; set; }
        //public string entity { get; set; }
        //public string req_date { get; set; }
        //public string mc_code { get; set; }
        //public int process_tag_no { get; set; }
        public List<RawProductView> datas { get; set; }
    }

    public class RawProductView
    {
        //public string process_tag_no { get; set; } 
        public string doc_no { get; set; }
        public string prod_code { get; set; }
        public string prod_name { get; set; }
    }

    public class PrintTagAddView
    {
        public string entity { get; set; }
        public string req_date { get; set; }
        public string mc_code { get; set; }
        public int process_tag_no { get; set; }
        public string doc_no { get; set; }
        public string prod_code { get; set; }
        public string prod_name { get; set; }
        public string qr { get; set; }

    }

    public class PrintTagProcView
    {
        public string entity { get; set; }
        public string req_date { get; set; }
        public string mc_code { get; set; }
        public int process_tag_no { get; set; }

    }

    public class ProcessTagSearchView
    {
        public string entity { get; set; }
        public string req_date { get; set; }
        public string mc_code { get; set; }
    }

    public class ProcessTagView
    {
        public int process_tag_no { get; set; }
    }

    public class ProcessTagNoSearch
    {
        public string entity { get; set; }
        public string req_date { get; set; }
        public string wc_code { get; set; }
        public string mc_code { get; set; }
        public string spring_grp { get; set; }
        public string size_desc { get; set; }
        public int qty { get; set; }
        public string printer { get; set; }
        public int process_tag_no { get; set; }
    }

    public class PrinterDataView
    {
        public string printer_name { get; set; }
        public string filepath_data { get; set; }
        public string filepath_txt { get; set; }
    }

    public class RawMatScanSerchView
    {
        public int process_tag_no { get; set; }
        public string qr { get; set; }
    }
    
}