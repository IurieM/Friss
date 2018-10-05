using Document.Identity.Commands;
using FluentValidation;

namespace Identity.Api.Validators
{
    public class AuthenticateUserValidator : AbstractValidator<AuthenticateUserCommand>
    {
        public AuthenticateUserValidator()
        {
            RuleFor(command => command.Username).NotEmpty();
            RuleFor(command => command.Password).NotEmpty();
        }
    }
}
