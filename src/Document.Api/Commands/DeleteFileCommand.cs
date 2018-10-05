using MediatR;

namespace Document.Api.Commands
{
    public class DeleteFileCommand:IRequest<int>
    {
        public int FileId { get; set; }
    }
}
