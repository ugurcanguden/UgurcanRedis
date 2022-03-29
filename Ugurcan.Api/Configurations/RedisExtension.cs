namespace Ugurcan.Api.Configurations
{
    public static class RedisExtension
    {
        public static void RedisSettings(this WebApplicationBuilder builder)
        {
            var section = builder.Configuration.GetSection("Redis");
            var configurations = section.Get<RedisConfig>();
        }
    }
}
