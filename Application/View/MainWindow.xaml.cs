using Application.Model.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace Application.View
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Loaded += OnWindowLoaded;

            InitializeComponent();
        }

        private static readonly string _fileName = "data.json";

        private async void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            await UploadCurrencies();
        }

        private void OnCellEditEnded(object sender, Telerik.Windows.Controls.GridViewCellEditEndedEventArgs e)
        {
            if(e.NewData.Equals(e.OldData))
            {
                return;
            }

            SaveToFile(JsonConvert.SerializeObject(CurrenciesGridView.ItemsSource));
        }

        private async Task UploadCurrencies()
        {
            if(File.Exists(_fileName))
            {
                await UploadCurrenciesFromFile();
            }
            else
            {
                await UploadCurrenciesFromServer();
            }
        }

        private async Task UploadCurrenciesFromServer()
        {
            string url = "https://api.nbrb.by/exrates/rates?periodicity=0";

            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(url);

            string responseContent = await response.Content.ReadAsStringAsync();

            IList<CurrencyRate> rates = JsonConvert.DeserializeObject<IList<CurrencyRate>>(responseContent);

            CurrenciesGridView.ItemsSource = rates;

            ShowResultMessageBox("Currency rates uploaded from server!");

            SaveToFile(responseContent);
        }

        private async Task UploadCurrenciesFromFile()
        {
            string fileContent = File.ReadAllText(_fileName);

            IList<CurrencyRate> rates = JsonConvert.DeserializeObject<IList<CurrencyRate>>(fileContent);

            if(rates != null)
            {
                CurrenciesGridView.ItemsSource = rates;
                ShowResultMessageBox("Currency rates uploaded from file!");
            }
            else
            {
                await UploadCurrenciesFromServer();
            }
        }

        private void SaveToFile(string content)
        {
            File.WriteAllText(_fileName, content);
        }

        private void ShowResultMessageBox(string message)
        {
            MessageBox.Show(message);
        }
    }
}
