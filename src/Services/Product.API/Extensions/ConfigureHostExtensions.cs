﻿namespace Product.API.Extensions
{
    public static class ConfigureHostExtensions
    {
        public static void AddAppConfigurations(this WebApplicationBuilder builder)
        {
            var env = builder.Environment;
            builder.Configuration
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
        }
    }
}
