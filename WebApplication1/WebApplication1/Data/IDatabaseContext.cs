using MongoDB.Driver;
using TaxAPI.Models;

namespace TaxAPI.Data
{
    public interface IDatabaseContext
    {
        IMongoCollection<MunicipalityTax> Taxes { get; }
    }
}
