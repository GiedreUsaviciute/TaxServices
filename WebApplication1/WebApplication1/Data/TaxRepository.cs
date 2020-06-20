using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxAPI.Models;

namespace TaxAPI.Data
{
    public class TaxRepository : ITaxRepository
    {
        private readonly IDatabaseContext _context;

        public TaxRepository(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task AddAsync(MunicipalityTax entity)
        {
            await _context.Taxes.InsertOneAsync(entity);
        }

        public async Task<IEnumerable<MunicipalityTax>> AllAsync()
        {
            return await _context.Taxes.Find(b => true).ToListAsync();
        }

        public async Task<MunicipalityTax> FirstOrDefault(string name)
        {
            return await _context.Taxes.Find(t => t.Name == name).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateAsync(MunicipalityTax entity)
        {
            var result = await _context.Taxes.ReplaceOneAsync(b => b.Id == entity.Id, entity);

            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public Task Delete(MunicipalityTax entity)
        {
            throw new NotImplementedException();
        }
    }
}
