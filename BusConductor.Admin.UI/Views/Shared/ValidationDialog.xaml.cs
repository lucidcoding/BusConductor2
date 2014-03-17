using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using BusConductor.Admin.UI.ViewModels.Shared;

namespace BusConductor.Admin.UI.Views.Shared
{
    public partial class ValidationDialog : Window
    {
        public ValidationDialog(IEnumerable<string> messages)
        {
            InitializeComponent();

            var validationViewModel = new ValidationViewModel();
            validationViewModel.Messages = new ObservableCollection<string>();

            foreach(var message in messages)
            {
                validationViewModel.Messages.Add(message);
            }

            DataContext = validationViewModel;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
