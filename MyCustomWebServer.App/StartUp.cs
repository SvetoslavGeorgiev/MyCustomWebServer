namespace MyCustomWebServer.App
{
    using System.Threading.Tasks;
    using Controllers;
    using MyCustomWebServer;
    using MyCustomWebServer.Controllers;
    using MyCustomWebServer.Results.Views;
    using Data;

    public class StartUp
    {
        static async Task Main(string[] args)
            => await HttpServer
                .WithRoutes(routes => routes
                    .MapStaticFiles()
                    .MapControllers()
                    .MapGet<HomeController>("/ToCats", c => c.LocalRedirect()))
                .WithServices(services => services
                    .Add<IViewEngine, CompilationViewEngine>()
                    .Add<IData, MyDbContext>())
                .Start();
    }
}