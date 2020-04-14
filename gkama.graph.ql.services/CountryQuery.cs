using System;

using GraphQL.Types;

using gkama.graph.ql.data;

namespace gkama.graph.ql.services
{
    public class CountryQuery : ObjectGraphType
    {
        public CountryQuery(ICountryRepository _repo)
        {
            FieldAsync<ListGraphType<CountryType>>(
                "countries",
                resolve: async context => await _repo.GetCountriesAsync()
                );

            FieldAsync<CountryType>(
                "country",
                arguments: new QueryArguments(
                    new QueryArgument<IdGraphType> { Name = "geoname_id" },
                    new QueryArgument<IdGraphType> { Name = "code" }
                    ),
                resolve: async context =>
                {
                    var id = context.GetArgument<int?>("geoname_id");
                    var code = context.GetArgument<string>("code");

                    return await _repo.GetCountryAsync(id, code);
                });

            FieldAsync<CountryPostalCodeType>(
                "countryPostalCode",
                arguments: new QueryArguments(
                    new QueryArgument<IdGraphType> { Name = "code" }
                ),
                resolve: async context =>
                {
                    var code = context.GetArgument<string>("code");

                    return await _repo.GetCountryPostalCodesAsync(code);
                });
        }
    }
}
