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
        ScanPcsView SearchScanCanclePcs(ScanPcsSearchView model);
        //void UpdatePcs(DataEntrySearchView model);
        //void CancelPcs(DataEntrySearchView model);

        ScanSendFinView UpdatePcs(DataEntrySearchView model);
        ScanSendFinView CancelPcs(DataEntrySearchView model);
        //ScanSendFinView CancelPcs(DataEntrySearchView model);

        ScanSendFinView SerachFinPcs(ScanSendFinSearchView model);
        ScanSendFinView SerachCanPcs(ScanSendFinSearchView model);
    }
}
