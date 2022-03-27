using Microsoft.AspNetCore.Mvc;
using Ugurcan.Api.models;
using Ugurcan.Core;

namespace Ugurcan.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CacheController : Controller
    {
        private readonly ICacheService _cacheService;
        public CacheController(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        [HttpGet]
        public string GetValueString([FromQuery] string key)
        {
            return _cacheService.Get<string>(key); 
        }
        [HttpGet]
        public int GetValueInt([FromQuery] string key)
        {
            return  _cacheService.Get<int>(key); 
        }
        [HttpGet]
        public List<int> GetValueListInt([FromQuery] string key)
        {
            return _cacheService.GetList<int>(key);
        }
        [HttpGet]
        public void DeleteValue([FromQuery] string key)
        {
            _cacheService.Remove(key);
        }
        [HttpPost]
        public void AddValue([FromBody] CacheValue value)
        {
            _cacheService.Add(value.Key, value.Value);
        }
    }
}
