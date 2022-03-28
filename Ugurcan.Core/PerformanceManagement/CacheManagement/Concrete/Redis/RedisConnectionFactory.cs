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
                return ConnectionMultiplexer.Connect("localhost : 6379");//redis server conn string bilgisi, web config'den almak daha doğru ancak şimdilik buraya yazdık
            });
        }
        public static ConnectionMultiplexer Connection => LazyConnection.Value;

        public static IDatabase RedisCache => Connection.GetDatabase();
    }
}
