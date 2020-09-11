namespace Xhznl.FileManagement
{
    public static class FileManagementDbProperties
    {
        public static string DbTablePrefix { get; set; } = "FileManagement";

        public static string DbSchema { get; set; } = null;

        public const string ConnectionStringName = "FileManagement";
    }
}
