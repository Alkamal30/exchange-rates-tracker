using Application.Model.Models;
using Application.Model.Services;
using Application.ViewModel.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace Application.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
            _currencyRates = new ObservableCollection<CurrencyRate>();
            _loadDataCommand = new CommonCommand(LoadDataCommandAsync);
            _updateCellValueCommand = new CommonCommand(UpdateCellValueCommandAsync);

            _commonService = new CommonService();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<CurrencyRate> CurrencyRates => _currencyRates;
        public ICommand LoadDataCommand => _loadDataCommand;
        public ICommand UpdateCellValueCommand => _updateCellValueCommand;

        private ObservableCollection<CurrencyRate> _currencyRates;
        private ICommand _loadDataCommand;
        private ICommand _updateCellValueCommand;
        private readonly CommonService _commonService;

        private async void LoadDataCommandAsync(object parameter)
        {
            IList<CurrencyRate> currencies = await _commonService.UploadCurrenciesAsync();

            _currencyRates.Clear();
            foreach(var rate in currencies)
            {
                _currencyRates.Add(rate);
            }
        }

        private async void UpdateCellValueCommandAsync(object parameter)
        {
            await _commonService.SaveCurrenciesToFile(_currencyRates);
        }
    }
}
