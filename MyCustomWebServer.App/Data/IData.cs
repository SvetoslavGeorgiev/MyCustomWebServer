namespace MyCustomWebServer.App.Data
{
    using Models;

    public interface IData
    {
        IEnumerable<Cat> Cats { get; }
    }
}
