using System;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;

using gkama.graph.ql.data;
using gkama.graph.ql.services;

namespace gkama.graph.ql.core
{
    public class Startup
    {
        public readonly IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }       

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();

            services.AddHealthChecks();

            /*
             * Transient objects are always different; a new instance is provided to every controller and every service
             * Scoped objects are the same within a request, but different across different requests
             * Singleton objects are the same for every object and every request
             */
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<FakeManager>();
            services.AddScoped<CountryType>();
            services.AddScoped<CountryNeighbourType>();
            services.AddScoped<CountryQuery>();
            services.AddScoped<CountrySchema>();

            services.AddDbContext<CountryContext>(o => o.UseInMemoryDatabase(nameof(CountryContext)));

            /*
             * graphql configuration 
             * useful resources
             * https://github.com/graphql-dotnet/graphql-dotnet
             * https://github.com/graphql-dotnet/graphql-dotnet/blob/master/docs/src/getting-started.md
             * https://github.com/graphql-dotnet/examples
             */
            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));            
            services.AddGraphQL(o => { o.ExposeExceptions = false; })
             .AddGraphTypes(ServiceLifetime.Scoped);

            services.AddMvc()
               .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
               .AddJsonOptions(o =>
               {
                   o.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
               });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider services)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            services.GetRequiredService<FakeManager>()
                .UseFakeContext()
                .Wait();

            app.UseGraphQL<CountrySchema>();
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());

            app.UseHealthChecks("/ping");
            app.UseMvc();
        }
    }
}
