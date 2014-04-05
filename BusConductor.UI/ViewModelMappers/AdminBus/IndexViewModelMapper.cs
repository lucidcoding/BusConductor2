using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusConductor.UI.ViewModels.AdminBus;
using BusConductor.Domain.Entities;

namespace BusConductor.UI.ViewModelMappers.AdminBus
{
    public static class IndexViewModelMapper
    {
        public static IndexViewModel Map(IList<Bus> busses)
        {
            var viewModel = new IndexViewModel();
            viewModel.Busses = new List<IndexViewModelBus>();

            foreach (var bus in busses)
            {
                var indexViewModelBus = new IndexViewModelBus();
                indexViewModelBus.BusId = bus.Id.Value;
                indexViewModelBus.Name = bus.Name;
                viewModel.Busses.Add(indexViewModelBus);
            }

            return viewModel;
        }
    }
}