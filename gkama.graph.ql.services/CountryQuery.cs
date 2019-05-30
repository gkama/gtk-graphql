using GraphQL.Types;

using gkama.graph.ql.data;

namespace gkama.graph.ql.services
{
    public class CountryQuery : ObjectGraphType
    {
        public CountryQuery(ICountryRepository repo)
        {
            Field<ListGraphType<CountryType>>(
                "countries",
                resolve: context => repo.GetAll()
                );
        }
    }
}
