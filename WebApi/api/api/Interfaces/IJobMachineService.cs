using api.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.Interfaces
{
    interface IJobMachineService
    {
        JobMachineView SearchCurrent(JobMachineSearchView model);
        JobMachineView SearchPending(JobMachineSearchView model);
        JobMachineView SearchForward(JobMachineSearchView model);

        //JobMachineView SearchDate(JobMachineDateSearchView model);
    }
}
