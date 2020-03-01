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
        //ScanPcsView SearchPcs(ScanPcsSearchView model);
        void UpdatePcs(DataEntrySearchView model);
        void CancelPcs(DataEntrySearchView model);
        //bool CheckPrevWc(string entity , string wc_code , string pcs_barcode);
        ScanSendFinView SerachFinPcs(ScanSendFinSearchView model);
        ScanSendFinView SerachCanPcs(ScanSendFinSearchView model);
    }
}
