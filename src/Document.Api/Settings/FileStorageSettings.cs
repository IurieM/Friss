namespace Document.Api.Settings
{
    public class FileStorageSettings
    {
        public string StorageType { get; set; }
        public FileSystemInfo FileSystem { get; set; }

        public sealed class FileSystemInfo
        {
            public string Path { get; set; }
        }

    }
}
