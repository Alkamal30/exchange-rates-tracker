using Application.Model.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Application.Model.Services
{
    public class CommonService
    {
        private static string _apiUrl = "https://api.nbrb.by/exrates/rates?periodicity=0";
        
        public async Task<IList<CurrencyRate>> UploadCurrenciesFromServerAsync()
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
