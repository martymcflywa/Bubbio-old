using System;
using Bubbio.Core;
using Bubbio.Domain.Validation;
using Bubbio.Persist.Mongo;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bubbio.WebApi
{
    public class Startup
    {
        private IConfiguration Configuration { get; set; }
        private string _connectionString;
        private string _schema;
        private string _collection;

        public Startup(IConfiguration configuration)
        {
            InitializeConfiguration(configuration);
        }

        private void InitializeConfiguration(IConfiguration configuration)
        {
            var connectionString = configuration["ConnectionString"];
            var schema = configuration["Schema"];
            var collection = configuration["Collection"];

            if(string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException("ConnectionString required in appsettings.json");

            if(string.IsNullOrWhiteSpace(schema))
                throw new InvalidOperationException("Schema required in appsettings.json");

            if(string.IsNullOrWhiteSpace(collection))
                throw new InvalidOperationException("Collection required in appsettings.json");

            Configuration = configuration;
            _connectionString = connectionString;
            _schema = schema;
            _collection = collection;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton<IRepository>(s => new Repository(_connectionString, _schema, _collection));
            services.AddSingleton<IValidate, EventValidator>();
            services.AddSingleton<IJsonDeserialize, EventJsonDeserializer>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
