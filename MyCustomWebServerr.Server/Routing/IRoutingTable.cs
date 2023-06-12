namespace MyCustomWebServer.Server.Routing
{
    using MyCustomWebServer.Server.Http;
    using MyCustomWebServer.Server.Responses;

    public interface IRoutingTable
    {
        IRoutingTable Map(HttpMethod method, string path, HttpResponse response);

        IRoutingTable MapGet(string path, HttpResponse response);
        IRoutingTable MapPost(string url, HttpResponse response);
        //IRoutingTable MapPut(string url, HttpResponse response);
        //IRoutingTable MapDelete(string url, HttpResponse response);

    }
}
