namespace MyCustomWebServer.Controllers
{
    using MyCustomWebServer.Http;
    using MyCustomWebServer.Routing;
    using System.IO;

    public static class RoutingTableExtentions
    {

        public static IRoutingTable MapGet<TController>(
            this IRoutingTable routingTable,
            string path,
            Func<TController, HttpResponse> controllerFunc)
            where TController : Controller
            => routingTable.MapGet(path, request => controllerFunc(
                CreateController<TController>(request)));

        public static IRoutingTable MapPost<TController>(
            this IRoutingTable routingTable,
            string path, 
            Func<TController, HttpResponse> controllerFunc)
            where TController : Controller
            => routingTable.MapPost(path, request => controllerFunc(
                CreateController<TController>(request)));

        private static TController CreateController<TController>(HttpRequest request)
            => (TController)Activator.CreateInstance(typeof(TController), new[] { request });
    }
}
