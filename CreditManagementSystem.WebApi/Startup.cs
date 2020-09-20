using AutoMapper;
using CreditManagementSystem.Common;
using CreditManagementSystem.Data.EntityFramework;
using CreditManagementSystem.Data.EntityFramework.DependencyInjection;
using CreditManagementSystem.Domain.Handler.DependencyInjection;
using CreditManagementSystem.WebApi.Filters;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace CreditManagementSystem.WebApi
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
            string connectionStrings = this.Configuration.GetConnectionString("CMS_Api_Main");

            services.AddDataEFServices(connectionStrings);

            services.AddDomainHandlerServices();

            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(ValidationActionModelAttribute));
            })
                .AddDataAnnotationsLocalization()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                })
                .AddFluentValidation(options =>
                {
                    options.RegisterValidatorsFromAssemblyContaining<Startup>();
                });

            services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            });

            services.AddAutoMapper(typeof(Startup));

            services.AddSwaggerGen(options =>
            {
                options.CustomSchemaIds(t => t.FullName);
                options.DescribeAllParametersInCamelCase();

                var versions = new[] { "v1" };

                foreach (var version in versions)
                {
                    options.SwaggerDoc(version, new OpenApiInfo
                    {
                        Version = version,
                        Title = "CreditManagementSystem API",
                        Description = "System for the control and management of credits",
                        TermsOfService = new Uri("https://creditmanagementsystem/termsOfService"),
                        Contact = new OpenApiContact
                        {
                            Name = "Dayron Jesús Díaz Diego",
                            Email = "dj.diazdiego@gmail.com",
                        },
                        License = new OpenApiLicense
                        {
                            Name = "License Name",
                            Url = new Uri("https://creditmanagementsystem/licence")
                        }
                    });
                }

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CreditManagementSystem API V1");
                c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
                c.RoutePrefix = string.Empty;
            });

            Task.Run(async () =>
            {
                using var dbContext = provider.GetService<CreditManagementSystemDbContext.CreditManagementSystemReadWriteDbContext>();
                await Utils.ApplyPenndingMigrations(dbContext);
                await Utils.ApplySeed(provider);
            }).Wait();
        }
    }
}
