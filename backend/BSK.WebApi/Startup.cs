using BSK.Repository;
using BSK.WebApi.Configuration;
using BSK.WebApi.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace BSK.WebApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMediator();
            services.AddResponseCaching();
            services.AddSingleton<IContext, Context>();
            services.AddCors(o => o.AddDefaultPolicy(b => b
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
            ));
            services.AddSwagger();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if(env.IsDevelopment()) app.UseDeveloperExceptionPage();
            app.UseResponseCaching();
            app.UseCors();
            app.UseMiddleware<TraceMiddleware>();
            app.UseMiddleware<ETagMiddleware>();
            app.UseMiddleware<StopwatchMiddleware>();
            app.UseSwashbuckleSwagger();
            app.UseMvc();
        }
    }
}
