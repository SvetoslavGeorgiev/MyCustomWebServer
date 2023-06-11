namespace MyCustomWebServer
{
    using MyCustomWebServer.Server;
    using System.Net;

    public class StartUp
    {
        static async Task Main(string[] args)
        {
            var address = IPAddress.Parse("127.0.0.1");
            var port = 8080;
            var server = new HttpServer(address, port);

            await server.Start();
        }
    }
}