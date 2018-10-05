using MediatR;
using Microsoft.AspNetCore.Http;

namespace Document.Api.Commands
{
    public class UploadFileCommand:IRequest<int>
    {
        public IFormFile File { get; set; }
        public int UserId { get; set; }
    }
}
