using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using gkama.graph.ql.data;

namespace gkama.graph.ql.services
{
    public interface ICountryRepository
    {
        Task<IEnumerable<Country>> GetCountriesAsync();
        Task<Country> GetCountryAsync(int? geoname_id = null, string code = null);
        Task<CountryPostalCode> GetCountryPostalCodesAsync(string code);
    }
}
