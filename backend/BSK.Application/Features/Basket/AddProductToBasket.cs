using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BSK.Infrastructure.Exceptions;
using BSK.Repository;
using FluentValidation;
using MediatR;

namespace BSK.Application.Features.Basket
{
    public class AddProductToBasket
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
                RuleFor(p => p.ProductId).NotEmpty();
                RuleFor(p => p.UserId).NotEmpty();
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

                var product = _bskContext.Products.FirstOrDefault(p => p.Id == command.ProductId);
                if(product == null) throw new NotFoundException("Product not found");

                var basket = _bskContext.Baskets.FirstOrDefault(b => b.UserId == command.UserId);
                if(basket == null)
                {
                    basket = new Models.Database.Basket(command.UserId);
                    _bskContext.Baskets.Add(basket);
                }

                basket.Products.Add(product);

                return Task.FromResult(new Result { Success = true });
            }
        }
    }
}