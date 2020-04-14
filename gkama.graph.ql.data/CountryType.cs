using GraphQL.Types;

namespace gkama.graph.ql.data
{
    public class CountryType : ObjectGraphType<Country>
    {
        public CountryType()
        {
            Field(x => x.geoname_id);
            Field(x => x.code);
            Field(x => x.name);
            Field(x => x.iso_numeric);
            Field(x => x.continent);
            Field(x => x.continent_name);
            Field(x => x.capital);
            Field(x => x.population);
            Field(x => x.currency_code);

            Field<ListGraphType<CountryNeighbourType>>("neighbours", resolve: context => context.Source.neighbour_countries);
            Field<CountryPostalCodeType>("postalcodes", resolve: context => context.Source.postal_codes);
        }
    }

    public class InputCountryType : InputObjectGraphType<Country>
    {
        public InputCountryType()
        {
            Field(x => x.geoname_id);
            Field(x => x.code);
            Field(x => x.name);
            Field(x => x.iso_numeric);
            Field(x => x.continent);
            Field(x => x.continent_name);
            Field(x => x.capital);
            Field(x => x.population);
            Field(x => x.currency_code);
        }
    }
}
