using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxAPI.Models
{
    public class TaxRequestBody
    {
        public string Municipality { get; set; }
        public string Date { get; set; }
    }
}
