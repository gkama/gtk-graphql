## gkama.graph.ql
sample project for `GraphQL` for `.NET Core`. it uses the following technologies:

- ASP.NET Core 2.2 API
- GraphQL with ui/playground
- Entity Framework Core for creating a in-memory database with sample datasets
- Defined query and schema for countries with neighbouring countries
- Docker image

the initial scaffolding is in `Start.cs` and then the rest is done through objects inheriting from GraphQL's classes. it defines a base class of `Country.cs`, `CountryNeighbour.cs`, `CountryPostalCode` and the corresponding `...Type` classes that inherit `ObjectGraphType<T>`. then the schema and query is defined, which are used by the `ui/playground` endpoint for the client side to work it and create queries

a docker image is also available (https://hub.docker.com/r/gkama/dotnet) via `docker pull gkama/dotnet:graphql`

#### example
a sample request would go like this

```
{
  countries {
    name
    neighbours {
      name
    }
    postalcodes {
        num_postal_codes
    }
  }
}
```

and the response

``` json
{
  "data": {
    "countries": [
      {
        "name": "Spain",
        "neighbours": [
          {
            "name": "Portugal"
          },
          {
            "name": "France"
          }
        ],
        "postalcodes": {
          "num_postal_codes": 37867
        }
      },
      {
        "name": "United States",
        "neighbours": [
          {
            "name": "Canada"
          },
          {
            "name": "Mexico"
          }
        ],
        "postalcodes": {
          "num_postal_codes": 41468
        }
      }
    ]
  }
}
```

#### resources
a little bit more about GraphQL for the ones unfamiliar - "it is an open-source data query and manipulation language for APIs, and a runtime for fulfilling queries with existing data. GraphQL was developed internally by Facebook in 2012 before being publicly released in 2015"

open sourced GraphQL project
- https://github.com/graphql/graphql-spec

open sourced GraphQL for .NET
- https://github.com/graphql-dotnet/graphql-dotnet
- https://github.com/graphql-dotnet/graphql-dotnet/blob/master/docs/src/getting-started.md
- https://github.com/graphql-dotnet/examples
