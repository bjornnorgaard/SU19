using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BSK.Infrastructure.Exceptions;
using BSK.Repository;
using FluentValidation;
using MediatR;

namespace BSK.Application.Features.Basket
{
    public class GetBasket
    {
        public class Command : IRequest<Result>
        {
            public int UserId { get; set; }
        }

        public class Result : IRequest
        {
            public Models.Database.Basket Basket { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(p => p.UserId).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command, Result>
        {
            private readonly IContext _context;

            public Handler(IContext context)
            {
                _context = context;
            }

            public Task<Result> Handle(Command command, CancellationToken cancellationToken)
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == command.UserId);
                if(user == null) throw new NotFoundException("User not found");

                var basket = _context.Baskets.FirstOrDefault(b => b.UserId == command.UserId);
                if(basket == null) basket = new Models.Database.Basket(command.UserId);

                return Task.FromResult(new Result { Basket = basket });
            }
        }
    }
}