using api.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.Interfaces
{
    interface IScanSendService
    {
        ScanSendView SearchSpringData(ScanSendSearchView model);
        ScanPcsView SearchScanPcs(ScanPcsSearchView model);
        ScanPcsView SearchPcs(ScanPcsSearchView model);
        void ScanPcs(ScanSendProcView model);
        void CancelPcs(ScanSendProcView model);
        ScanSendFinView SerachFinPcs(ScanSendFinSearchView model);
    }
}
