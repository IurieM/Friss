using Document.Identity.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Document.Api.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator mediator;

        public AuthenticationController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost, Route("login")]
        public async Task<IActionResult> Login([FromBody]AuthenticateUserCommand authenticateUser)
        {
            var userModel = await mediator.Send(authenticateUser);
            userModel.Token = await mediator.Send(new GenerateTokenCommand(userModel.Id, userModel.Username, userModel.Role));
            return Ok(userModel);
        }
    }
}
