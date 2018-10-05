using Document.Identity.Models;
using MediatR;

namespace Document.Identity.Commands
{
    public class AuthenticateUserCommand: IRequest<UserModel>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
