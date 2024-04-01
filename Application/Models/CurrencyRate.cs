using Newtonsoft.Json;
using System;

namespace Application.Models
{
    public class CurrencyRate
    {
        [JsonProperty("Date")]
        public DateTime Date { get; set; }

        [JsonProperty("Cur_Abbreviation")]
        public string Abbreviation { get; set; }

        [JsonProperty("Cur_Name")]
        public string Name { get; set; }

        [JsonProperty("Cur_OfficialRate")]
        public double OfficialRate { get; set; }
    }
}
