namespace Bnaya.Samples
{
    public class Component<T> where T : class,  IFunctionality
    {
        private readonly IKeyed<T> _selector;

        public Component(IKeyed<T> selector)
        {
            _selector = selector;
        }

        public string GetText(string k) => _selector[k].Id; 
    }
}
