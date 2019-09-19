using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackMyMoney.Common;
using TrackMyMoney.Models;

namespace TrackMyMoney.Services.Contracts
{
    public interface ICostsManager
    {
        OperationResult<List<Cost>> GetCosts();
        OperationResult SaveCost(Cost cost);
    }
}
