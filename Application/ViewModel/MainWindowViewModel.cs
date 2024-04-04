using Application.Model.Models;
using Application.Model.Services;
using Application.ViewModel.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Application.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
            _currencyRates = new ObservableCollection<CurrencyRate>();
            _loadDataCommand = new CommonCommand(ExecuteLoadDataCommand);

            _commonService = new CommonService();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<CurrencyRate> CurrencyRates => _currencyRates;
        public ICommand LoadDataCommand => _loadDataCommand;

        private ObservableCollection<CurrencyRate> _currencyRates;
        private CommonCommand _loadDataCommand;
        private readonly CommonService _commonService;

        private async void ExecuteLoadDataCommand(object parameter)
        {
            IList<CurrencyRate> currencies = await _commonService.UploadCurrenciesFromServerAsync();

            _currencyRates.Clear();
            foreach(var rate in currencies)
            {
                _currencyRates.Add(rate);
            }
        }
    }
}
