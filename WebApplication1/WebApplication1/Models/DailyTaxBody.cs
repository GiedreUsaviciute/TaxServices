using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxAPI.Models
{
    public class DailyTaxBody : Tax
    {
        public IEnumerable<string> Dates { get; set; }
    }
}
