using BSK.Infrastructure.Exceptions;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BSK.WebApi.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly IHostingEnvironment _environment;

        public ExceptionFilter(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        public void OnException(ExceptionContext context)
        {
            switch(context.Exception)
            {
                case NotFoundException exception:
                    if(_environment.IsDevelopment())
                        context.Result = new NotFoundObjectResult(exception);
                    else
                        context.Result = new NotFoundResult();
                    break;
                case ValidationException exception:
                    var validationResult = new ValidationResult(exception.Errors);
                    validationResult.AddToModelState(context.ModelState, null);
                    context.Result = new BadRequestObjectResult(context.ModelState);
                    break;
            }
        }
    }
}