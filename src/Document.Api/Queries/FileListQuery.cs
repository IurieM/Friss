using Document.Api.Models;
using MediatR;
using System.Collections.Generic;

namespace Document.Api.Queries
{
    public class FileListQuery: IRequest<List<FileListModel>>
    {
        
    }
}
