using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bubbio.Core;
using Bubbio.Persist.Mongo;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Bubbio.WebApi
{
    public class Startup
    {
        private IConfiguration Configuration { get; set; }
        private string _connectionString;
        private string _schema;

        public Startup(IConfiguration configuration)
        {
            InitializeConfiguration(configuration);
        }

        private void InitializeConfiguration(IConfiguration configuration)
        {
            var connectionString = configuration["ConnectionString"];
            var schema = configuration["Schema"];

            if(string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException("ConnectionString required in appsettings.json");

            if(string.IsNullOrWhiteSpace(schema))
                throw new InvalidOperationException("Schema required in appsettings.json");

            Configuration = configuration;
            _connectionString = connectionString;
            _schema = schema;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddTransient<IRepository>(s => new Repository(_connectionString, _schema));
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
