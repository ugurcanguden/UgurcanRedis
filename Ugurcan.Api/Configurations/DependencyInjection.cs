using Ugurcan.Core;

namespace Ugurcan.Api
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Inversion Of Controller 
        /// </summary>
        /// <param name="services"></param>
        public static void DependencyInjectionSettings(this IServiceCollection services)
        {
            services.AddSingleton<IUtilitiesManagement, UtilitiesManagement>();
            services.AddSingleton<ICacheService, RedisCacheService>();
        }
    }
}
