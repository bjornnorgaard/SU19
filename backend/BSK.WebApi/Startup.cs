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
            services.AddSingleton<IContext, Context>();
            services.AddSwagger();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if(env.IsDevelopment()) app.UseDeveloperExceptionPage();
            app.UseMiddleware<TraceMiddleware>();
            app.UseMiddleware<StopwatchMiddleware>();
            app.UseSwashbuckleSwagger();
            app.UseMvc();
        }
    }
}
