using api.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.Interfaces
{
    interface IPrintTagService
    {
        PrintTagView searchPrintData(PrintTagSearchView model);
        RawMatView searchRawData(RawMatSearchView model);
        PrintTagView AddTag(PrintTagSearchView model);
        void DeleteTag(PrintTagProcView model);
        void PringTag(PrintTagView model);
        CommonSearchView<ProcessTagView>  searchProcessTagNoList(ProcessTagSearchView model);
        PrintTagView searchProcessTagNo(ProcessTagNoSearch model);
        RawMatitemView searchRawScan(RawMatScanSerchView model);
    }
}
