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
        void AddTag(PrintTagAddView model);
        void DeleteTag(PrintTagProcView model);
        void PringTag(PrintTagProcView model);
    }
}
