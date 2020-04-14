﻿using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using GraphQL;
using GraphQL.Client.Http;

using gkama.graph.ql.services;

namespace gkama.graph.ql.core.Controllers
{
    [Route("api")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        public readonly ICountryRepository _repo;
        
        public CountryController(ICountryRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        [Route("countries/all")]
        [HttpGet]
        public async Task<IActionResult> GetCountriesAsync()
        {
            return Ok(await _repo.GetCountriesAsync());
        }

        [Route("graphql/countries/all")]
        [HttpGet]
        public async Task<IActionResult> GetCountries()
        {
            using (var graphQLClient = new GraphQLHttpClient("http://localhost:5000/graphql", null))
            {
                var query = new GraphQLRequest
                {
                    Query = @"{
                        countries {
                          geoname_id
                          code
                          name
                          iso_numeric
                          continent
                          continent_name
                          capital
                          population
                          currency_code
                          neighbours {
                            geoname_id
                            code
                            name
                            country_geoname_id
                          }
                        }
                      }",
                };

                return Ok(await graphQLClient.SendQueryAsync<object>(query));
            }
        }
    }
}
