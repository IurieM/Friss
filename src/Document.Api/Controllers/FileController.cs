using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Document.Api.Queries;
using Document.Api.Commands;
using Microsoft.AspNetCore.Authorization;
using Document.Common;
using Document.Identity.Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;

namespace Document.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IMediator mediator;

        public FileController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var files = await mediator.Send(new FileListQuery());
            return Ok(files);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var downloadedFile = await mediator.Send(new DownloadFileCommand() { FileId = id });
            return File(downloadedFile.Content, downloadedFile.ContentType, downloadedFile.Name);
        }

        [HttpPost]
        [Authorize(Roles = Constants.Roles.Admin)]
        public async Task<IActionResult> Post(IFormFile file)
        {
            var command = new UploadFileCommand()
            {
                File = file,
                UserId = User.Identity.UserId()
            };
            var fileId = await mediator.Send(command);
            return Ok(fileId);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Constants.Roles.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            var fileId = await mediator.Send(new DeleteFileCommand() { FileId = id });
            return Ok(fileId);
        }
    }
}
