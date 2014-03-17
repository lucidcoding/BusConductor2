using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using BusConductor.Admin.UI.Helpers;
using BusConductor.Admin.UI.ViewModels.Bus;
using BusConductor.Admin.UI.Views.Shared;
using BusConductor.Application.Contracts;
using BusConductor.Application.Requests.Bus;
using BusConductor.Data.Common;
using BusConductor.Domain.Common;
using BusConductor.Domain.RepositoryContracts;
using StructureMap;

namespace BusConductor.Admin.UI.Views.Bus
{
    public partial class Edit : Page
    {
        private IContextProvider _contextProvider;
        private IBusRepository _busRepository;
        private IBusService _busService;

        public Edit(Guid id)
        {
            InitializeComponent();
            _contextProvider = ObjectFactory.GetInstance<IContextProvider>();
            _busRepository = ObjectFactory.GetInstance<IBusRepository>();
            _busService = ObjectFactory.GetInstance<IBusService>();
            InitializeComponent();

            using (_contextProvider)
            {
                var detailsViewModel = new EditViewModel();
                detailsViewModel.PricingPeriods = new ObservableCollection<EditPricingPeriodViewModel>();
                var bus = _busRepository.GetById(id);
                detailsViewModel.Id = bus.Id.Value;
                detailsViewModel.Name = bus.Name;
                detailsViewModel.Description = bus.Description;
                detailsViewModel.Overview = bus.Overview;
                detailsViewModel.Details = bus.Details;
                detailsViewModel.DriveSide = bus.DriveSide;
                detailsViewModel.Berth = bus.Berth;
                detailsViewModel.Year = bus.Year;

                foreach(var pricingPeriod in bus.PricingPeriods.OrderBy(x => x.EndMonth).ThenBy(x => x.EndDay))
                {
                    var editPricingPeriodViewModel = new EditPricingPeriodViewModel();
                    editPricingPeriodViewModel.Id = pricingPeriod.Id.Value;
                    editPricingPeriodViewModel.StartMonth = pricingPeriod.StartMonth;
                    editPricingPeriodViewModel.StartDay = pricingPeriod.StartDay;
                    editPricingPeriodViewModel.EndMonth = pricingPeriod.EndMonth;
                    editPricingPeriodViewModel.EndDay = pricingPeriod.EndDay;
                    editPricingPeriodViewModel.FridayToFridayRate = pricingPeriod.FridayToFridayRate;
                    editPricingPeriodViewModel.FridayToMondayRate = pricingPeriod.FridayToMondayRate;
                    editPricingPeriodViewModel.MondayToFridayRate = pricingPeriod.MondayToFridayRate;
                    detailsViewModel.PricingPeriods.Add(editPricingPeriodViewModel);
                }

                DataContext = detailsViewModel;
            }
        }

        private void AddPricingPeriod_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as EditViewModel;
            viewModel.PricingPeriods.Add(new EditPricingPeriodViewModel
                                             {
                                                 Id = Guid.NewGuid(),
                                                 StartMonth = 0,
                                                 StartDay = 1,
                                                 EndMonth = 0,
                                                 EndDay = 1,
                                                 FridayToFridayRate = 0,
                                                 FridayToMondayRate = 0,
                                                 MondayToFridayRate = 0
                                             });
        }

        public void Save_Clicked(object sender, RoutedEventArgs e)
        {
            var firstLineValidation = new List<string>();
            if (Validation.GetHasError(Year)) firstLineValidation.Add("Year is not valid");

            foreach(var item in ListViewPricingPeriods.Items)
            {
                var pricingPeriodTitle = " at Pricing Period at index " + ListViewPricingPeriods.Items.IndexOf(item);
                var listViewItem = ListViewPricingPeriods.ItemContainerGenerator.ContainerFromItem(item) as ListViewItem;
                //if(Validation.GetHasError(ControlFinder.FindChild<TextBox>(listViewItem, "StartDay"))) firstLineValidation.Add("Incorrect Start Day" + pricingPeriodTitle);
                //if (Validation.GetHasError(ControlFinder.FindChild<TextBox>(listViewItem, "EndDay"))) firstLineValidation.Add("Incorrect End Day" + pricingPeriodTitle);
                //if (Validation.GetHasError(ControlFinder.FindChild<TextBox>(listViewItem, "FridayToFridayRate"))) firstLineValidation.Add("Incorrect Friday to Friday rate" + pricingPeriodTitle);
                //if (Validation.GetHasError(ControlFinder.FindChild<TextBox>(listViewItem, "FridayToMondayRate"))) firstLineValidation.Add("Incorrect Friday to Monday rate" + pricingPeriodTitle);
                //if (Validation.GetHasError(ControlFinder.FindChild<TextBox>(listViewItem, "MondayToFridayRate"))) firstLineValidation.Add("Incorrect Monday to Friday rate" + pricingPeriodTitle);
                if (Validation.GetHasError(listViewItem.FindChild<TextBox>("StartDay"))) firstLineValidation.Add("Incorrect Start Day" + pricingPeriodTitle);
                if (Validation.GetHasError(listViewItem.FindChild<TextBox>("EndDay"))) firstLineValidation.Add("Incorrect End Day" + pricingPeriodTitle);
                if (Validation.GetHasError(listViewItem.FindChild<TextBox>("FridayToFridayRate"))) firstLineValidation.Add("Incorrect Friday to Friday rate" + pricingPeriodTitle);
                if (Validation.GetHasError(listViewItem.FindChild<TextBox>("FridayToMondayRate"))) firstLineValidation.Add("Incorrect Friday to Monday rate" + pricingPeriodTitle);
                if (Validation.GetHasError(listViewItem.FindChild<TextBox>("MondayToFridayRate"))) firstLineValidation.Add("Incorrect Monday to Friday rate" + pricingPeriodTitle);
            }

            if (firstLineValidation.Any())
            {
                var validationDialog = new ValidationDialog(firstLineValidation);
                validationDialog.Owner = Window.GetWindow(this);
                var dialogResult = validationDialog.ShowDialog();
                return;
            }

            var viewModel = DataContext as EditViewModel;
            var request = new EditRequest();
            request.Id = viewModel.Id;
            request.Name = viewModel.Name;
            request.Description = viewModel.Description;
            request.Overview = viewModel.Overview;
            request.Details = viewModel.Details;
            request.DriveSide = viewModel.DriveSide;
            request.Berth = viewModel.Berth;
            request.Year = viewModel.Year;
            request.PricingPeriods = new List<EditPricingPeriodRequest>();

            foreach (var editPricingPeriodViewModel in viewModel.PricingPeriods)
            {
                var editPricingPeriodRequest = new EditPricingPeriodRequest();
                editPricingPeriodRequest.Id = editPricingPeriodViewModel.Id;
                editPricingPeriodRequest.StartMonth = editPricingPeriodViewModel.StartMonth;
                editPricingPeriodRequest.StartDay = editPricingPeriodViewModel.StartDay;
                editPricingPeriodRequest.EndMonth = editPricingPeriodViewModel.EndMonth;
                editPricingPeriodRequest.EndDay = editPricingPeriodViewModel.EndDay;
                editPricingPeriodRequest.FridayToFridayRate = editPricingPeriodViewModel.FridayToFridayRate;
                editPricingPeriodRequest.FridayToMondayRate = editPricingPeriodViewModel.FridayToMondayRate;
                editPricingPeriodRequest.MondayToFridayRate = editPricingPeriodViewModel.MondayToFridayRate;
                request.PricingPeriods.Add(editPricingPeriodRequest);
            }

            ValidationMessageCollection validationMessages;

            using (_contextProvider)
            {
                validationMessages = _busService.ValidateEdit(request);
            }

            if (validationMessages.Any())
            {
                var validationDialog = new ValidationDialog(validationMessages.Select(x => x.Text));
                validationDialog.Owner = Window.GetWindow(this);
                var dialogResult = validationDialog.ShowDialog();
                return;
            }

            using(var transactionScope = new TransactionScope())
            using(_contextProvider)
            {
                _busService.Edit(request);
                _contextProvider.SaveChanges();
                transactionScope.Complete();
            }

            MessageBox.Show(viewModel.Name + " has been updated.");
        }
    }
}