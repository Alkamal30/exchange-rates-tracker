using Application.Model.Models;
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
        public event PropertyChangedEventHandler PropertyChanged;

        private static string _apiUrl = "https://api.nbrb.by/exrates/rates?periodicity=0";

        private ObservableCollection<CurrencyRate> _currencyRates;
        public ObservableCollection<CurrencyRate> CurrencyRates
        {
            get
            {
                if(_currencyRates is null)
                {
                    _currencyRates = new ObservableCollection<CurrencyRate>();
                }

                return _currencyRates;
            }
            //set {
            //    _currencyRates = value;
            //    //PropertyChanged(this, new PropertyChangedEventArgs(nameof(CurrencyRates)));
            //}
        }

        private CommonCommand _loadDataCommand;
        public ICommand LoadDataCommand
        {
            get
            {
                return _loadDataCommand ?? (_loadDataCommand = new CommonCommand(ExecuteLoadDataCommand));
            }
        }

        private async void ExecuteLoadDataCommand(object parameter)
        {
            IList<CurrencyRate> rates = await GetCurrenciesAwait();

            _currencyRates.Clear();
            foreach(var rate in rates)
            {
                _currencyRates.Add(rate);
            }
        }

        private async Task<IList<CurrencyRate>> GetCurrenciesAwait()
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(_apiUrl);

            string responseContent = await response.Content.ReadAsStringAsync();

            IList<CurrencyRate> rates = JsonConvert.DeserializeObject<IList<CurrencyRate>>(responseContent);

            return rates;
        }

        //private async Task UploadCurrenciesFromServer()
        //{
        //    string url = "https://api.nbrb.by/exrates/rates?periodicity=0";

        //    HttpClient httpClient = new HttpClient();
        //    HttpResponseMessage response = await httpClient.GetAsync(url);

        //    string responseContent = await response.Content.ReadAsStringAsync();

        //    IList<CurrencyRate> rates = JsonConvert.DeserializeObject<IList<CurrencyRate>>(responseContent);

        //    ShowResultMessageBox("Currency rates uploaded from server!");

        //    SaveToFile(responseContent);
        //}

        //private async Task UploadCurrenciesFromFile()
        //{
        //    string fileContent = File.ReadAllText(_fileName);

        //    IList<CurrencyRate> rates = JsonConvert.DeserializeObject<IList<CurrencyRate>>(fileContent);

        //    if (rates != null)
        //    {
        //        CurrenciesGridView.ItemsSource = rates;
        //        ShowResultMessageBox("Currency rates uploaded from file!");
        //    }
        //    else
        //    {
        //        await UploadCurrenciesFromServer();
        //    }
        //}

        //private void SaveToFile(string content)
        //{
        //    File.WriteAllText(_fileName, content);
        //}

        //private void ShowResultMessageBox(string message)
        //{
        //    MessageBox.Show(message);
        //}
    }
}
