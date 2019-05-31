﻿using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

using gkama.graph.ql.data;

namespace gkama.graph.ql.services
{
    public class CountryRepository : ICountryRepository
    {
        public readonly CountryContext context;
        public readonly ILogger log;

        public CountryRepository(CountryContext context, ILogger<CountryRepository> log)
        {
            this.context = context;
            this.log = log;
        }

        public IEnumerable<Country> GetAll()
        {
            return GetCountryQuery()
                .AsEnumerable();
        }

        private IQueryable<Country> GetCountryQuery()
        {
            return context
                .countries
                    .Include(x => x.neighbour_countries)
                    .Include(x => x.postal_codes)
                .AsQueryable();
        }
    }
}
