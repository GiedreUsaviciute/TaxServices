using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using TaxAPI.Data;

namespace WebApplication1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<TaxDbOptions>(Configuration.GetSection("TaxDatabaseSettings"));
            services.AddSingleton<IDatabaseContext, DatabaseContext>();

            services.AddCors();

            services
               .AddControllers(x => x.EnableEndpointRouting = false)
               .SetCompatibilityVersion(CompatibilityVersion.Latest)
               .AddNewtonsoftJson(options =>
               {
                   options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
               });

            services.AddTransient<ITaxRepository, TaxRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
                builder.WithOrigins("*")
                    .AllowAnyMethod()
                    .AllowAnyHeader());

            app.UseMvc();
        }
    }
}
