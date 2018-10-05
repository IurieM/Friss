using System;

namespace Document.Data.Entities
{
    public class File : Entity
    {
        public string Name { get; set; }
        public string ContentType { get; set; }
        public decimal Size { get; set; }
        public DateTime UploadedDate { get; set; }
        public DateTime LastAccessedDate { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
