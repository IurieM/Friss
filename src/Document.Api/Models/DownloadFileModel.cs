using System.IO;

namespace Document.Api.Models
{
    public class DownloadFileModel
    {
        public Stream Content { get; set; }
        public string Name { get; set; }
        public string ContentType { get; set; }
    }
}
