using System.Collections.ObjectModel;

namespace BusConductor.Admin.UI.ViewModels.Bus
{
    public class IndexViewModel 
    {
        public ObservableCollection<IndexBusViewModel> Busses { get; set; }
    }
}
