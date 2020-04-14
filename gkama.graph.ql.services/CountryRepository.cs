using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

using gkama.graph.ql.data;

namespace gkama.graph.ql.services
{
    public class CountryRepository : ICountryRepository
    {
        public readonly CountryContext _context;
        public readonly ILogger _logger;

        public CountryRepository(CountryContext context, ILogger<CountryRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        private IQueryable<Country> GetCountryQuery()
        {
            return _context
                .countries
                    .Include(x => x.neighbour_countries)
                        .ThenInclude(x => x.postal_codes)
                    .Include(x => x.postal_codes)
                .AsQueryable();
        }

        public async Task<IEnumerable<Country>> GetCountriesAsync()
        {
            return await GetCountryQuery()
                .ToListAsync();
        }

        public async Task<Country> GetCountryAsync(int? geoname_id = null, string code = null)
        {
            if (geoname_id != null)
                return await GetCountryQuery()
                    .FirstOrDefaultAsync(x => x.geoname_id == geoname_id);
            else if (code != null)
                return await GetCountryQuery()
                    .FirstOrDefaultAsync(x => x.code == code);
            else
                return null;
        }

        public async Task<Country> AddCountryAsync(Country country)
        {
            if (country == null)
                throw new ArgumentNullException($"cannot add a 'country' that is null");

            try
            {
                await _context.countries
                    .AddAsync(country);

                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }

            return await GetCountryAsync(country.geoname_id);
        }

        private IQueryable<CountryPostalCode> GetCountryPostalCodesQuery()
        {
            return _context.postal_codes
                .AsQueryable();
        }

        public async Task<CountryPostalCode> GetCountryPostalCodesAsync(string code)
        {
            return await GetCountryPostalCodesQuery()
                .FirstOrDefaultAsync(x => x.code == code);
        }
    }
}
