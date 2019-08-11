using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BSK.Infrastructure;
using BSK.Models;
using BSK.Repository;
using FluentValidation;
using MediatR;

namespace BSK.Application.Features
{
    public class GetBasket
    {
        public class Command : IRequest<Result>
        {
            public int UserId { get; set; }
        }

        public class Result : IRequest
        {
            public Basket Basket { get; set; }
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
            private readonly IRepository _repository;

            public Handler(IRepository repository)
            {
                _repository = repository;
            }

            public Task<Result> Handle(Command command, CancellationToken cancellationToken)
            {
                var user = _repository.Users.FirstOrDefault(u => u.Id == command.UserId);
                if(user == null) throw new NotFoundException("User not found");

                var basket = _repository.Baskets.FirstOrDefault(b => b.UserId == command.UserId);
                if(basket == null) throw new NotFoundException("User not found");
                                                                    
                return Task.FromResult(new Result { Basket = basket });
            }
        }
    }
}