namespace MyCustomWebServer.App
{
    using Responses;

    public class StartUp
    {
        static async Task Main(string[] args)
            => await new HttpServer(routes => routes
            .MapGet("/", new TextResponse("Hello From Svetoslav"))
            .MapGet("/Cats", request =>
            {
                const string nameKey = "Name";

                var query = request.Query;

                var CatName = query.ContainsKey(nameKey)
                    ? query[nameKey]
                    : "the cats";

                var result = $"<h1>Hello from {CatName}!</h1>";

                return new HtmlResponse(result);
            })
            .MapGet("/Dogs", new HtmlResponse("<h1>Hello from Dogs</h1>")))
            .Start();
}
}