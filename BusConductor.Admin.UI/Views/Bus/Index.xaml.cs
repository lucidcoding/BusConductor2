using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using BusConductor.Admin.UI.ViewModels.Bus;
using BusConductor.Data.Common;
using BusConductor.Domain.RepositoryContracts;
using StructureMap;

namespace BusConductor.Admin.UI.Views.Bus
{
    public partial class Index : Page
    {
        private IContextProvider _contextProvider;
        private IBusRepository _busRepository;

        public Index()
        {
            _contextProvider = ObjectFactory.GetInstance<IContextProvider>();
            _busRepository = ObjectFactory.GetInstance<IBusRepository>();
            InitializeComponent();

            using (_contextProvider)
            {
                var indexViewModel = new IndexViewModel();
                indexViewModel.Busses = new ObservableCollection<IndexBusViewModel>();
                var busses = _busRepository.GetAllUndeleted();

                foreach (var bus in busses)
                {
                    var indexBusViewModel = new IndexBusViewModel();
                    indexBusViewModel.Id = bus.Id.Value;
                    indexBusViewModel.Name = bus.Name;
                    indexViewModel.Busses.Add(indexBusViewModel);
                }
                
                DataContext = indexViewModel;
            }

        }

        public void Edit_Clicked(object sender, RoutedEventArgs e)
        {
            var button = (Button) sender;
            var busId = (Guid) button.CommandParameter;
            var detail = new Edit(busId);
            NavigationService.Navigate(detail);
        }
    }
}
