using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ugurcan.Core
{
    
    public class RedisCacheService : ICacheService
    {
        private IDatabase _redisDatebase;
        private IUtilitiesManagement _utilitiesManagement;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="utilitiesManagement"></param>
        public RedisCacheService(
            IUtilitiesManagement utilitiesManagement 
            )
        {
            _redisDatebase = RedisConnectionFactory.Connection.GetDatabase(); 
            _utilitiesManagement = utilitiesManagement;
        }
        /// <summary>
        /// Datayı çeker...
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get<T>(string key)
        { 
            return _utilitiesManagement.Deserialize<T>(_redisDatebase.StringGet(key));
        }
        /// <summary>
        /// Datayı ekler..
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Add(string key, object value)
        {
            Add(key, value, DateTime.Now);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expireDate"></param>
        public void Add(string key, object value, DateTime expireDate)
        {
            var expireTimeSpan = expireDate.Subtract(expireDate.AddDays(-1));
            _redisDatebase.StringSet(key, _utilitiesManagement.Serialize(value), expireTimeSpan);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        public void Remove(string key)
        {
            _redisDatebase.KeyDelete(key);
        }
        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
          //_redisDatebase.FlushAllDatabases();             
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool KeyExists(string key)
        {
            return _redisDatebase.KeyExists(key);
        }

        public List<T> GetList<T>(string key)
        {
            return _utilitiesManagement.Deserialize<List<T>>(_redisDatebase.StringGet(key));
        }
    }
}
