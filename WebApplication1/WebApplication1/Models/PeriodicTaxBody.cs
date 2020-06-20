using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxAPI.Models
{
    public class PeriodicTaxBody : Tax
    {
        public string StartDate { get; set; }

        public string EndDate { get; set; }
    }
}
