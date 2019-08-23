using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Models;

namespace Services.Contracts
{
    public interface ICostsManager
    {
        OperationResult<List<Cost>> GetCosts();
    }
}
