﻿using System.Net;
using System.Reflection;
using CodeChallenge.Filters;
using CodeChallenge.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace CodeChallenge
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("AllowEverything", builder => { builder.AllowAnyOrigin(); }));
            services.AddLogging();
            services.AddMvc(options => { options.Filters.Add(typeof(LoggingFilter)); });
            services.AddScoped<IQuoteRepository, QuoteRepository>();
            
            services.Configure<KestrelServerOptions>(opts => { opts.Listen(IPAddress.Any, 7200); });
            services.AddSwaggerGen(c =>
            {
                c.CustomSchemaIds(x => x.FullName);
                c.SwaggerDoc("v1", new Info() { Title = Assembly.GetEntryAssembly()?.GetName().Name });
                c.IncludeXmlComments("CodeChallenge.xml");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (!env.IsEnvironment("Production"))
            {
                string rootPath = "api/quotes/";
                app.UseDeveloperExceptionPage();
                app.UseSwagger(c =>
                {
                    c.RouteTemplate = rootPath + "swagger/{documentName}/swagger.json";
                    
                });
                app.UseSwaggerUI(c =>
                {
                    c.RoutePrefix = "swagger";
                    c.SwaggerEndpoint($"/{rootPath}swagger/v1/swagger.json", Assembly.GetEntryAssembly()?.GetName().Name);
                    
                });
            }
            app.UseCors("Allow");
            app.UseMvc();
        }
    }
}