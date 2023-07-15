namespace MyCustomWebServer.Routing
{
    using Http;
    using Common;

    public interface IRoutingTable
    {
        IRoutingTable MapStaticFiles(string folder = Settings.StaticFilesRootFolder);
        IRoutingTable Map(HttpMethod method, string path, HttpResponse response);
        IRoutingTable Map(HttpMethod method, string path, Func<HttpRequest, HttpResponse> responseFunc);

        IRoutingTable MapGet(string path, HttpResponse response);

        IRoutingTable MapGet(string path, Func<HttpRequest, HttpResponse> responseFunc);

        IRoutingTable MapPost(string url, HttpResponse response);

        IRoutingTable MapPost(string path, Func<HttpRequest, HttpResponse> responseFunc);

        
        //IRoutingTable MapPut(string url, HttpResponse response);
        //IRoutingTable MapDelete(string url, HttpResponse response);

    }
}
