using MediatR;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Document.Common;
using System;

namespace Document.Identity.Commands
{
    public class GenerateTokenCommandHandler : IRequestHandler<GenerateTokenCommand, string>
    {
        private readonly IOptions<AuthenticationSettings> authSettings;

        public GenerateTokenCommandHandler(IOptions<AuthenticationSettings> authSettings)
        {
            this.authSettings = authSettings;
        }

        public Task<string> Handle(GenerateTokenCommand request, CancellationToken cancellationToken)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings.Value.SecretKey));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokeOptions = new JwtSecurityToken(
                issuer: authSettings.Value.Authority,
                audience: authSettings.Value.Audience,
                claims: GetTokenClaims(request),
                expires: DateTime.Now.AddMinutes(authSettings.Value.ExpireInMinutes),
                signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

            return Task.FromResult(tokenString);
        }

        private IEnumerable<Claim> GetTokenClaims(GenerateTokenCommand command)
        {
            return new List<Claim>()
            {
                new Claim(ClaimTypes.Sid, command.UserId.ToString()),
                new Claim(ClaimTypes.Name, command.Username),
                new Claim(ClaimTypes.Role, command.UserRole)
            };
        }
    }
}
