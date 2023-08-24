using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ugurcan.Core 
{    

        /// <summary>
    /// 
    /// </summary>
    public class RedisConnectionFactory: IRedisConnectionFactory {
         
        /// <summary>
        /// 
        /// </summary>
        /// <param name="redisConfigOptions"></param>
        public RedisConnectionFactory(IRedisConfig redisConfig) { 
            LazyConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(redisConfig.RedisConnectionString));
        }

        private readonly Lazy<ConnectionMultiplexer> LazyConnection;

        public ConnectionMultiplexer Connection => LazyConnection.Value;

        public IDatabase RedisCache => Connection.GetDatabase();
    } 
    /// <summary>
    /// RedisConfig
    /// </summary>
    public class RedisConfig : IRedisConfig {
        /// <summary>
        /// 
        /// </summary>
        public string RedisConnectionString { get; set; }
    }
    internal static class RedisConfig
    {
        public static string RedisConnectionString = "localhost:6379";
    }
     /// <summary>
    /// RedisConfig
    /// </summary>
    public class IRedisConfig {
        /// <summary>
        /// 
        /// </summary>
        public string RedisConnectionString { get; set; }
    }
        /// <summary>
    /// 
    /// </summary>
    public interface IRedisConnectionFactory {

        public ConnectionMultiplexer Connection { get; }

        public IDatabase RedisCache => Connection.GetDatabase();
    }   /// <summary>
    /// 
    /// </summary>
    public class RedisCache : IDistributedCache {
        private IDatabase _redisDatebase; 
        /// <summary>
        /// 
        /// </summary> 
        public RedisCache(IRedisConnectionFactory redisConnectionFactory) {
            _redisDatebase = redisConnectionFactory.Connection.GetDatabase();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<string> GetValueAsync(string key) {
            return await _redisDatebase.StringGetAsync(key);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<bool> SetValueAsync(string key, string value) {
            return await _redisDatebase.StringSetAsync(key, value);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<bool> DeleteValueAsync(string key) {
            return await _redisDatebase.KeyDeleteAsync(key);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        public async Task UpdateValueAsync(string key, string newValue) {
            if (await DeleteValueAsync(key)) {
                await SetValueAsync(key, newValue);
            }
        }
        /// <summary>
        /// HashSetAsync
        /// </summary        
        /// <param name="key"></param>
        /// <param name="hashEntries"></param>
        /// <returns></returns>
        public async Task HashSetAsync<T>(string key, List<T> list, string keyField) {
            await DeleteValueAsync(key);
            var hashEntries = ConvertListToHashEntry(list,keyField);
            await _redisDatebase.HashSetAsync(key, hashEntries);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<List<T>> HashGetAllAsync<T>(string key) { 
            var result =  await _redisDatebase.HashGetAllAsync(key);
            List<T> list = new List<T>();
            foreach(var item in result) {
                list.Add(System.Text.Json.JsonSerializer.Deserialize<T>(item.Value));
            }
            return list;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="keyField"></param>
        /// <returns></returns>
        private HashEntry[] ConvertListToHashEntry<T>(List<T> list,string keyField) {
            System.Reflection.PropertyInfo prop = typeof(T).GetProperty(keyField);
            HashEntry[] hashFields = new HashEntry[list.Count];
            foreach(var item in list) {
                string json = System.Text.Json.JsonSerializer.Serialize(item);
                hashFields[list.IndexOf(item)] = new HashEntry(prop.GetValue(item).ToString(),json);
            }
            return hashFields;
        } 
}
