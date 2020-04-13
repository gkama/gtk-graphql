using System;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
            services.AddScoped<CountryPostalCodeType>();
            services.AddScoped<CountryQuery>();
            services.AddScoped<CountrySchema>();

            services.AddDbContext<CountryContext>(o => o.UseInMemoryDatabase(nameof(CountryContext)));

            /*
             * graphql configuration 
             * useful resources
             * https://github.com/graphql-dotnet/graphql-dotnet
             * https://github.com/graphql-dotnet/graphql-dotnet/blob/master/docs/src/getting-started.md
             * https://graphql-dotnet.github.io/docs/getting-started/introduction
             * https://github.com/graphql-dotnet/examples
             */
            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));            
            services.AddGraphQL(o => { o.ExposeExceptions = false; })
             .AddGraphTypes(ServiceLifetime.Scoped);

            services.Configure<KestrelServerOptions>(o => { o.AllowSynchronousIO = true; });

            services.AddMvc()
               .AddJsonOptions(o =>
               {
                   o.JsonSerializerOptions.WriteIndented = true;
               });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services)
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

            app.UseRouting();
            app.UseEndpoints(e =>
            {
                e.MapControllers();
            });
        }
    }
}
