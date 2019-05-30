using System.Collections.Generic;
using System.Threading.Tasks;

namespace gkama.graph.ql.data
{
    public class FakeManager
    {
        /*
         * fake data manager
         * 
         * courtesy to http://www.geonames.org/ for the data set
         * the data is real from geonames but it's used as test data here
         */
        private readonly CountryContext context;

        public FakeManager(CountryContext context)
        {
            this.context = context;
        }

        public async Task UseFakeContext()
        {
            await context
                .countries
                .AddRangeAsync(GetFakeCountries());

            await context
                .neighbour_countries
                .AddRangeAsync(GetFakeNeighborCountries());

            await context.SaveChangesAsync();
        }

        public IEnumerable<Country> GetFakeCountries()
        {
            return new List<Country>()
            {
                new Country()
                {
                    geoname_id = 6252001,
                    code = "US",
                    name = "United States",
                    iso_numeric = 840,
                    continent = "NA",
                    continent_name = "North America",
                    capital = "Washington",
                    population = 310232863,
                    currency_code = "USD"
                },
                new Country()
                {
                    geoname_id = 2510769,
                    code = "ES",
                    name = "Spain",
                    iso_numeric = 724,
                    continent = "EU",
                    continent_name = "Europe",
                    capital = "Madrid",
                    population = 46505963,
                    currency_code = "EUR"
                }
            };
        }

        public IEnumerable<CountryNeighbour> GetFakeNeighborCountries()
        {
            return new List<CountryNeighbour>()
            {
                new CountryNeighbour()
                {
                    geoname_id = 6251999,
                    code = "CA",
                    name = "Canada",
                    country_geoname_id = 6252001
                },
                new CountryNeighbour()
                {
                    geoname_id = 3996063,
                    code = "MX",
                    name = "Mexico",
                    country_geoname_id = 6252001
                },
                new CountryNeighbour()
                {
                    geoname_id = 2264397,
                    code = "PT",
                    name = "Portugal",
                    country_geoname_id = 2510769
                },
                new CountryNeighbour()
                {
                    geoname_id = 3017382,
                    code = "FR",
                    name = "France",
                    country_geoname_id = 2510769
                }
            };
        }
    }
}
