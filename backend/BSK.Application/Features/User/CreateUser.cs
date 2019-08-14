using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BSK.Repository;
using FluentValidation;
using MediatR;

namespace BSK.Application.Features.User
{
    public class CreateUser
    {
        public class Command : IRequest<Result>
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class Result : IRequest
        {
            public Models.Database.User User { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(p => p.Id).NotEmpty();
                RuleFor(p => p.Name).NotEmpty();
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
                var user = _context.Users.FirstOrDefault(u => u.Id == command.Id);
                if(user != null) throw new ArgumentException("User already exists");

                user = new Models.Database.User { Id = command.Id, Name = command.Name };
                _context.Users.Add(user);

                return Task.FromResult(new Result { User = user });
            }
        }
    }
}