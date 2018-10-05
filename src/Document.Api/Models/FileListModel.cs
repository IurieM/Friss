using System;

namespace Document.Api.Models
{
    public class FileListModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Size { get; set; }
        public DateTime UploadedDate { get; set; }
        public DateTime LastAccessedDate { get; set; }
        public string UploadedBy { get; set; }
    }
}
