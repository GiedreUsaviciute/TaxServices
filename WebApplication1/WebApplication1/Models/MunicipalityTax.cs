using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxAPI.Models
{
    [BsonIgnoreExtraElements]
    public class MunicipalityTax
    {
        [BsonId]
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public PeriodicTax? Yearly { get; set; }
        public PeriodicTax? Monthly { get; set; }
        public PeriodicTax? Weekly { get; set; }
        public DailyTax? Daily { get; set; }
    }
}
