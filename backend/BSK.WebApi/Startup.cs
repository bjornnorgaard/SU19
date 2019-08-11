﻿using BSK.Application;
using BSK.Repository;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

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

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
