using GraphQL.Types;

namespace gkama.graph.ql.data
{
    public class CountryPostalCodeType : ObjectGraphType<CountryPostalCode>
    {
        public CountryPostalCodeType()
        {
            Field(a => a.code);
            Field(a => a.num_postal_codes);
            Field(a => a.max_postal_code);
            Field(a => a.min_postal_code);
        }
    }
}
