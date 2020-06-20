using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxAPI.Models
{
    public class PeriodicTax : Tax
    {
        [BsonRepresentation(BsonType.String)]
        public DateTime StartDate { get; set; }

        [BsonRepresentation(BsonType.String)]
        public DateTime EndDate { get; set; }
    }
}
