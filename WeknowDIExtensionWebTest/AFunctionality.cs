namespace Bnaya.Samples
{
    public class AFunctionality : IFunctionalityTransient
    {
        public AFunctionality()
        {
            Thread.Sleep(1);
        }
        public string Id { get; } = $"A {DateTime.Now:fff}";
    }
}
