using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BSK.Infrastructure.Exceptions;
using BSK.Repository;
using FluentValidation;
using MediatR;

namespace BSK.Application.Features.Basket
{
    public class RemoveProductFromBasket
    {
        public class Command : IRequest<Result>
        {
            public int ProductId { get; set; }
            public int UserId { get; set; }
        }

        public class Result : IRequest
        {
            public bool Success { get; set; }
            public string Error { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(p => p.UserId).NotEmpty();
                RuleFor(p => p.ProductId).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command, Result>
        {
            private readonly IBskContext _bskContext;

            public Handler(IBskContext bskContext)
            {
                _bskContext = bskContext;
            }

            public Task<Result> Handle(Command command, CancellationToken cancellationToken)
            {
                var user = _bskContext.Users.FirstOrDefault(u => u.Id == command.UserId);
                if(user == null) throw new NotFoundException("User not found");

                var basket = _bskContext.Baskets.FirstOrDefault(b => b.UserId == command.UserId);
                if(basket == null) return Task.FromResult(new Result { Success = true, Error = "Basket not found" });

                var product = basket.Products.FirstOrDefault(p => p.Id == command.ProductId);
                if (product == null) return Task.FromResult(new Result {Success = true, Error = "Product not found"});

                basket.Remove(product);

                return Task.FromResult(new Result { Success = true });
            }
        }
    }
}