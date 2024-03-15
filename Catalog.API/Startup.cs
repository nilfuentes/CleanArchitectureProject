using Catalog.Core.Repositories;
using Catalog.Infraestructure.Data;
using Catalog.Infraestructure.Data.Repositories;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;


namespace Catalog.API
{
    public class Startup
    {
        public IConfiguration Configuration;
        public  Startup(IConfiguration configuration) 
        {
            this.Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddHealthChecks()
                .AddMongoDb(Configuration["DatabaseSettings:ConnectionString"], "Catalog Mongo Db Health Check",
                HealthStatus.Degraded);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo() 
                {
                    Title = "Catalog.API", Version = "v1" 
                });
            });
            services.AddAutoMapper(typeof(Startup));            
            services.AddScoped<ICatalogContext, CatalogContext>();
            services.AddScoped<IProductRespository, ProductRepository>();
            services.AddScoped<IProductTypeRepository, ProductRepository>();
            services.AddScoped<IBrandRespository, ProductRepository>();
        }

        public void Configure(IApplicationBuilder appBuilder, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                appBuilder.UseDeveloperExceptionPage();
                appBuilder.UseSwagger();
                appBuilder.UseSwaggerUI(c=>c.SwaggerEndpoint("/swagger/v1/swagger.json","Catalog.API v1"));
            }

            appBuilder.UseRouting();
            appBuilder.UseStaticFiles();
            appBuilder.UseAuthorization();
            appBuilder.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health", new HealthCheckOptions()
                {
                    Predicate=_=> true,
                    ResponseWriter=UIResponseWriter.WriteHealthCheckUIResponse
                });
            }
            );
            
        }
    }
}
