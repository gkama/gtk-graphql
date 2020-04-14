using System;

using GraphQL;
using GraphQL.Types;

namespace gkama.graph.ql.services
{
    public class CountrySchema : Schema
    {
        public CountrySchema(IDependencyResolver resolver)
            : base(resolver)
        {
            Query = resolver.Resolve<CountryQuery>();
            Mutation = resolver.Resolve<CountryMutation>();
        }
    }
}
