using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ugurcan.Core
{
    public interface ICacheService
    {
        /// <summary>
        /// Cache listesini getirir.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T Get<T>(string key);
        /// <summary>
        /// Cache listesini getirir.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        List<T> GetList<T>(string key);
        /// <summary>
        /// Yeni veri ekler.
        /// </summary>
        /// <param name="key">Key değeri</param>
        /// <param name="value">İçeriği</param>
        void Add(string key,object value,DateTime expireDate);
        /// <summary>
        /// Yeni veri ekler.
        /// </summary>
        /// <param name="key">Key değeri</param>
        /// <param name="value">İçeriği</param>
        void Add(string key, object value);
        /// <summary>
        /// İçeriği siler
        /// </summary>
        /// <param name="key">silinecek verinin adı</param>
        void Remove(string key);
        /// <summary>
        /// Cache'i temizler
        /// </summary>
        void Clear();
        /// <summary>
        /// Key var mı 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool KeyExists(string key);
    }
}
