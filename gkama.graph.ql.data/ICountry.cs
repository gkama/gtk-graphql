namespace gkama.graph.ql.data
{
    public interface ICountry
    {
        int geoname_id { get; set; }
        string code { get; set; }
        string name { get; set; }
    }
}
