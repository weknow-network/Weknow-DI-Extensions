using System.Diagnostics.CodeAnalysis;

using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class WeknowServiceCollectionExtensions
    {
        #region AddKeyed

        /// <summary>
        /// Adds the keyed.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="services">The services.</param>
        /// <param name="key">The key.</param>
        /// <param name="lifetime">The lifetime.</param>
        /// <returns></returns>
        private static IServiceCollection AddKeyed<TService, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TImplementation>(
            this IServiceCollection services,
            string key,
            ServiceLifetime lifetime)
            where TService : class where TImplementation : class, TService
        {
            switch (lifetime)
            {
                case ServiceLifetime.Singleton:
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
                    break;
                case ServiceLifetime.Scoped:
                    services.AddScoped<TService, TImplementation>();
                    services.AddScoped<IKeyedBridge<TService>>(sp =>
                    {
                        var items = sp.GetServices<TService>().Where(m => m is TImplementation);
                        var imp = items.First();
                        var keyed = new KeyedBridge<TService>(imp, key);

                        return keyed;
                    });
                    services.AddScoped<IKeyed<TService>>(sp =>
                    {
                        return new Keyed<TService>(sp);
                    });
                    break;
                case ServiceLifetime.Transient:
                    services.AddTransient<TService, TImplementation>();
                    services.AddTransient<IKeyedBridge<TService>>(sp =>
                    {
                        var items = sp.GetServices<TService>().Where(m => m is TImplementation);
                        var imp = items.First();
                        var keyed = new KeyedBridge<TService>(imp, key);

                        return keyed;
                    });
                    services.AddTransient<IKeyed<TService>>(sp =>
                    {
                        return new Keyed<TService>(sp);
                    });
                    break;
            }


            return services;
        }

        private static IServiceCollection AddKeyed<TService, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TImplementation>(
            this IServiceCollection services, 
            string key,
            Func<IServiceProvider, TImplementation> implementationFactory,
            ServiceLifetime lifetime) 
            where TService : class where TImplementation : class, TService
        {
            switch (lifetime)
            {
                case ServiceLifetime.Singleton:
                    services.AddSingleton<TService, TImplementation>(implementationFactory);
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
                    break;
                case ServiceLifetime.Scoped:
                    services.AddScoped<TService, TImplementation>(implementationFactory);
                    services.AddScoped<IKeyedBridge<TService>>(sp => 
                    {
                        var items = sp.GetServices<TService>().Where(m => m is TImplementation);
                        var imp = items.First();
                        var keyed = new KeyedBridge<TService>(imp, key);

                        return keyed;
                    });
                    services.AddScoped<IKeyed<TService>>(sp => 
                    {
                        return new Keyed<TService>(sp);
                    });
                    break;
                case ServiceLifetime.Transient:
                    services.AddTransient<TService, TImplementation>(implementationFactory);
                    services.AddTransient<IKeyedBridge<TService>>(sp => 
                    {
                        var items = sp.GetServices<TService>().Where(m => m is TImplementation);
                        var imp = items.First();
                        var keyed = new KeyedBridge<TService>(imp, key);

                        return keyed;
                    });
                    services.AddTransient<IKeyed<TService>>(sp => 
                    {
                        return new Keyed<TService>(sp);
                    });
                    break;
            }


            return services;
        }

        #endregion // AddKeyed

        #region AddKeyedTransient

        /// <summary>
        /// Adds a transient service of the type specified in TService with an implementation
        /// type specified in TImplementation to the specified Microsoft.Extensions.DependencyInjection.IServiceCollection.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="services">The services.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static IServiceCollection AddKeyedTransient<TService, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TImplementation>(
            this IServiceCollection services,
            string key ) 
            where TService : class where TImplementation : class, TService
        {
            return services.AddKeyed<TService, TImplementation>(key, ServiceLifetime.Transient);
        }


        /// <summary>
        /// Adds a transient service of the type specified in TService to the specified
        ///  Microsoft.Extensions.DependencyInjection.IServiceCollection. 
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="services">The services.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static IServiceCollection AddKeyedTransient<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TService>(
            this IServiceCollection services,
            string key) 
            where TService : class
        {
            return services.AddKeyed<TService, TService>(key, ServiceLifetime.Transient);
        }

        /// <summary>
        /// Adds a transient service of the type specified in TService with a factory specified
        /// in implementationFactory to the specified Microsoft.Extensions.DependencyInjection.IServiceCollection.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="services">The services.</param>
        /// <param name="implementationFactory">The implementation factory.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static IServiceCollection AddKeyedTransient<TService>(
            this IServiceCollection services, 
            Func<IServiceProvider, TService> implementationFactory,
            string key) where TService : class
        {
            return services.AddKeyed<TService, TService>(key, implementationFactory, ServiceLifetime.Transient);
        }

        /// <summary>
        /// Adds a transient service of the type specified in TService with an implementation
        /// type specified in TImplementation using the factory specified in implementationFactory
        /// to the specified Microsoft.Extensions.DependencyInjection.IServiceCollection.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="services">The services.</param>
        /// <param name="implementationFactory">The implementation factory.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">
        /// services
        /// or
        /// implementationFactory
        /// </exception>
        public static IServiceCollection AddKeyedTransient<TService, TImplementation>(
            this IServiceCollection services, 
            Func<IServiceProvider, TImplementation> implementationFactory,
            string key) 
            where TService : class where TImplementation : class, TService
        {
            return services.AddKeyed<TService,TImplementation >(key, implementationFactory, ServiceLifetime.Transient);
        }

        #endregion // AddKeyedTransient

        #region AddKeyedScoped

        /// <summary>
        /// Adds a Scoped service of the type specified in TService with an implementation
        /// type specified in TImplementation to the specified Microsoft.Extensions.DependencyInjection.IServiceCollection.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="services">The services.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static IServiceCollection AddKeyedScoped<TService, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TImplementation>(
            this IServiceCollection services,
            string key ) 
            where TService : class where TImplementation : class, TService
        {
            return services.AddKeyed<TService, TImplementation>(key, ServiceLifetime.Scoped);
        }


        /// <summary>
        /// Adds a Scoped service of the type specified in TService to the specified
        ///  Microsoft.Extensions.DependencyInjection.IServiceCollection. 
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="services">The services.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static IServiceCollection AddKeyedScoped<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TService>(
            this IServiceCollection services,
            string key) 
            where TService : class
        {
            return services.AddKeyed<TService, TService>(key, ServiceLifetime.Scoped);
        }

        /// <summary>
        /// Adds a Scoped service of the type specified in TService with a factory specified
        /// in implementationFactory to the specified Microsoft.Extensions.DependencyInjection.IServiceCollection.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="services">The services.</param>
        /// <param name="implementationFactory">The implementation factory.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static IServiceCollection AddKeyedScoped<TService>(
            this IServiceCollection services, 
            Func<IServiceProvider, TService> implementationFactory,
            string key) where TService : class
        {
            return services.AddKeyed<TService, TService>(key, implementationFactory, ServiceLifetime.Scoped);
        }

        /// <summary>
        /// Adds a Scoped service of the type specified in TService with an implementation
        /// type specified in TImplementation using the factory specified in implementationFactory
        /// to the specified Microsoft.Extensions.DependencyInjection.IServiceCollection.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="services">The services.</param>
        /// <param name="implementationFactory">The implementation factory.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">
        /// services
        /// or
        /// implementationFactory
        /// </exception>
        public static IServiceCollection AddKeyedScoped<TService, TImplementation>(
            this IServiceCollection services, 
            Func<IServiceProvider, TImplementation> implementationFactory,
            string key) 
            where TService : class where TImplementation : class, TService
        {
            return services.AddKeyed<TService,TImplementation >(key, implementationFactory, ServiceLifetime.Scoped);
        }

        #endregion // AddKeyedScoped

        #region AddKeyedSingleton

        /// <summary>
        /// Adds a Singleton service of the type specified in TService with an implementation
        /// type specified in TImplementation to the specified Microsoft.Extensions.DependencyInjection.IServiceCollection.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="services">The services.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static IServiceCollection AddKeyedSingleton<TService, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TImplementation>(
            this IServiceCollection services,
            string key ) 
            where TService : class where TImplementation : class, TService
        {
            return services.AddKeyed<TService, TImplementation>(key, ServiceLifetime.Singleton);
        }


        /// <summary>
        /// Adds a Singleton service of the type specified in TService to the specified
        ///  Microsoft.Extensions.DependencyInjection.IServiceCollection. 
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="services">The services.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static IServiceCollection AddKeyedSingleton<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TService>(
            this IServiceCollection services,
            string key) 
            where TService : class
        {
            return services.AddKeyed<TService, TService>(key, ServiceLifetime.Singleton);
        }

        /// <summary>
        /// Adds a Singleton service of the type specified in TService with a factory specified
        /// in implementationFactory to the specified Microsoft.Extensions.DependencyInjection.IServiceCollection.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="services">The services.</param>
        /// <param name="implementationFactory">The implementation factory.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static IServiceCollection AddKeyedSingleton<TService>(
            this IServiceCollection services, 
            Func<IServiceProvider, TService> implementationFactory,
            string key) where TService : class
        {
            return services.AddKeyed<TService, TService>(key, implementationFactory, ServiceLifetime.Singleton);
        }

        /// <summary>
        /// Adds a Singleton service of the type specified in TService with an implementation
        /// type specified in TImplementation using the factory specified in implementationFactory
        /// to the specified Microsoft.Extensions.DependencyInjection.IServiceCollection.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="services">The services.</param>
        /// <param name="implementationFactory">The implementation factory.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">
        /// services
        /// or
        /// implementationFactory
        /// </exception>
        public static IServiceCollection AddKeyedSingleton<TService, TImplementation>(
            this IServiceCollection services, 
            Func<IServiceProvider, TImplementation> implementationFactory,
            string key) 
            where TService : class where TImplementation : class, TService
        {
            return services.AddKeyed<TService,TImplementation >(key, implementationFactory, ServiceLifetime.Singleton);
        }

        #endregion // AddKeyedSingleton
    }

}
