using Document.Api.Models;
using MediatR;

namespace Document.Api.Commands
{
    public class DownloadFileCommand : IRequest<DownloadFileModel>
    {
        public int FileId { get; set; }
    }
}
