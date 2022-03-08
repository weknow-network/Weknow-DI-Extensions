namespace Microsoft.Extensions.DependencyInjection
{
    internal class Keyed<T>: IKeyed<T> where T : class
    {
        private readonly IServiceProvider _serviceProvider;

        public Keyed(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        T IKeyed<T>.this[string key] => 
                            _serviceProvider.GetServices<IKeyedBridge<T>>()
                                .Where(m => m.Key == key)
                                .First()
                                .Target;
    }
}
