namespace MyCustomWebServer.Server.Routing
{
    using MyCustomWebServer.Server.Common;
    using MyCustomWebServer.Server.Http;
    using MyCustomWebServer.Server.Responses;
    using System;

    public class RoutingTable : IRoutingTable
    {
        private readonly Dictionary<HttpMethod, Dictionary<string, HttpResponse>> routes;

        public RoutingTable() => this.routes = new()
        {
            [HttpMethod.Get] = new(),
            [HttpMethod.Post] = new(),
            [HttpMethod.Put] = new(),
            [HttpMethod.Delete] = new()
        };


        public IRoutingTable Map(HttpMethod method, string path, HttpResponse response)
        {
            Guard.AgainstNull(path, nameof(path));
            Guard.AgainstNull(response, nameof(response));

            routes[HttpMethod.Get][path] = response;

            return this;
        }
            


        public IRoutingTable MapGet(string path, HttpResponse response)
            => Map(HttpMethod.Get, path, response);

        public IRoutingTable MapPost(string path, HttpResponse response)
            => Map(HttpMethod.Post, path, response);

        public HttpResponse MatchRequest(HttpRequest request)
        {
            var requestMethod = request.Method;
            var requsePath = request.Path;

            if (!routes.ContainsKey(requestMethod) || !routes[requestMethod].ContainsKey(requsePath))
            {
                return new NotFoundResponse();
            }

            return routes[requestMethod][requsePath];
        }
    }
}
