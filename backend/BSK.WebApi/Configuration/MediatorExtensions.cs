using BSK.Application;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BSK.WebApi.Configuration
{
    public static class MediatorExtensions
    {
        public static IServiceCollection AddMediator(this IServiceCollection services)
        {
            services.AddMediatR(typeof(AssemblyAnchor));

            var validators = AssemblyScanner.FindValidatorsInAssemblies(new[] { typeof(AssemblyAnchor).Assembly });
            validators.ForEach(validator => services.AddTransient(validator.InterfaceType, validator.ValidatorType));

            return services;
        }
    }
}