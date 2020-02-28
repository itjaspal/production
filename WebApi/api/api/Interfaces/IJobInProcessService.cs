using api.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.Interfaces
{
    interface IJobInProcessService
    {
        //JobInProcessView SearchPcs(JobInProcessSearchView model);
        JobInProcessView SearchScanPcs(JobInProcessSearchView model);
        JobInProcessView SearchScanCancelPcs(JobInProcessSearchView model);
        JobInProcessScanFinView SerachFinPcs(JobInProcessSearchView model);
        JobInProcessScanFinView SerachCancelPcs(JobInProcessSearchView model);
        //void UpdatePcs(JobInProcessSearchView model);
        //void CancelPcs(JobInProcessSearchView model);
    }
}
