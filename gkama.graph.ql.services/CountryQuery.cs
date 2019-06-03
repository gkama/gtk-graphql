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

            Field<CountryType>(
                "country",
                arguments: new QueryArguments(
                    new QueryArgument<IdGraphType> { Name = "code" }
                    ),
                resolve: context =>
                {
                    var code = context.GetArgument<string>("code");
                    return repo.GetCountry(code);
                });
        }
    }
}
