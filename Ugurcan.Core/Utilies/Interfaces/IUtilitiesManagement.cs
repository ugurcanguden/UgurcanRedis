using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ugurcan.Core
{
    public  interface IUtilitiesManagement
    {
        /// <summary>
        /// objeyi serileze eder.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        string Serialize(object obj);
        /// <summary>
        /// string objeye cast eder..
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        T Deserialize<T>(string data);
    }
}
