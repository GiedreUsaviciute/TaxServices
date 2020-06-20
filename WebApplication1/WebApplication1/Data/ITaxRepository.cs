using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxAPI.Models;

namespace TaxAPI.Data
{
    public interface ITaxRepository
    {
        Task AddAsync(MunicipalityTax entity);

        Task<IEnumerable<MunicipalityTax>> AllAsync();

        Task<MunicipalityTax> FirstOrDefault(string id);

        Task<bool> UpdateAsync(MunicipalityTax entity);

        Task Delete(MunicipalityTax entity);
    }
}
