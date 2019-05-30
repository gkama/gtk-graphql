namespace gkama.graph.ql.data
{
    public class CountryNeighbour : ICountry
    {
        public int geoname_id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public int country_geoname_id { get; set; }
    }
}
