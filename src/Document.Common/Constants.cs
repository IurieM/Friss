namespace Document.Common
{
    public static class Constants
    {
        public static class FileStorageType
        {
            public const string FileSystem = "FileSystem";
        }

        public static class Roles
        {
            public const string Admin = "Admin";
        }

        public static class ErrorCodes
        {
            public const string InvalidCredentials = "invalid_credentials";
            public const string DuplicateFileName = "duplicate_file_name";
            public const string FileNotFound = "file_not_found";
            public const string Required = "required";
        }
    }
}
