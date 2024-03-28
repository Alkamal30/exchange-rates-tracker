using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
