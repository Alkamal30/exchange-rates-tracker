using System;

namespace Application.Models
{
    public class CurrencyRate
    {
        public DateTime Date { get; set; }
        
        public string Abbreviation { get; set; }
        
        public string Name { get; set; }

        public double OfficialRate { get; set; }
    }
}
