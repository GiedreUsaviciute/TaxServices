using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxAPI.Models
{
    [BsonIgnoreExtraElements]
    public class MunicipalityTaxBody
    {
        [BsonId]
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public PeriodicTaxBody? Yearly { get; set; }
        public PeriodicTaxBody? Monthly { get; set; }
        public PeriodicTaxBody? Weekly { get; set; }
        public DailyTaxBody? Daily { get; set; }
    }
}
