using System.Collections.Generic;

using gkama.graph.ql.data;

namespace gkama.graph.ql.services
{
    public interface ICountryRepository
    {
        IEnumerable<Country> GetAll();
        Country GetCountry(int? geoname_id = null, string code = null);
    }
}
