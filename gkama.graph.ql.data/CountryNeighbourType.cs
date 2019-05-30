using GraphQL.Types;

namespace gkama.graph.ql.data
{
    public class CountryNeighbourType : ObjectGraphType<CountryNeighbour>
    {
        public CountryNeighbourType()
        {
            Field(a => a.geoname_id);
            Field(a => a.code);
            Field(a => a.name);
            Field(a => a.country_geoname_id);
        }
    }
}
