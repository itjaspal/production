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
        SpringTagView SearchCurrent(SpringTagSearchView model);
        SpringTagView SearchPending(SpringTagSearchView model);
        SpringTagView SearchForward(SpringTagSearchView model);
    }
}
