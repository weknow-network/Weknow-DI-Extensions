namespace Bnaya.Samples
{
    public class BFunctionality : IFunctionalityTransient
    {
        public string Id { get; } = $"B {DateTime.Now:fff}";
    }
}
