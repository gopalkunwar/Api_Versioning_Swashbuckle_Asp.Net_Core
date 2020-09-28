using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api_Versioing_Swashbuckle_Asp.Net_Core.Filters;
using Api_Versioing_Swashbuckle_Asp.Net_Core.Infra.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Api_Versioing_Swashbuckle_Asp.Net_Core
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
            services.AddDbContext<ApplicationDbContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllers();
            services.AddApiVersioning(options => {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1,0);
                options.ApiVersionReader = ApiVersionReader.Combine(new QueryStringApiVersionReader("v"),
                                                                    new HeaderApiVersionReader("v"));
            });

            services.AddSwaggerGen(o=> {
                o.SwaggerDoc("v1.0", new OpenApiInfo 
                                    { 
                                        Title="Api Versioning using Swashbuckle",
                                        Version="v1.0"
                                    });

                o.SwaggerDoc("v2.0", new OpenApiInfo
                {
                    Title = "Api Versioning using Swashbuckle",
                    Version = "v2.0"
                });

                o.OperationFilter<RemoveVersionParameterFilter>();
                o.DocumentFilter<ReplaceVersionWithExactValueInPathFilter>();
                o.EnableAnnotations();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(
                c =>
                {
                    c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "v1");
                    c.SwaggerEndpoint("/swagger/v2.0/swagger.json", "v2");


                });
        }
    }
}
