using System.Collections.ObjectModel;

namespace ViewModelsContracts
{
    public interface ICostsListViewModel
    {
        ObservableCollection<ICostViewModel> Costs { get; set; }
    }
}
