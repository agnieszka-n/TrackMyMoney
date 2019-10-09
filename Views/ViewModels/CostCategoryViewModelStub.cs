using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackMyMoney.ViewModels.Contracts;

namespace TrackMyMoney.Views.ViewModels
{
    internal class CostCategoryViewModelStub : ICostCategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
