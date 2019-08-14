using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace BSK.WebApi.Configuration
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(o =>
            {
                o.SwaggerDoc("v1", new Info { Title = "Basket API", Version = "v1" });
                o.CustomSchemaIds(x => x.FullName);
            });
            return services;
        }

        public static IApplicationBuilder UseSwashbuckleSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(o => o.SwaggerEndpoint("/swagger/v1/swagger.json", "api v1"));
            return app;
        }
    }
}