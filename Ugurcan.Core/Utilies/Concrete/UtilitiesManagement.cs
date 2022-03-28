using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ugurcan.Core
{
    public class UtilitiesManagement : IUtilitiesManagement
    { 
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public T Deserialize<T>(string data)
        {
            try
            {
                return JsonSerializer.Deserialize<T>(data) ?? Activator.CreateInstance<T>();
            }
            catch (Exception)
            {

                return Activator.CreateInstance<T>();
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string Serialize(object obj)
        {
            return JsonSerializer.Serialize(obj);
        }
    }
}
