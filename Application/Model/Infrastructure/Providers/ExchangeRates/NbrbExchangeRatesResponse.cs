using Application.Model.Infrastructure.Providers.Abstraction;
using Application.Model.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Application.Model.Infrastructure.Providers.ExchangeRates
{
    public class NbrbExchangeRatesResponse : IApiResponse<CurrencyRate>
    {
        [JsonProperty("status")]
        private int Status { get; set; }

        [JsonProperty("errors")]
        private Dictionary<string, IList<string>> Errors { get; set; }

        public bool IsSuccess => throw new System.NotImplementedException();

        public string GetErrorMessage()
        {
            throw new System.NotImplementedException();
        }

        public CurrencyRate GetResponseData()
        {
            throw new System.NotImplementedException();
        }
    }
}
