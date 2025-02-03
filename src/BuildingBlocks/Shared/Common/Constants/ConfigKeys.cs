namespace Shared.Common.Constants
{
    public static class ConfigKeys
    {
        public const string AutoMigration = "AutoMigration";

        public struct Databases
        {
            public const string MainDb = "ConnectionStrings:MyDatabase";
            public const string Redis = "ConnectionStrings:RedisConnection";
        }
    }
}
