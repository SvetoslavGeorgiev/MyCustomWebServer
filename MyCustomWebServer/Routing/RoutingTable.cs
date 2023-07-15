namespace MyCustomWebServer.Routing
{
    using Common;
    using Http;
    using System;

    public class RoutingTable : IRoutingTable
    {
        private readonly Dictionary<HttpMethod, Dictionary<string, Func<HttpRequest, HttpResponse>>> routes;

        public RoutingTable() => this.routes = new()
        {
            [HttpMethod.Get] = new(),
            [HttpMethod.Post] = new(),
            [HttpMethod.Put] = new(),
            [HttpMethod.Delete] = new()
        };


        public IRoutingTable Map(HttpMethod method, string path, HttpResponse response)
        {
            Guard.AgainstNull(response, nameof(response));

            return Map(method, path, request => response);
        }

        public IRoutingTable Map(HttpMethod method, string path, Func<HttpRequest, HttpResponse> responseFunc)
        {
            Guard.AgainstNull(path, nameof(path));
            Guard.AgainstNull(responseFunc, nameof(responseFunc));

            routes[method][path.ToLower()] = responseFunc;

            return this;
        }



        public IRoutingTable MapGet(string path, HttpResponse response)
            => MapGet(path, request => response);

        public IRoutingTable MapGet(string path, Func<HttpRequest, HttpResponse> responseFunc)
            => Map(HttpMethod.Get, path, responseFunc);

        public IRoutingTable MapPost(string path, HttpResponse response)
            => MapPost(path, request => response);

        public IRoutingTable MapPost(string path, Func<HttpRequest, HttpResponse> responseFunc)
            => Map(HttpMethod.Post, path, responseFunc);

        public HttpResponse ExecuteRequest(HttpRequest request)
        {
            var requestMethod = request.Method;
            var requsePath = request.Path.ToLower();

            if (!routes.ContainsKey(requestMethod) || !routes[requestMethod].ContainsKey(requsePath))
            {
                return new HttpResponse(HttpStatusCode.NOT_FOUND);
            }

            var responseFunc = routes[requestMethod][requsePath];

            return responseFunc(request);
        }

        public IRoutingTable MapStaticFiles(string folder = Settings.StaticFilesRootFolder)
        {
            const char DirectoryPathseparator = '\\';
            var currentDirectory = Directory.GetCurrentDirectory();
            var staticFilesFolder = Path.Combine(currentDirectory + DirectoryPathseparator + folder);

            if (!Directory.Exists(staticFilesFolder))
            {
                return this;
            }

            var staticFiles = Directory.GetFiles(staticFilesFolder, "*.*", SearchOption.AllDirectories);



            foreach ( var file in staticFiles)
            {
                const char UrlPathseparator = '/';

                var relativePath = Path.GetRelativePath(staticFilesFolder, file);

                var urlPath = UrlPathseparator + relativePath.Replace(DirectoryPathseparator, UrlPathseparator);

                MapGet(urlPath, request =>
                {
                    var content = File.ReadAllBytes(file);

                    var fileExtension = Path.GetExtension(file).Trim('.');
                    var contentType = HttpContentType.GetByFileExtension(fileExtension);

                    return new HttpResponse(HttpStatusCode.OK)
                        .SetContent(content, contentType);
                });
            }

            return this;
        }
    }
}
