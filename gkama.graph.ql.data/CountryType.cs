using GraphQL.Types;

namespace gkama.graph.ql.data
{
    public class CountryType : ObjectGraphType<Country>
    {
        public CountryType()
        {
            Field(a => a.geoname_id);
            Field(a => a.code);
            Field(a => a.name);
            Field(a => a.iso_numeric);
            Field(a => a.continent);
            Field(a => a.continent_name);
            Field(a => a.capital);
            Field(a => a.population);
            Field(a => a.currency_code);

            Field<ListGraphType<CountryNeighbourType>>("neighbours", resolve: context => context.Source.neighbour_countries);
            Field<CountryPostalCodeType>("postalcodes", resolve: context => context.Source.postal_codes);
        }
    }
}
