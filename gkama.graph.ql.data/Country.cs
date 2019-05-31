using System;
using System.Collections.Generic;

namespace gkama.graph.ql.data
{
    public class Country : ICountry
    {
        public int geoname_id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public int iso_numeric { get; set; }
        public string continent { get; set; }
        public string continent_name { get; set; }
        public string capital { get; set; }
        public int population { get; set; }
        public string currency_code { get; set; }

        public ICollection<CountryNeighbour> neighbour_countries { get; set; } = new List<CountryNeighbour>();
        public CountryPostalCode postal_codes { get; set; }
    }
}
