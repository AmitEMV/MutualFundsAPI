using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using MutualFundsAPI.Helpers;
using MutualFundsAPI.Implementation;
using MutualFundsAPI.Interfaces;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;

namespace MutualFundsAPI
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
            services.AddTransient<IDBConnector, AppDb>(_ => new AppDb(Configuration["ConnectionStrings:DefaultConnection"]));
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<IPortfolioService, PortfolioService>();

            services.AddCors(options =>
            {
                options.AddPolicy("policy",
                                  builder =>
                                  {
                                      builder.AllowAnyOrigin()
                                             .AllowAnyMethod()
                                             .AllowAnyHeader();
                                  });
            });

            services.AddControllers();
            //services.AddAuthentication(CertificateAuthenticationDefaults.AuthenticationScheme).AddCertificate(options =>
            //{
            //    new CertificateAuthenticationOptions()
            //    {
            //        AllowedCertificateTypes = CertificateTypes.All
            //    };
            //});
            services.AddSwaggerGen(sg =>
            {
                sg.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Portfolio Tracker API",
                    Version = "v1",
                    Description = @"APIs for Portfolio Tracker Blazor WebAssembly app. The service performs all CRUD operations against a MySQL database. ",
                    License = new Microsoft.OpenApi.Models.OpenApiLicense
                    {
                        Name = "Use under MIT",
                        Url = new System.Uri("https://mit-license.org/")
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                sg.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseSwagger();
            app.UseSwaggerUI(sg =>
            {
                sg.SwaggerEndpoint("/swagger/v1/swagger.json", "Portfolio Tracker API");
                sg.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseCors("policy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
