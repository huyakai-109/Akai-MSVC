namespace Shared.Common.Constants
{
    public static class ConfigKeys
    {
        public const string AutoMigration = "AutoMigration";
        public const string StorageUrl = "StorageUrl";

        public struct Databases
        {
            public const string MainDb = "ConnectionStrings:MyDatabase";
            public const string MainDb_Cus = "ConnectionStrings:CustomerDb";
            public const string Redis = "ConnectionStrings:RedisConnection";
        }

        public struct MinIO
        {
            public const string Endpoint = "MinIO:Endpoint";
            public const string AccessKey = "MinIO:AccessKey";
            public const string SecretKey = "MinIO:SecretKey";
            public const string Secure = "MinIO:Secure";
            public const string Bucket = "MinIO:Bucket";
            public const string Region = "MinIO:Region";
        }
    }
}
