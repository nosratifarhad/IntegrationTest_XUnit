using ECommerce.Api.Domain;
using ECommerce.Api.Infra.Repositories.ReadRepositories.ProductReadRepositories;
using ECommerce.Api.Infra.Repositories.WriteRepositories.ProductWriteRepositories;
using ECommerce.Api.Services;
using ECommerce.Api.Services.Contract;
using Microsoft.OpenApi.Models;

namespace ECommerce.Api
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
            services.AddControllers();

            #region [ Application ]

            services.AddScoped<IProductService, ProductService>();

            #endregion [Application]

            #region [ Infra - Data ]

            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();

            #endregion [ Infra - Data EventSourcing ]

            #region [ Swagger ]
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ECommerce.Api", Version = "v1" });
            });
            #endregion [ Swagger ]
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ECommerce.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

}