namespace Microsoft.Extensions.DependencyInjection
{
    internal interface IKeyedBridge<T>  where T : class
    {
        T Target { get; }

        string Key { get; }
    }
}
