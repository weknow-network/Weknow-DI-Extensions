using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Extensions
    {
        public static IServiceCollection AddKeyedSingleton<TService,  TImplementation>(
            this IServiceCollection services, string key) 
            where TService : class where TImplementation : class, TService
        {
            services.AddSingleton<TService, TImplementation>();
            services.AddSingleton<IKeyedBridge<TService>>(sp => 
            {
                var items = sp.GetServices<TService>().Where(m => m is TImplementation);
                var imp = items.First();
                var keyed = new KeyedBridge<TService>(imp, key);

                return keyed;
            });
            services.AddSingleton<IKeyed<TService>>(sp => 
            {
                return new Keyed<TService>(sp);
            });


            return services;
        }


    }
}
