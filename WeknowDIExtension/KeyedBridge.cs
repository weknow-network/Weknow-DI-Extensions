namespace Microsoft.Extensions.DependencyInjection
{
    internal record KeyedBridge<T> (T Target, string Key) : IKeyedBridge<T> where T : class;
}
