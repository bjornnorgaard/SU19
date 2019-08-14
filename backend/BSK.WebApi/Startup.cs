using BSK.Application;
using BSK.Repository;
using BSK.WebApi.Middleware;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace BSK.WebApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(AssemblyAnchor));

            var validators = AssemblyScanner.FindValidatorsInAssemblies(new[] { typeof(AssemblyAnchor).Assembly });
            validators.ForEach(validator => services.AddTransient(validator.InterfaceType, validator.ValidatorType));

            services.AddSingleton<IRepository, Repository.Repository>();

            services.AddSwaggerGen(o =>
            {
                o.SwaggerDoc("v1", new Info {Title = "Basket API", Version = "v1"});
                o.CustomSchemaIds(x => x.FullName);
            });

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<TraceMiddleware>();
            app.UseMiddleware<StopwatchMiddleware>();

            app.UseSwagger();
            app.UseSwaggerUI(o => o.SwaggerEndpoint("/swagger/v1/swagger.json", "api v1"));

            app.UseMvc();
        }
    }
}
