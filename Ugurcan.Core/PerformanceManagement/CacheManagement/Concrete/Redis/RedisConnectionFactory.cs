using StackExchange.Redis; 

namespace Ugurcan.Core 
{
    public class RedisConnectionFactory
    {
        private static readonly Lazy<ConnectionMultiplexer> LazyConnection;

        static RedisConnectionFactory()
        {
            LazyConnection = new Lazy<ConnectionMultiplexer>(() =>
            {
                return ConnectionMultiplexer.Connect(RedisConfig.RedisConnectionString); 
            });
        }
        public static ConnectionMultiplexer Connection => LazyConnection.Value;

        public static IDatabase RedisCache => Connection.GetDatabase();
    }
}
