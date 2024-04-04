using Application.ViewModel;
using System.Windows;
using Telerik.Windows.Controls;

namespace Application.View
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            MainWindowViewModel viewModel = DataContext as MainWindowViewModel;

            viewModel?.LoadDataCommand.Execute(null);
        }

        private void OnCellEditEnded(object sender, GridViewCellEditEndedEventArgs e)
        {
            if(e.OldData.Equals(e.NewData))
            {
                return;
            }

            MainWindowViewModel viewModel = DataContext as MainWindowViewModel;

            viewModel?.UpdateCellValueCommand.Execute(null);
        }
    }
}
