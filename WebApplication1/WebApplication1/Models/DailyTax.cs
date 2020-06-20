using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxAPI.Models
{
    public class DailyTax : Tax
    {
        public IEnumerable<DateTime> Dates { get; set; }
    }
}
