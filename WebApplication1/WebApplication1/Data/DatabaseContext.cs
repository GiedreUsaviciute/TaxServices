using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TaxAPI.Models;

namespace TaxAPI.Data
{
    public class DatabaseContext : IDatabaseContext
    {
        private readonly IMongoDatabase _context;

        public DatabaseContext(IOptions<TaxDbOptions> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _context = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<MunicipalityTax> Taxes => _context.GetCollection<MunicipalityTax>("taxes");
    }
}
