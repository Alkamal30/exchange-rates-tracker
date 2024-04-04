using Application.Model.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace Application.Model.Services
{
    public class CommonService
    {
        private static readonly string _apiUrl = "https://api.nbrb.by/exrates/rates?periodicity=0";
        private static readonly string _fileName = "data.json";
        
        public async Task<IList<CurrencyRate>> UploadCurrenciesAsync()
        {
            IList<CurrencyRate> currencies = await UploadCurrenciesFromFileAsync()
                ?? await UploadCurrenciesFromServerAsync();

            return currencies;
        }

        public async Task SaveCurrenciesToFile(IList<CurrencyRate> currencies)
        {
            SaveToFile(JsonConvert.SerializeObject(currencies));
        }

        private async Task<IList<CurrencyRate>> UploadCurrenciesFromFileAsync()
        {
            try
            {
                string fileContent = File.ReadAllText(_fileName);

                IList<CurrencyRate> rates = JsonConvert.DeserializeObject<IList<CurrencyRate>>(fileContent);

                if (rates != null)
                {
                    ShowResultMessageBox("Currency rates uploaded from file!");
                    return rates;
                }
            }
            catch
            {
            }

            return null;
        }

        private async Task<IList<CurrencyRate>> UploadCurrenciesFromServerAsync()
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(_apiUrl);

            string responseContent = await response.Content.ReadAsStringAsync();

            IList<CurrencyRate> rates = JsonConvert.DeserializeObject<IList<CurrencyRate>>(responseContent);

            if(rates != null)
            {
                ShowResultMessageBox("Currency rates uploaded from server!");
                SaveToFile(responseContent);
                return rates;
            }

            return null;
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
