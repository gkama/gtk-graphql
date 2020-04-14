using System;
using System.Collections.Generic;
using System.Text;

using GraphQL;
using GraphQL.Types;

using gkama.graph.ql.data;

namespace gkama.graph.ql.services
{
    public class CountryMutation : ObjectGraphType
    {
        public CountryMutation(ICountryRepository _repo)
        {
            FieldAsync<CountryType>(
                "addCountry",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<InputCountryType>> { Name = "country" }
                ),
                resolve: async context =>
                {
                    var country = context.GetArgument<Country>("country");

                    return await _repo.AddCountryAsync(country);
                });
        }
    }
}
