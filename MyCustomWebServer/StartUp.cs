namespace MyCustomWebServer
{
    using MyCustomWebServer.Server;
    using MyCustomWebServer.Server.Responses;

    public class StartUp
    {
        static async Task Main(string[] args)
            => await new HttpServer(routes => routes
            .MapGet("/", new TextResponse("Hello From Svetoslav"))
            .MapGet("/Dogs", new HtmlResponse("<h1>Hello from Dogs</h1>")))
            .Start();
    }
}