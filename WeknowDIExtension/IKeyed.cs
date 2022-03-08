namespace Microsoft.Extensions.DependencyInjection
{
    public interface IKeyed<T>  where T : class
    {
        T this [string key] { get;  }
    }


}
