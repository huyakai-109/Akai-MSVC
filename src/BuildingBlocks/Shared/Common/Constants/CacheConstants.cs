namespace Shared.Common.Constants
{
    public static class CacheConstants
    {
        public struct Redis
        {
            public const string InstanceName = "Akai.Redis_";
            public const int ExpirationInOneDay = 1; // 1 day
            public const int ExpirationInThreeDay = 3; // 3 day
            public const int ExpirationInSevenDays = 7; // 7 days
        }

        //public struct AllowCacheEntities
        //{
        //    public static readonly string[] All =
        //    [
        //       Cache1, 
        //       Cache2,
        //    ];

        //    public const string Cache1 = "Cache1";
        //    public const string Cache2 = "Cache2";
        //}
    }
}
