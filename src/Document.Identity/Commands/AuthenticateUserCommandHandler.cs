using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Document.Identity.Models;
using Document.Data.DbContexts;
using Document.Common.Exceptions;
using Document.Common;
using System.Net;

namespace Document.Identity.Commands
{
    public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, UserModel>
    {
        private readonly IDocumentDbContext dbContext;

        public AuthenticateUserCommandHandler(IDocumentDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<UserModel> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            //TODO: Password need to be hashed
            var user = await dbContext.Users.SingleOrDefaultAsync(u => u.Username == request.Username && u.Password == request.Password);

            if (user == null)
            {
                throw new AppException(Constants.ErrorCodes.InvalidCredentials, HttpStatusCode.Unauthorized);
            }

            return new UserModel()
            {
                Id = user.Id,
                Role = user.Role,
                Username = user.Username
            };
        }
    }
}
