namespace MyCustomWebServer.Server.Routing
{
    using MyCustomWebServer.Server.Http;

    public interface IRoutingTable
    {
        IRoutingTable Map(string url, HttpMethod method, HttpResponse response);

        IRoutingTable MapGet(string url, HttpResponse response);
        IRoutingTable MapPost(string url, HttpResponse response);
        //void MapPut(string url, HttpResponse response);
        //void MapDelete(string url, HttpResponse response);

    }
}
