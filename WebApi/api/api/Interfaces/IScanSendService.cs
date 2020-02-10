﻿using api.ModelViews;
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
        JobInProcessView SearchScanPcs(ScanPcsSearchView model);
        void ScanPcs(ScanSendProcView model);
        void CancelPcs(ScanSendProcView model);
        ScanSendFinView SerachFinPcs(ScanSendFinSearchView model);
    }
}
